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
        public static void Init()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            AppDomain appDomain = (AppDomain)sender;
            AssemblyName asmName = new AssemblyName(args.Name);
            
            string dll = Path.Combine(appDomain.BaseDirectory, "Plugin", $"{asmName.Name}.dll");

            try
            {
                var asm = Assembly.LoadFile(dll);
                return asm;
            }
            catch
            { }

            return null;
        }        
    }
}
