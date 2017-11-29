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
            var class3 = new Class3();
            var result = class3.Echo("teste");

            Console.WriteLine("result = " + result);
        }
    }
}
