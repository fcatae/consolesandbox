using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace ConsoleSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string dllname = "Plugin\\ClassLibrary1.dll";

            var bytes = File.ReadAllBytes(dllname);

            // assembly = System.Reflection.Assembly.Load();
        }
    }
}
