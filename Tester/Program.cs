using ClassLibrary2;
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
            var class50 = new Class50();
            var result = class50.Echo("teste");

            Console.WriteLine("result = " + result);
        }
    }
}
