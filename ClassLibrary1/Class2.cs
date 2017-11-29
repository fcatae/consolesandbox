using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class Class2 : IPlugin
    {
        public string Echo(string text)
        {
            return JsonConvert.SerializeObject(new { Type = "Json", Text = text });
        }
    }
}
