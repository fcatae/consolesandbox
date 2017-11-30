using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSandbox
{
    public interface IPlugin
    {
        string Repeat(string text);
    }

    public class PluginProxyClass : IPlugin
    {
        private PluginProxy _proxy;

        public PluginProxyClass(PluginProxy proxy)
        {
            this._proxy = proxy;
        }

        public string Repeat(string text)
        {
            var obj = _proxy.CreateSelf();

            return obj.Repeat(text);
        }
    }
}
