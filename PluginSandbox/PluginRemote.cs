using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginSandbox
{
    public class PluginRemote : MarshalByRefObject
    {
        public PluginProxy Create(string assemblyName, string typeName)
        {
            var appDomain = AppDomain.CurrentDomain;

            var proxy = new PluginProxy(assemblyName, typeName);

            return proxy;
        }        
    }

}
