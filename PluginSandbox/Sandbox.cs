using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginSandbox
{
    public class Sandbox
    {
        public IPlugin Create<T>() where T: class
        {
            Type t = typeof(T);
            string assemblyName = t.Assembly.FullName;
            string typeName = t.FullName;

            var assembly = Assembly.Load(assemblyName);

            var obj = assembly.CreateInstance(typeName);

            return CreateProxy(obj);
        }

        IPlugin CreateProxy(object obj)
        {
            return (IPlugin)obj;
        }
    }
}
