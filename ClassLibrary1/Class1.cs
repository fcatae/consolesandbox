using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class1 : IPlugin2
    {
        public string LongEcho(string text)
        {
            return "LOOOOOOOONG Echo: " + text;
        }
    }
}
