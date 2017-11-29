using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace ConsoleSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            (new Program()).Start();
        }

        void StartLoader()
        {
            Loader.Init();

            string dllname = "Plugin\\ClassLibrary1.dll";

            var bytes = LoadDll(dllname);

            var assembly = Assembly.Load(bytes);

            var class1 = CreateInstance(assembly, "ClassLibrary1.Class1");
            var class2 = CreateInstance(assembly, "ClassLibrary1.Class2");

            var mi1 = RunMethod("IPlugin.Echo", class1, "abc");

            // next command: fail
            var mi2 = RunMethod("IPlugin.Echo", class2, "fail");
        }
        void Start()
        {
            string dllname = "Plugin\\ClassLibrary1.dll";

            var bytes = LoadDll(dllname);

            var assembly = Assembly.Load(bytes);

            var class1 = CreateInstance(assembly, "ClassLibrary1.Class1");
            var class2 = CreateInstance(assembly, "ClassLibrary1.Class3");

            var mi1 = RunMethod("IPlugin.Echo", class1, "abc");

            // next command: fail
            var mi2 = RunMethod("IPlugin.Echo", class2, "fail");
        }

        void StartSandboxFailuire()
        {
            string dllname = "Plugin\\ClassLibrary1.dll";

            using (var sandbox = Sandbox.CreateFromFile(dllname))
            {
                //var assembly = sandbox.LoadAssembly(dllname);

                //var class1 = CreateInstance(assembly, "ClassLibrary1.Class1");
                //var class2 = CreateInstance(assembly, "ClassLibrary1.Class2");

                var class1 = sandbox.CreateInstance("ClassLibrary1", "ClassLibrary1.Class1");
                var class2 = sandbox.CreateInstance("ClassLibrary1", "ClassLibrary1.Class2");

                var mi1 = RunMethod("IPlugin.Echo", class1, "abc");

                // next command: fail
                var mi2 = RunMethod("IPlugin.Echo", class2, "fail");
            }            
        }

        byte[] LoadDll(string dllname)
        {
            return File.ReadAllBytes(dllname); 
        }

        object CreateInstance(Assembly assembly, string objectName)
        {
            Type pluginType = assembly.GetType(objectName);

            if (pluginType == null)
                return null;

            return Activator.CreateInstance(pluginType);
        }

        object RunMethod(string action, object instance, object param1)
        {
            string[] components = action.Split('.');

            string interfaceName = components[0];
            string methodName = components[1];

            Type pluginType = instance.GetType();

            var t = pluginType.GetInterface(interfaceName);
            var mi = t.GetMethod(methodName);

            var result = mi.Invoke(instance, new object[] { param1 });

            return result;
        }

    }
}
