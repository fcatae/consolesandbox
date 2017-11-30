using PluginSandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class4 : IPlugin2
    {
        public string LongEcho(string text)
        {
            // create sandbox
            var sandbox = new Sandbox("dll");

            // create class2 into sandbox
            var class2 = (IPlugin2)sandbox.Create<Class2>();

            // call class2
            var result = class2.LongEcho(text);

            // return
            return result;
        }
    }
}
