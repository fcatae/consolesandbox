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
        string _pluginDirectory;
        
        public Sandbox()
        {
            _pluginDirectory = ".";
        }

        public Sandbox(string pluginDirectory)
        {
            _pluginDirectory = pluginDirectory;
        }
        
        public PluginProxy Create(string assemblyName, string typeName)
        {
            Type t = typeof(PluginRemote);
            
            string assemblyRemote = t.Assembly.FullName;
            string typeRemote = t.FullName;

            var appDomain = CreateAppDomain(typeRemote, _pluginDirectory);
            
            var remote = (PluginRemote)appDomain.CreateInstanceAndUnwrap(assemblyRemote, typeRemote);

            return remote.Create(assemblyName, typeName);
        }

        public void Register<I,T>() 
            where I: class
            where T: I, new()
        {

        }

        public T Create<T>(string assemblyName, string typeName) where T: class
        {
            return null;
        }

        public object CreateAndRun(string assemblyName, string typeName) 
        {
            var proxy = Create(assemblyName, typeName);

            string param1 = "testeParam1";
            object result = proxy.Echo(param1);

            return result;
        }

        AppDomain CreateAppDomain(string name, string path)
        {
            string basePath = Path.GetFullPath(".");
            string privPath = Path.GetFullPath(path);

            var appInfo = new AppDomainSetup() { PrivateBinPath = privPath };
            var appDomain = AppDomain.CreateDomain(name, null, appInfo);

            //appDomain.AssemblyResolve += SandboxAssemblyResolver;

            return appDomain;
        }
        
        //private static Assembly SandboxAssemblyResolver(object sender, ResolveEventArgs args)
        //{
        //    AppDomain domain = (AppDomain)sender;
        //    string baseDirectory = domain.BaseDirectory;

        //    string dll = FindAssemblyFile(baseDirectory, args.Name);

        //    var asm = Assembly.LoadFile(dll);

        //    return asm;
        //}

        //private static string FindAssemblyFile(string baseDirectory, string assemblyName)
        //{
        //    AssemblyName asmName = new AssemblyName(assemblyName);
        //    string name = (new AssemblyName(assemblyName)).Name;
        //    string defaultName = $"{name}.dll";

        //    string[] validPaths = new string[] {
        //        // Path.Combine(baseDirectory, "Plugin", this._plugin, $"{name}.dll"),
        //        Path.Combine(baseDirectory, "Plugin", $"{name}.dll")
        //    };

        //    foreach (string path in validPaths)
        //    {
        //        if (File.Exists(path))
        //            return path;
        //    }

        //    return defaultName;
        //}        
    }
}
