// <copyright file="AerospikeTags.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using Datadog.Trace.ExtensionMethods;

namespace Datadog.Trace.Tagging
{
    internal class AerospikeTags : InstrumentationTags
    {
        protected static readonly IProperty<string>[] AerospikeTagsProperties =
            InstrumentationTagsProperties.Concat(
                new ReadOnlyProperty<AerospikeTags, string>(Trace.Tags.InstrumentationName, t => t.InstrumentationName),
                new Property<AerospikeTags, string>(Trace.Tags.AerospikeKey, t => t.Key, (t, v) => t.Key = v));

        public override string SpanKind => SpanKinds.Client;

        public string InstrumentationName => "aerospike";

        public string Key { get; set; }

        protected override IProperty<string>[] GetAdditionalTags() => AerospikeTagsProperties;
    }
}
