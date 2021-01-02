using System;
using System.Collections.Generic;
using System.Linq;

namespace JSonSample
{
    class Program
    {
        static void PrintKeys(List<KeyValuePair<string, string>> keys)
        {
            Console.WriteLine("****");
            Console.WriteLine("Print file");
            keys.ForEach(x => Console.WriteLine($"key: {x.Key} value: {x.Value}"));
            Console.WriteLine("****");
        }
        static void Main(string[] args)
        {
            var fileStore = new JsonFileStore();
            fileStore.SetKey("molleja", "caliente");
            PrintKeys(fileStore.GetKeys().ToList());
            fileStore.SetKey("molleja2", "frias");
            PrintKeys(fileStore.GetKeys().ToList());            
            fileStore.SetKey("molleja", "caliente");
            PrintKeys(fileStore.GetKeys().ToList());
        }

    }
}
