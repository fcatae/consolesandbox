using PluginSandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Class50 : IPlugin
    {
        public string Echo(string text)
        {
            var class2 = new ClassLibrary1.Class2();

            return class2.Echo(text);
        }
    }
}
