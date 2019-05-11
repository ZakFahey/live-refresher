using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveRefresher
{
    class Config
    {
        public string TerrariaServerPath = "/";
        public int CheckInterval = 2000;
        public string[] PluginPathsToCheck = new string[] { };
        public string ServerStartupParameters = "";
    }
}
