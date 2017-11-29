using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSandbox
{
    public interface IPlugin
    {
        string Echo(string text);
    }
}
