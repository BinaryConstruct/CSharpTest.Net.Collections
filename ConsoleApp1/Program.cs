using CSharpTest.Net.Collections;
using CSharpTest.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new BPlusTree<float, string>.OptionsV2(PrimitiveSerializer.Float, PrimitiveSerializer.String);
            options.CalcBTreeOrder(4, 24);
            options.CreateFile = CreatePolicy.Always;
            options.FileName = Path.GetTempFileName();
            options.CachePolicy = CachePolicy.Recent;
            options.CacheKeepAliveTimeout = 500;
            options.StoragePerformance = StoragePerformance.LogFileInCache;

            using (var tree = new BPlusTree<float, string>(options))
            {
                var sw = new Stopwatch();
                sw.Start();

                for (var i = 0; i < 1e6; i++)
                {
                    tree.Add(i, $"test{i}");
;               }

                Console.WriteLine($"Inserting 1M rows: {sw.Elapsed}");

                sw.Restart();

                var statValue = 749212.4f;
                int rank;
                if (tree.TryGetRank(statValue, out rank))
                {
                    Console.Write($"Value {statValue} is not in tree.");
                }
                Console.WriteLine($"{statValue} would be rank #{rank}");
                Console.WriteLine($"Fetching rank for stat in top ~750K: {sw.Elapsed}");

                sw.Restart();

                var test = tree.Top(999800, 100);

                Console.WriteLine($"Top players: Page 9998, 100 per page: {sw.Elapsed}");
            }

            Console.ReadLine();
        }
    }
}
