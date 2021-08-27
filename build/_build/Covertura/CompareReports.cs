﻿// <copyright file="CompareReports.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Nuke.Common.IO;

namespace Covertura
{
    public static class CodeCoverage
    {
        public static CoverturaReport ReadReport(AbsolutePath path)
        {
            var doc = XDocument.Load(path);
            var report = new CoverturaReport();

            // get coverage element
            var coverage = doc.Descendants("coverage").First();

            report.LineRate = decimal.Parse(coverage.Attribute("line-rate").Value);
            report.BranchRate = decimal.Parse(coverage.Attribute("branch-rate").Value);
            report.LinesCovered = int.Parse(coverage.Attribute("lines-covered").Value);
            report.LinesValid = int.Parse(coverage.Attribute("lines-valid").Value);
            report.BranchesCovered = int.Parse(coverage.Attribute("branches-covered").Value);
            report.BranchesValid = int.Parse(coverage.Attribute("branches-valid").Value);
            report.Complexity = int.Parse(coverage.Attribute("complexity").Value);

            report.Packages = coverage
                             .Descendants("packages")
                             .SelectMany(x => x.Descendants("package"))
                             .Select(packageEle => new CoverturaReport.Package
                              {
                                  Name = packageEle.Attribute("name").Value,
                                  LineRate = decimal.Parse(packageEle.Attribute("line-rate").Value),
                                  BranchRate = decimal.Parse(packageEle.Attribute("branch-rate").Value),
                                  Complexity = int.Parse(packageEle.Attribute("complexity").Value),
                                  Classes = packageEle
                                           .Descendants("classes")
                                           .SelectMany(x => x.Descendants("class"))
                                           .Select(classEle =>
                                            {
                                                var lineHits = classEle.Descendants("lines")
                                                                       .First()
                                                                       .Descendants("line")
                                                                       .Select(x => int.Parse(x.Attribute("hits").Value) > 0)
                                                                       .ToList();

                                                return new CoverturaReport.ClassDetails()
                                                {
                                                    Name = classEle.Attribute("name").Value,
                                                    Filename = classEle.Attribute("filename").Value,
                                                    LineRate = decimal.Parse(classEle.Attribute("line-rate").Value),
                                                    BranchRate = decimal.Parse(classEle.Attribute("branch-rate").Value),
                                                    Complexity = int.Parse(classEle.Attribute("complexity").Value),
                                                    LinesCovered = lineHits.Count,
                                                    LinesValid = lineHits.Count(x => x),
                                                };
                                            })
                                           .ToDictionary(x => $"{x.Filename}_{x.Name}", x => x),
                              })
                             .ToDictionary(x => x.Name, x => x);

            return report;
        }

        public static CoverturaReportComparison Compare(CoverturaReport oldReport, CoverturaReport newReport)
        {
            var comparison = new CoverturaReportComparison
            {
                Old = oldReport,
                New = newReport,
                LineCoverageChange = decimal.Round(newReport.LineRate - oldReport.LineRate, decimals: 2),
                BranchCoverageChange = decimal.Round(newReport.BranchRate - oldReport.BranchRate, decimals: 2),
                ComplexityChange = newReport.Complexity - oldReport.Complexity,
            };

            var matchedPackages = new Dictionary<string, CoverturaReportComparison.PackageChanges>();

            foreach (var kvp in oldReport.Packages)
            {
                var oldPackage = kvp.Value;
                // try and find package in other report
                if (!newReport.Packages.TryGetValue(kvp.Key, out var newPackage))
                {
                    // package was removed
                    comparison.RemovedPackages.Add(oldPackage);
                    continue;
                }

                // do class-level comparison
                var changes = new CoverturaReportComparison.PackageChanges
                {
                    Old = oldPackage,
                    New = newPackage,
                    LineCoverageChange = decimal.Round(newPackage.LineRate - oldPackage.LineRate, decimals: 2),
                    BranchCoverageChange = decimal.Round(newPackage.BranchRate - oldPackage.BranchRate, decimals: 2),
                    ComplexityChange = newPackage.Complexity - oldPackage.Complexity,
                };

                foreach (var classKvp in oldPackage.Classes)
                {
                    var oldClass = classKvp.Value;
                    if (!newPackage.Classes.TryGetValue(classKvp.Key, out var newClass))
                    {
                        changes.RemovedClasses.Add(oldClass);
                        continue;
                    }

                    var changeSummary = new CoverturaReportComparison.ClassChanges
                    {
                        Name = oldClass.Name,
                        Filename = oldClass.Filename,
                        LineCoverageChange = decimal.Round(newClass.LineRate - oldClass.LineRate, decimals: 2),
                        BranchCoverageChange = decimal.Round(newClass.BranchRate - oldClass.BranchRate, decimals: 2),
                        ComplexityChange = newClass.Complexity - oldClass.Complexity,
                    };
                    // somewhat arbitrary
                    changeSummary.IsSignificantChange = Math.Abs(changeSummary.LineCoverageChange) > 0.05m
                                                     || Math.Abs(changeSummary.BranchCoverageChange) > 0.05m;

                    changes.ClassChanges[classKvp.Key] = changeSummary;

                }

                changes.NewClasses =
                    newPackage.Classes
                             .Where(kvp => !newPackage.Classes.ContainsKey(kvp.Key))
                             .Select(kvp => kvp.Value)
                             .ToList();

                matchedPackages.Add(kvp.Key, changes);
            }

            comparison.MatchedPackages = matchedPackages.Values.ToList();

            comparison.NewPackages =
                newReport.Packages
                         .Where(kvp => !matchedPackages.ContainsKey(kvp.Key))
                         .Select(kvp => kvp.Value)
                         .ToList();

            return comparison;
        }

        public static string RenderAsMarkdown(CoverturaReportComparison comparison, int prNumber, string oldReportLink, string newReportlink)
        {
            var oldBranchMarkdown = "[master](https://github.com/DataDog/dd-trace-dotnet/tree/master)";
            var newBranchMarkdown = $"#{prNumber}";
            var prFiles = $"https://github.com/DataDog/dd-trace-dotnet/pull/{prNumber}/files";
            var tree = "https://github.com/DataDog/dd-trace-dotnet/tree/master";
            var oldReport = comparison.Old;
            var newReport = comparison.New;

            var sb = new StringBuilder($@"## Code Coverage Report :bar_chart:

{GetIcon(comparison.LineCoverageChange)} Merging {newBranchMarkdown} into {oldBranchMarkdown} will {GetDescription(comparison.LineCoverageChange, "line coverage")}
{GetIcon(comparison.BranchCoverageChange)} Merging {newBranchMarkdown} into {oldBranchMarkdown} will {GetDescription(comparison.BranchCoverageChange, "branch coverage")}
{GetIcon(-comparison.ComplexityChange)} Merging {newBranchMarkdown} into {oldBranchMarkdown} will {GetComplexityDescription(comparison.ComplexityChange)}


|           | {oldBranchMarkdown} | {newBranchMarkdown}       | Change   | 
|:----------|:-----------:|:-----------:|:--------:|
| Lines     | `{oldReport.LinesCovered}` / `{oldReport.LinesValid}` | `{newReport.LinesCovered}` / `{newReport.LinesValid}` |          |
| Lines %   | `{oldReport.LineRate:F2}%`      | `{newReport.LineRate:F2}%`      |  `{comparison.LineCoverageChange:F2}%` {GetIcon(comparison.LineCoverageChange)}  |
| Branches  | `{oldReport.BranchesCovered}` / `{oldReport.BranchesValid}` | `{newReport.BranchesCovered}` / `{newReport.BranchesValid}` |          |
| Branches %| `{oldReport.BranchRate:F2}%`      | `{newReport.BranchRate:F2}%`      |  `{comparison.BranchCoverageChange:F2}%` {GetIcon(comparison.BranchCoverageChange)}  |
| Complexity|   `{oldReport.Complexity}`      | `{newReport.Complexity}`        |  `{comparison.ComplexityChange}`  {GetIcon(-comparison.ComplexityChange)}    |

View the full report for further details:

* [For master]({oldReportLink})
* [For this PR #{prNumber}]({newReportlink})

");
              foreach (var package in comparison.MatchedPackages)
              {
                  sb.Append($@"### {package.New.Name} Breakdown {GetIcon(package.LineCoverageChange)}

|        | {oldBranchMarkdown} | {newBranchMarkdown}       | Change   | 
|:-------|:-----------:|:-----------:|:--------:|
| Lines %| `{package.Old.LineRate:F2}%`      | `{package.New.LineRate:F2}%`       |  `{package.LineCoverageChange:F2}%` {GetIcon(package.LineCoverageChange)}  |
| Branches %| `{package.Old.BranchRate:F2}%`      | `{package.New.BranchRate:F2}%`       |  `{package.BranchCoverageChange:F2}%` {GetIcon(package.BranchCoverageChange)}  |
| Complexity| `{package.Old.Complexity}`      | `{package.New.Complexity}`       |  `{package.ComplexityChange}` {GetIcon(-package.ComplexityChange)}  |
");
                  if (package.ClassChanges.Any())
                  {
                      sb.Append($@"
The following classes have significant coverage changes.

| File    | Line coverage change | Branch coverage change | Complexity change |
|:--------|:--------------------:|:----------:|:--------:|");

                      var maxFileDisplay = 10;
                      var significantChanges = package.ClassChanges
                                                .Where(x => x.Value.IsSignificantChange)
                                                .OrderBy(x => x.Value.LineCoverageChange)
                                                .ThenBy(x => x.Value.BranchCoverageChange)
                                                .ThenBy(x => x.Value.Name)
                                                .ToList();
                      foreach (var classChange in significantChanges.Take(maxFileDisplay))
                      {
                          var change = classChange.Value;
                          sb.Append($@"
| [{change.Name}]({FixFilename(change.Filename)}) | `{change.LineCoverageChange:F2}%` {GetIcon(change.LineCoverageChange)} | `{change.BranchCoverageChange:F2}%` {GetIcon(change.BranchCoverageChange)}   | `{change.ComplexityChange}` {GetIcon(-change.ComplexityChange)} |");
                      }

                      var extras = significantChanges.Count - maxFileDisplay;
                      if (extras > 0)
                      {
                          sb.Append($@"
| ...And {extras} more  | | | |");
                      }

                      sb.AppendLine().AppendLine();
                  }

                  if (package.NewClasses.Any())
                  {
                      sb.Append($@"
The following classes were added in {newBranchMarkdown}:

| File    | Line coverage | Branch coverage | Complexity |
|:--------|:--------------------:|:----------:|:--------:|");

                      var maxFileDisplay = 5;
                      foreach (var newClass in package.NewClasses.Take(maxFileDisplay))
                      {
                          sb.Append($@"
| [{newClass.Name}]({prFiles}) | `{newClass.LineRate:F2}%` | `{newClass.BranchRate:F2}%` | `{newClass.Complexity}` |");
                      }

                      var extras = package.NewClasses.Count - maxFileDisplay;
                      if (extras > 0)
                      {
                          sb.Append($@"
| ...And {extras} more  | | | |");
                      }

                      sb.AppendLine().AppendLine();
                  }

                  if (package.RemovedClasses.Any())
                  {
                      sb.AppendLine($@"
{package.RemovedClasses.Count} classes were removed from {package.New.Name} in {newBranchMarkdown}")
                        .AppendLine();
                  }
              }

              if(comparison.NewPackages.Any())
              {
                  sb.Append($@"### New projects

{comparison.NewPackages.Count} were added in {newBranchMarkdown}:


| Project    | Line coverage | Branch coverage | Complexity |
|:--------|:--------------------:|:----------:|:--------:|");

                  var maxDisplay = 5;
                  foreach (var newProject in comparison.NewPackages.Take(maxDisplay))
                  {
                      sb.Append($@"
| [{newProject.Name}]({prFiles}) | `{newProject.LineRate:F2}%` | `{newProject.BranchRate:F2}%` | `{newProject.Complexity}` |");
                  }

                  var extras = comparison.NewPackages.Count - maxDisplay;
                  if (extras > 0)
                  {
                      sb.Append($@"
| ...And {extras} more  | | | |");
                  }
              }

              if (comparison.RemovedPackages.Any())
              {
                  sb.AppendLine($@"### Deleted projects


{comparison.RemovedPackages.Count} projects were removed in {newBranchMarkdown}

| Project    | Line coverage | Branch coverage | Complexity |
|:--------|:--------------------:|:----------:|:--------:|");

                  var maxDisplay = 5;
                  foreach (var oldProject in comparison.RemovedPackages.Take(maxDisplay))
                  {
                      sb.Append($@"
| [{oldProject.Name}]({prFiles}) | `{oldProject.LineRate:F2}%` | `{oldProject.BranchRate:F2}%` | `{oldProject.Complexity}` |");
                  }

                  var extras = comparison.RemovedPackages.Count - maxDisplay;
                  if (extras > 0)
                  {
                      sb.Append($@"
| ...And {extras} more  | | | |");
                  }
              }

              sb.AppendLine($@"
View the full reports for further details:

* [For master]({oldReportLink})
* [For this PR #{prNumber}]({newReportlink})");

              static string GetIcon(decimal value) => value switch
              {
                  <= -0.05m => ":no_entry:",
                  >= 0 => ":heavy_check_mark:",
                  _ => ":warning:",
              };

              static string GetDescription(decimal value, string metric) => value switch
              {
                  < 0 => $"will **decrease** {metric} by `{-value:F2}%`",
                  > 0 => $"will **increase** {metric} by `{value:F2}%`",
                  _ => $"**not change** {metric}",
              };

              static string GetComplexityDescription(int value) => value switch
              {
                  < 0 => $"will **decrease** complexity by `{-value}`",
                  > 0 => $"will **increase** complexity by `{value}`",
                  _ => $"**not change** complexity",
              };

              string FixFilename(string filename)
              {
                  return tree + filename
                               .Substring(8) // remove azdo file path prefix
                               .Replace('\\', '/');
              }

              return sb.ToString();
        }
    }
}
