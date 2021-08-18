// <copyright file="MassTransitAutofacSagaTest.cs" company="Datadog">
// Unless explicitly stated otherwise all files in this repository are licensed under the Apache 2 License.
// This product includes software developed at Datadog (https://www.datadoghq.com/). Copyright 2017 Datadog, Inc.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Datadog.Trace.ClrProfiler.IntegrationTests.SmokeTests
{
    public class MassTransitAutofacSagaTest : SmokeTestBase
    {
        public MassTransitAutofacSagaTest(ITestOutputHelper output)
        : base(output, "MassTransit.Autofac.Saga", maxTestRunSeconds: 90)
        {
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void NoExceptions()
        {
            CheckForSmoke(shouldDeserializeTraces: false);
        }
    }
}
