using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginSandbox
{
    class PluginProxy : MarshalByRefObject, IPlugin
    {
        string _assemblyName;
        string _typeName;

        public PluginProxy(string assemblyName, string typeName)
        {
            _assemblyName = assemblyName;
            _typeName = typeName;
            
            var appDomain = AppDomain.CurrentDomain;
        }

        public string Echo(string text)
        {
            var t = Type.GetType(_typeName);

            var appDomain = AppDomain.CurrentDomain;

            var assembly = Assembly.Load(_assemblyName);
            var instance = assembly.CreateInstance(_typeName);

            //var plugin = (IPlugin)instance;
            dynamic plugin = instance;

            return plugin.Echo(text);
        }
    }
}
