using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class2 : IPluginExtension
    {
        public string Repeat(string text)
        {
            return JsonConvert.SerializeObject(new { Type = "Json", Text = text, Description = "LKDJSLKFJSLKDFJKLJSFDLKJFS" });
        }
    }
}
