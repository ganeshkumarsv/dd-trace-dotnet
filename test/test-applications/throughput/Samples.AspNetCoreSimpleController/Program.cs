using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection;

namespace Samples.AspNetCoreSimpleController
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(typeof(Datadog.Trace.ClrProfiler.Instrumentation).Assembly.FullName);
            Console.WriteLine();

            if (Environment.GetEnvironmentVariable("COR_ENABLE_PROFILING") == "1" ||
                Environment.GetEnvironmentVariable("CORECLR_ENABLE_PROFILING") == "1")
            {
                bool isAttached = IsProfilerAttached();
                Console.WriteLine(" * Checking if the profiler is attached: {0}", isAttached);
                if (!isAttached)
                {
                    Console.WriteLine("Error: Profiler is required and is not loaded.");
                    Environment.Exit(1);
                    return;
                }
            }
            else
            {
                Console.WriteLine(" * Running without profiler.");
            }

            Console.WriteLine();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static bool IsProfilerAttached()
        {
            try
            {
                // Forces loader injection on CallSite scenarios (not required in CallTarget).
                new HttpClient().GetAsync("http://localhost/bad-url").GetAwaiter().GetResult();
            }
            catch
            {
                //
            }

            Type nativeMethodsType = Type.GetType("Datadog.Trace.ClrProfiler.NativeMethods, Datadog.Trace.ClrProfiler.Managed");
            MethodInfo profilerAttachedMethodInfo = nativeMethodsType.GetMethod("IsProfilerAttached");
            try
            {
                return (bool)profilerAttachedMethodInfo.Invoke(null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }
    }
}
