// <copyright file="AerospikeCommon.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using Datadog.Trace.Configuration;
using Datadog.Trace.DuckTyping;
using Datadog.Trace.Tagging;
using Datadog.Trace.Vendors.Serilog;

namespace Datadog.Trace.ClrProfiler.AutoInstrumentation.Aerospike
{
    internal class AerospikeCommon
    {
        private const string ServiceName = "aerospike";
        private const string OperationName = "aerospike.command";
        public const string IntegrationName = nameof(IntegrationIds.Aerospike);
        internal static readonly IntegrationInfo IntegrationId = IntegrationRegistry.GetIntegrationInfo(IntegrationName);

        public static Scope CreateScope<TTarget>(Tracer tracer, out AerospikeTags tags, TTarget target)
        {
            tags = null;

            if (!tracer.Settings.IsIntegrationEnabled(IntegrationId))
            {
                // integration disabled, don't create a scope, skip this trace
                return null;
            }

            Scope scope = null;

            try
            {
                tags = new AerospikeTags();
                var serviceName = tracer.Settings.GetServiceName(tracer, ServiceName);
                scope = tracer.StartActiveWithTags(OperationName, tags: tags, serviceName: serviceName);
                var span = scope.Span;

                if (target.TryDuckCast<HasKey>(out var key) && key.Key != null)
                {
                    tags.Key = key.Key.ToString();
                }

                span.Type = SpanTypes.Aerospike;
                span.ResourceName = ExtractResourceName(target.GetType());

                tags.SetAnalyticsSampleRate(IntegrationId, tracer.Settings, enabledWithGlobalSetting: false);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error creating or populating scope.");
            }

            return scope;
        }

        private static string ExtractResourceName(Type type)
        {
            const string syncPrefix = "Sync";
            const string asyncPrefix = "Async";
            const string commandSuffix = "Command";

            var typeName = type.Name;
            var startIndex = 0;

            if (typeName.StartsWith(syncPrefix))
            {
                startIndex = syncPrefix.Length;
            }
            else if (typeName.StartsWith(asyncPrefix))
            {
                startIndex = asyncPrefix.Length;
            }

            var length = typeName.Length - startIndex;

            if (typeName.EndsWith(commandSuffix))
            {
                length -= commandSuffix.Length;
            }

            return typeName.Substring(startIndex, length);
        }
    }
}
