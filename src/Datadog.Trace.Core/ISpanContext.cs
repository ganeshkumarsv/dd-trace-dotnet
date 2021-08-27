// <copyright file="ISpanContext.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace Datadog.Trace
{
    /// <summary>
    /// Span context interface.
    /// </summary>
    public interface ISpanContext
    {
        /// <summary>
        /// Gets the trace identifier.
        /// </summary>
        ulong TraceId { get; }

        /// <summary>
        /// Gets the span identifier.
        /// </summary>
        ulong SpanId { get; }

        /// <summary>
        /// Gets the parent's span identifier.
        /// </summary>
        ulong? ParentId { get; }

        /// <summary>
        /// Gets the service name to propagate to child spans.
        /// </summary>
        string ServiceName { get; }

        /// <summary>
        /// Gets the origin of the trace.
        /// </summary>
        string Origin { get; }

        /// <summary>
        /// Gets the parent span context.
        /// </summary>
        ISpanContext Parent { get; }

        /// <summary>
        /// Gets the trace context.
        /// Returns null for contexts created from incoming propagated context.
        /// </summary>
        ITraceContext TraceContext { get; }

        /// <summary>
        /// Gets the sampling priority for contexts created from incoming propagated context.
        /// Returns null for local contexts.
        /// </summary>
        SamplingPriority? SamplingPriority { get; }
    }
}
