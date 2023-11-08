﻿// <copyright company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>
// <auto-generated/>

#nullable enable

namespace Datadog.Trace.Telemetry.Metrics;

/// <summary>
/// Extension methods for <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks" />
/// </summary>
internal static partial class IastInstrumentedSinksExtensions
{
    /// <summary>
    /// The number of members in the enum.
    /// This is a non-distinct count of defined names.
    /// </summary>
    public const int Length = 14;

    /// <summary>
    /// Returns the string representation of the <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks"/> value.
    /// If the attribute is decorated with a <c>[Description]</c> attribute, then
    /// uses the provided value. Otherwise uses the name of the member, equivalent to
    /// calling <c>ToString()</c> on <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to retrieve the string value for</param>
    /// <returns>The string representation of the value</returns>
    public static string ToStringFast(this Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks value)
        => value switch
        {
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.None => "vulnerability_type:none",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.WeakCipher => "vulnerability_type:weak_cipher",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.WeakHash => "vulnerability_type:weak_hash",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.SqlInjection => "vulnerability_type:sql_injection",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.CommandInjection => "vulnerability_type:command_injection",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.PathTraversal => "vulnerability_type:path_traversal",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.LdapInjection => "vulnerability_type:ldap_injection",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.Ssrf => "vulnerability_type:ssrf",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.UnvalidatedRedirect => "vulnerability_type:unvalidated_redirect",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.InsecureCookie => "vulnerability_type:insecure_cookie",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.NoHttpOnlyCookie => "vulnerability_type:no_httponly_cookie",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.NoSameSiteCookie => "vulnerability_type:no_samesite_cookie",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.WeakRandomness => "vulnerability_type:weak_randomness",
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.HardcodedSecret => "vulnerability_type:hardcoded_secret",
            _ => value.ToString(),
        };

    /// <summary>
    /// Retrieves an array of the values of the members defined in
    /// <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks" />.
    /// Note that this returns a new array with every invocation, so
    /// should be cached if appropriate.
    /// </summary>
    /// <returns>An array of the values defined in <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks" /></returns>
    public static Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks[] GetValues()
        => new []
        {
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.None,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.WeakCipher,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.WeakHash,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.SqlInjection,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.CommandInjection,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.PathTraversal,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.LdapInjection,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.Ssrf,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.UnvalidatedRedirect,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.InsecureCookie,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.NoHttpOnlyCookie,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.NoSameSiteCookie,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.WeakRandomness,
            Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.HardcodedSecret,
        };

    /// <summary>
    /// Retrieves an array of the names of the members defined in
    /// <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks" />.
    /// Note that this returns a new array with every invocation, so
    /// should be cached if appropriate.
    /// Ignores <c>[Description]</c> definitions.
    /// </summary>
    /// <returns>An array of the names of the members defined in <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks" /></returns>
    public static string[] GetNames()
        => new []
        {
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.None),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.WeakCipher),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.WeakHash),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.SqlInjection),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.CommandInjection),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.PathTraversal),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.LdapInjection),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.Ssrf),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.UnvalidatedRedirect),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.InsecureCookie),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.NoHttpOnlyCookie),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.NoSameSiteCookie),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.WeakRandomness),
            nameof(Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks.HardcodedSecret),
        };

    /// <summary>
    /// Retrieves an array of the names of the members defined in
    /// <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks" />.
    /// Note that this returns a new array with every invocation, so
    /// should be cached if appropriate.
    /// Uses <c>[Description]</c> definition if available, otherwise uses the name of the property
    /// </summary>
    /// <returns>An array of the names of the members defined in <see cref="Datadog.Trace.Telemetry.Metrics.MetricTags.IastInstrumentedSinks" /></returns>
    public static string[] GetDescriptions()
        => new []
        {
            "vulnerability_type:none",
            "vulnerability_type:weak_cipher",
            "vulnerability_type:weak_hash",
            "vulnerability_type:sql_injection",
            "vulnerability_type:command_injection",
            "vulnerability_type:path_traversal",
            "vulnerability_type:ldap_injection",
            "vulnerability_type:ssrf",
            "vulnerability_type:unvalidated_redirect",
            "vulnerability_type:insecure_cookie",
            "vulnerability_type:no_httponly_cookie",
            "vulnerability_type:no_samesite_cookie",
            "vulnerability_type:weak_randomness",
            "vulnerability_type:hardcoded_secret",
        };
}