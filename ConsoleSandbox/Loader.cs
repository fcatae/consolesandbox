using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace ConsoleSandbox
{
    // https://stackoverflow.com/questions/185836/equivalent-of-class-loaders-in-net
    public class Loader
    {
        private Loader(ModuleBuilder dynamicModule)
        {
            m_dynamicModule = dynamicModule;
            m_definedTypes = new HashSet<string>();
        }

        private static readonly Loader m_instance;
        private readonly ModuleBuilder m_dynamicModule;
        private readonly HashSet<string> m_definedTypes;

        static Loader()
        {
            //var name = new AssemblyName("$Runtime");
            //var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
            //var module = assemblyBuilder.DefineDynamicModule("$Runtime");
            //m_instance = new Loader(module);
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        public static void Init()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AppDomain appDomain = (AppDomain)sender;
            AssemblyName asmName = new AssemblyName(args.Name);
            
            string dll = Path.Combine(appDomain.BaseDirectory, "Plugin", $"{asmName.Name}.dll");

            var asm = Assembly.LoadFile(dll);

            return asm;
        }

        public static Loader Instance
        {
            get
            {
                return m_instance;
            }
        }

        public bool IsDefined(string name)
        {
            return m_definedTypes.Contains(name);
        }

        public TypeBuilder DefineType(string name)
        {
            //in a real system we would not expose the type builder.
            //instead a AST for the type would be passed in, and we would just create it.
            var type = m_dynamicModule.DefineType(name, TypeAttributes.Public);
            m_definedTypes.Add(name);
            return type;
        }
    }
}
