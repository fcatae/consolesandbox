using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1 : IPlugin
    {
        public string Echo(string text)
        {
            return "Echo: " + text;
        }
    }
}
