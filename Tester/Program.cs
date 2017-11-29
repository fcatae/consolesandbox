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
            var class4 = new Class4();
            var result = class4.Echo("teste");

            Console.WriteLine("result = " + result);
        }
    }
}
