using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSandbox
{
    [Serializable]
    public class PluginFactory<T> where T: IPlugin, new()
    {
        public IPlugin Create()
        {
            return new T();
        }
    }
}
