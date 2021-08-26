// <copyright file="ISpan.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using Datadog.Trace.Tagging;

namespace Datadog.Trace
{
    /// <summary>
    /// Defines the interface for spans.
    /// </summary>
    public interface ISpan
    {
        /// <summary>
        /// Gets the span's context.
        /// </summary>
        ISpanContext Context { get; }

        /// <summary>
        /// Gets the id of the trace that this span belongs to.
        /// </summary>
        ulong TraceId { get; }

        /// <summary>
        /// Gets the id of the span.
        /// </summary>
        ulong SpanId { get; }

        /// <summary>
        /// Gets or sets the span's service name.
        /// </summary>
        string ServiceName { get; set; }

        /// <summary>
        /// Gets or sets the span's operation name (aka span name).
        /// </summary>
        string OperationName { get; set; }

        /// <summary>
        /// Gets or sets the span's resource name.
        /// </summary>
        string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the span's type.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this span represents an error.
        /// </summary>
        bool Error { get; set; }

        /// <summary>
        /// Gets a value indicating whether this span is the top-level span for its service.
        /// </summary>
        bool IsTopLevel { get; }

        /// <summary>
        /// Gets the starting time of this span.
        /// </summary>
        DateTimeOffset StartTime { get; }

        /// <summary>
        /// Gets the during of this span.
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Gets the collection of tags associated to this span.
        /// </summary>
        ITags Tags { get; }

        /// <summary>
        /// Add a the specified string tag to this span.
        /// </summary>
        /// <param name="key">The tag's name.</param>
        /// <param name="value">The tag's value.</param>
        /// <returns>This span to allow method chaining.</returns>
        ISpan SetTag(string key, string value);

        /// <summary>
        /// Gets the value of the string tag with the specified name, or <c>null</c> if not found.
        /// </summary>
        /// <param name="key">The tag's name.</param>
        /// <returns>
        /// The value of the numeric tag with the specified name,
        /// or <c>null</c> if not found.
        /// </returns>
        string GetTag(string key);

        /// <summary>
        /// Add a the specified numeric tag to this span.
        /// </summary>
        /// <param name="key">The tag's name.</param>
        /// <param name="value">The tag's value.</param>
        /// <returns>This span to allow method chaining.</returns>
        ISpan SetMetric(string key, double? value);

        /// <summary>
        /// Gets the value of the numeric tag with the specified name, or <c>null</c> if not found.
        /// </summary>
        /// <param name="key">The tag's name.</param>
        /// <returns>
        /// The value of the numeric tag with the specified name,
        /// or <c>null</c> if not found.
        /// </returns>
        double? GetMetric(string key);

        /// <summary>
        /// Sets the span's error flag and adds error tags
        /// using values from the specified <paramref name="exception"/> object.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void SetException(Exception exception);
    }
}
