using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PluginSandbox
{
    public class Sandbox
    {
        public IPlugin CreateLocal<T>() where T: class
        {
            Type t = typeof(T);
            string assemblyName = t.Assembly.FullName;
            string typeName = t.FullName;

            var assembly = Assembly.Load(assemblyName);

            var obj = assembly.CreateInstance(typeName);

            return CreateProxy(obj);
        }
        public IPlugin Create<T>() where T : IPlugin, new()
        {
            //Type t = typeof(T);
            Type t = typeof(PluginFactory<T>);
            
            string assemblyName = t.Assembly.FullName;
            string typeName = t.FullName;

            var appDomain = CreateAppDomain(typeName, "plugin");
            var remoteFactory = (PluginFactory<T>)appDomain.CreateInstanceAndUnwrap(assemblyName, typeName);

            return remoteFactory.Create();
        }

        AppDomain CreateAppDomain(string name, string path)
        {
            var appInfo = new AppDomainSetup() { ApplicationBase = Path.GetFullPath(path) };
            var appDomain = AppDomain.CreateDomain(name, null, appInfo);

            return appDomain;
        }

        IPlugin CreateProxy(object obj)
        {
            return (IPlugin)obj;
        }
    }
}
