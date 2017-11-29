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
            _domain = AppDomain.CreateDomain(plugin);
            _domain.AssemblyResolve += SandboxAssemblyResolve;
        }

        public static Sandbox CreateFromFile(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);

            return new Sandbox(name);
        }

        public Assembly LoadAssembly(string path)
        {
            return Assembly.LoadFile(path);
        }

        private Assembly SandboxAssemblyResolve(object sender, ResolveEventArgs args)
        {            
            string baseDirectory = _domain.BaseDirectory;
            
            string dll = FindAssemblyFile(baseDirectory, args.Name);

            var asm = Assembly.LoadFile(dll);

            return asm;
        }

        private string FindAssemblyFile(string baseDirectory, string assemblyName)
        {
            AssemblyName asmName = new AssemblyName(assemblyName);
            string name = (new AssemblyName(assemblyName)).Name;
            string defaultName = $"{name}.dll";

            string[] validPaths = new string[] {
                Path.Combine(baseDirectory, "Plugin", this._plugin, $"{name}.dll"),
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
