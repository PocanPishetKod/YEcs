// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Benchmark;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<GeneralBenchmark>();

/*var stopWatch = new Stopwatch();
stopWatch.Start();
new GeneralBenchmark().Create_1_000_000_Entities_And_Components_And_Update_Filters();
stopWatch.Stop();
Console.WriteLine(stopWatch.Elapsed);*/