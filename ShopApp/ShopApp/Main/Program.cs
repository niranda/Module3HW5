using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace ShopApp.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var task = Task.Run(async () => await ParallelHandlerAsync());
            task.Wait();
            var result = task.Result;
            Console.WriteLine(result);
        }

        public static async Task<string> FirstReaderAsync()
        {
            var strReader = new StreamReader("hello.txt");
            return await Task.Run(async () => await strReader.ReadToEndAsync());
        }

        public static async Task<string> SecondReaderAsync()
        {
            var strReader = new StreamReader("world.txt");
            return await Task.Run(async () => await strReader.ReadToEndAsync());
        }

        public static async Task<string> ParallelHandlerAsync()
        {
            var list = new List<Task<string>>();
            list.Add(FirstReaderAsync());
            list.Add(SecondReaderAsync());
            await Task.WhenAll(list);

            var result = await list[0] + " " + await list[1];
            return result;
        }
    }
}
