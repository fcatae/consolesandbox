using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var class10 = new Class1();
            var result = class10.Repeat("teste");

            Console.WriteLine("result = " + result);
        }
    }
}
