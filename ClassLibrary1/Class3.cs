﻿using PluginSandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Class3 : IPlugin
    {
        public string Echo(string text)
        {
            // create sandbox
            var sandbox = new Sandbox();

            // create class2 into sandbox
            var class2 = sandbox.Create<Class2>();

            // call class2
            var result = class2.Echo(text);

            // return
            return result;
        }
    }
}