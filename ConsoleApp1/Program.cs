using System;
using System.Collections.Generic;
using FixerIoCore;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var teste = new[] {Symbol.GBP, Symbol.EUR};

            var fixer = new FixerIoClient(Symbol.BRL, teste);
            var test = fixer.GetLatest();

            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}