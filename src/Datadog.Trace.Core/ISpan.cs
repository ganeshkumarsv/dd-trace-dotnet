// <copyright file="ISpan.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;

namespace Datadog.Trace
{
    internal interface ISpan
    {
        ISpanContext Context { get; }

        ulong TraceId { get; }

        ulong SpanId { get; }

        string ServiceName { get; set; }

        string OperationName { get; set; }

        string ResourceName { get; set; }

        string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this span represents an error.
        /// </summary>
        bool Error { get; set; }

        /// <summary>
        /// Gets a value indicating whether this span is the top-level span for its service.
        /// </summary>
        bool IsTopLevel { get; }

        ISpan SetTag(string key, string value);

        string GetTag(string key);

        ISpan SetMetric(string key, double? value);

        double? GetMetric(string key);

        void SetException(Exception exception);
    }
}
