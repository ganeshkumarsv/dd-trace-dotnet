// <copyright file="ISpan.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using Datadog.Trace.Tagging;

namespace Datadog.Trace
{
    public interface ISpan
    {
        string ResourceName { get; set; }

        string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this span represents an error.
        /// </summary>
        bool Error { get; set; }

        ITags Tags { get; }

        ISpanContext Context { get; }

        /// <summary>
        /// Gets or sets operation name
        /// </summary>
        string OperationName { get; set; }

        DateTimeOffset StartTime { get; }

        TimeSpan Duration { get; }

        ISpan SetTag(string key, string value);

        string GetTag(string key);

        void SetException(Exception exception);

        /// <summary>
        /// Records the end time of the span and marks it ready to send.
        /// Any changes to the span after it is finished will be ignored.
        /// </summary>
        void Finish();

        /// <summary>
        /// Sets the end time of the span to the specified value and marks he span ready to send.
        /// Any changes to the span after it is finished will be ignored.
        /// </summary>
        /// <param name="finishTimestamp">Explicit value for the end time of the span.</param>
        void Finish(DateTimeOffset finishTimestamp);
    }
}
