using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSandbox
{
    class Sandbox : IDisposable
    {
        string _plugin;
        AppDomain _domain;

        public Sandbox(string plugin)
        {
            _plugin = plugin;

            AppDomainSetup domaininfo = new AppDomainSetup();
            domaininfo.ApplicationBase = Path.GetFullPath("plugin");

            _domain = AppDomain.CreateDomain(plugin, null, domaininfo);
            //_domain.AssemblyResolve += SandboxAssemblyResolve;

            AppDomain.CurrentDomain.AssemblyResolve += SandboxAssemblyResolve;
        }

        public static Sandbox CreateFromFile(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);

            return new Sandbox(name);
        }


        public Assembly LoadAssembly(string assemblyFile)
        {
            return LoadAssembly(this._domain, assemblyFile);
        }

        static Assembly LoadAssembly(AppDomain domain, string assemblyFile)
        {
            var bytes = File.ReadAllBytes(assemblyFile);

            return domain.Load(bytes);
        }

        public object CreateInstance(string assemblyName, string typeName)
        {
            System.Runtime.Remoting.ObjectHandle a;
            a = _domain.CreateInstance(assemblyName, typeName);

            return a.CreateObjRef(typeof(IPlugin));
        }

        private static Assembly SandboxAssemblyResolve(object sender, ResolveEventArgs args)
        {
            AppDomain domain = (AppDomain)sender;
            string baseDirectory = domain.BaseDirectory;
            
            string dll = FindAssemblyFile(baseDirectory, args.Name);

            var asm = LoadAssembly(domain, dll);

            return asm;
        }

        private static string FindAssemblyFile(string baseDirectory, string assemblyName)
        {
            AssemblyName asmName = new AssemblyName(assemblyName);
            string name = (new AssemblyName(assemblyName)).Name;
            string defaultName = $"{name}.dll";

            string[] validPaths = new string[] {
                // Path.Combine(baseDirectory, "Plugin", this._plugin, $"{name}.dll"),
                Path.Combine(baseDirectory, "Plugin", $"{name}.dll")
            };

            foreach(string path in validPaths)
            {
                if (File.Exists(path))
                    return path;
            }

            return defaultName;
        }

        public void Dispose()
        {
            AppDomain.Unload(_domain);
        }
    }
}
