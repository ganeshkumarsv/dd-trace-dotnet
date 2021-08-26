// <copyright file="IScope.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;

namespace Datadog.Trace
{
    /// <summary>
    /// A scope is a handle used to manage the concept of an active span.
    /// Meaning that at a given time at most one span is considered active and
    /// all newly created spans that are not created with the ignoreActiveSpan
    /// parameter will be automatically children of the active span.
    /// </summary>
    public interface IScope : IDisposable
    {
        /// <summary>
        /// Gets the span associated to this scope.
        /// </summary>
        ISpan Span { get; }

        /// <summary>
        /// Gets the scope that is the parent of this scope,
        /// or <c>null</c> if it has no parent.
        /// </summary>
        IScope Parent { get; }
    }
}
