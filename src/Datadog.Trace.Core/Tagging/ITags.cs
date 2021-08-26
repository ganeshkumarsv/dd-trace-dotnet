// <copyright file="ITags.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

namespace Datadog.Trace.Tagging
{
    // TODO: rename to something like ITagBag or ITagCollection? add an indexer or even IDictionary<TKey,TValue>?

    /// <summary>
    /// Represents a collection of tags identified by a string key.
    /// </summary>
    public interface ITags
    {
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
        /// Add a the specified string tag to this span.
        /// </summary>
        /// <param name="key">The tag's name.</param>
        /// <param name="value">The tag's value.</param>
        void SetTag(string key, string value);

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
        /// Add a the specified numeric tag to this span.
        /// </summary>
        /// <param name="key">The tag's name.</param>
        /// <param name="value">The tag's value.</param>
        void SetMetric(string key, double? value);

        /// <summary>
        /// Serializes the tags in this collection into the specified buffer.
        /// </summary>
        /// <param name="buffer">The buffer to use to save the serialized tags.</param>
        /// <param name="offset">The offset within <paramref name="buffer"/> to begin saving bytes into.</param>
        /// <param name="span">The span these tags belong to. Used to determine the span's origin and if it is a top-level span.</param>
        /// <returns>The number of bytes used to serialize the tags.</returns>
        int SerializeTo(ref byte[] buffer, int offset, ISpan span);
    }
}
