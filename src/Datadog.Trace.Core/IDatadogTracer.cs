// <copyright file="IDatadogTracer.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using Datadog.Trace.Sampling;
using Datadog.Trace.Tagging;

namespace Datadog.Trace
{
    internal interface IDatadogTracer
    {
        string DefaultServiceName { get; }

        string AgentVersion { get; set; }

        IScopeManager ScopeManager { get; }

        ISampler Sampler { get; }

        /// <summary>
        /// Gets the currently active scope.
        /// Returns <c>null</c> if there is no active scope.
        /// </summary>
        IScope ActiveScope { get; }

        ISpan StartSpan(string operationName);

        ISpan StartSpan(string operationName, ISpanContext parent);

        ISpan StartSpan(string operationName, ISpanContext parent, string serviceName, DateTimeOffset? startTime, bool ignoreActiveScope);

        void Write(ArraySegment<ISpan> span);

        /// <summary>
        /// Make a span the active span and return its new scope.
        /// </summary>
        /// <param name="span">The span to activate.</param>
        /// <returns>A Scope object wrapping this span.</returns>
        IScope ActivateSpan(ISpan span);

        /// <summary>
        /// Make a span the active span and return its new scope.
        /// </summary>
        /// <param name="span">The span to activate.</param>
        /// <param name="finishOnClose">Determines whether closing the returned scope will also finish the span.</param>
        /// <returns>A Scope object wrapping this span.</returns>
        IScope ActivateSpan(ISpan span, bool finishOnClose);

        /// <summary>
        /// This is a shortcut for <see cref="StartSpan(string, ISpanContext, string, DateTimeOffset?, bool)"/>
        /// and <see cref="ActivateSpan(ISpan, bool)"/>. It creates a new span with the given parameters and makes it active.
        /// </summary>
        /// <param name="operationName">The span's operation name</param>
        /// <param name="parent">The span's parent</param>
        /// <param name="serviceName">The span's service name</param>
        /// <param name="startTime">An explicit start time for that span</param>
        /// <param name="ignoreActiveScope">If set the span will not be a child of the currently active span</param>
        /// <param name="finishOnClose">If set to false, closing the returned scope will not close the enclosed span </param>
        /// <returns>A scope wrapping the newly created span</returns>
        IScope StartActive(string operationName, ISpanContext parent = null, string serviceName = null, DateTimeOffset? startTime = null, bool ignoreActiveScope = false, bool finishOnClose = true);

        IScope StartActiveWithTags(string operationName, ISpanContext parent = null, string serviceName = null, DateTimeOffset? startTime = null, bool ignoreActiveScope = false, bool finishOnClose = true, ITags tags = null, ulong? spanId = null);
    }
}
