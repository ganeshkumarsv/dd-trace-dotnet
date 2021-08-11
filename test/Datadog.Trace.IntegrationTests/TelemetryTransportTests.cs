﻿// <copyright file="TelemetryTransportTests.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Threading.Tasks;
using Datadog.Trace.Telemetry;
using Datadog.Trace.TestHelpers;
using FluentAssertions;
using Xunit;

namespace Datadog.Trace.IntegrationTests
{
    public class TelemetryTransportTests
    {
        private const string Env = "telemetrytest";
        private const string ServiceVersion = "2.0.0";

        [Fact]
        public async Task CanSendTelemetry()
        {
            using var agent = new MockTelemetryAgent<TelemetryData>(TcpPortProvider.GetOpenPort());
            var telemetryUri = new Uri($"http://localhost:{agent.Port}");

            var transport = new TelemetryTransportFactory(telemetryUri).Create();
            var sentData = new TelemetryData()
            {
                SeqId = 3,
                Env = "TracerTelemetryTest",
                ServiceName = "TelemetryTransportTests"
            };

            await transport.PushTelemetry(sentData);

            var received = agent.WaitForLatestTelemetry(x => x.Configuration?.TracerInstanceCount > 0);

            received.Should().NotBeNull();

            // check some basic values
            received.SeqId.Should().Be(sentData.SeqId);
            received.Env.Should().Be(sentData.Env);
            received.ServiceName.Should().Be(sentData.ServiceName);
        }
    }
}
