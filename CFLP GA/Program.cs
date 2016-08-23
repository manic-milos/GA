using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class Program
    {
        static void Main(string[] args)
        {

            List<int[]> arrs = new List<int[]>();
            HashSet<int[]> arrsset = new HashSet<int[]>();
            List<double> vals = new List<double>();
            int m = 30;
            EvaluationCache cache = new EvaluationCache(m);
            for (int i = 0; i < 500000; i++)
            {
                int[] arr = new int[m];
                for (int j = 0; j < arr.Length; j++)
                {
                    arr[j] = ControlledRandom.getRandomFromRange(0, 2);
                    //Console.Write(arr[j]);
                }
                arrsset.Add(arr);
                //Console.Write("-");

            }
            foreach(int[] arr in arrsset)
            {
                double randval = ControlledRandom.getRandomDouble();
                arrs.Add(arr);
                vals.Add(randval);
                cache.AddToCache(arr, randval);
                Console.Write(".");
            }
            for (int i = 0; i < arrs.Count; i++)
            {
                double checkvalue = cache.CheckCache(arrs[i]);
                if (checkvalue != vals[i])
                {
                    var arr = arrs[i];
                    for (int j = 0; j < arr.Length; j++)
                    {
                        Console.Write(arr[j]);
                    }
                    throw new Exception("not found!");
                }
                else
                    Console.WriteLine(i + ":" + checkvalue + "==" + vals[i]);
            } Console.Read();

            //TestOnData test = new TestOnData();
            //test.testSelectOnFolder(@"D:\reinstalacija\F\Projects\ConsoleApplication8\ConsoleApplication8\FormatedInstances",
            //    false, false, true, false);
            //Console.Read();
        }
    }
}
