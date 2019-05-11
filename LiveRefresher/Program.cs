using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LiveRefresher
{
    class Program
    {
        static Config config;
        static Process server = null;

        static void Main(string[] args)
        {
            if(!File.Exists("live-refresher-config.json"))
            {
                File.WriteAllText("live-refresher-config.json", JsonConvert.SerializeObject(new Config(), Formatting.Indented));
                Console.WriteLine("Config file `live-refresher-config.json` created. Please fill the config out and restart the program.");
                Console.ReadLine();
                Environment.Exit(1);
            }

            config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("live-refresher-config.json"));

            while(true)
            {
                var filesToCopy = new List<string>();
                foreach(string plugin in config.PluginPathsToCheck)
                {
                    if(CheckFiles(plugin)) filesToCopy.Add(plugin);
                }
                if(filesToCopy.Count > 0 || server == null || server.HasExited)
                {
                    RestartServerAndCopyFiles(filesToCopy);
                }
                Thread.Sleep(config.CheckInterval);
            }
        }

        static bool CheckFiles(string pluginPath)
        {
            string filename = Path.GetFileName(pluginPath);
            string newLocation = Path.Combine(config.TerrariaServerPath, "ServerPlugins", filename);
            if(!File.Exists(newLocation) || File.GetLastWriteTimeUtc(pluginPath) > File.GetLastWriteTimeUtc(newLocation))
            {
                Console.WriteLine($"{filename} has changed.");
                return true;
            }
            return false;
        }

        static void RestartServerAndCopyFiles(List<string> filesToCopy)
        {
            Console.WriteLine("Restarting server.");
            if(server != null && !server.HasExited)
            {
                server.Kill();
            }
            foreach(string file in filesToCopy)
            {
                string newLocation = Path.Combine(config.TerrariaServerPath, "ServerPlugins", Path.GetFileName(file));
                File.Copy(file, newLocation, true);
            }
            var startInfo = new ProcessStartInfo(Path.Combine(config.TerrariaServerPath, "TerrariaServer.exe"), config.ServerStartupParameters);
            server = Process.Start(startInfo);
        }
    }
}
