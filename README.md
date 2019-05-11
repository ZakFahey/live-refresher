# Live Refresher for TShock

Do you develop [TShock](https://github.com/Pryaxis/TShock) plugins? Then you can use this utility to automatically copy your server plugin from your Visual studio build directory and restart your server whenever you compile your plugin. 

All you need to do is download the executable [here](https://github.com/ZakFahey/live-refresher/releases) and copy it to your TShock directory. When you first run it, a config file, `live-refresher-config.json`, will be created. You will want to fill this out with your specific project parameters. Example config file:

```
{
  // The path to TerrariaServer.exe. Should be in the current directory
  "TerrariaServerPath": "",
  // The time in milliseconds between checks for files
  "CheckInterval": 2000,
  // Build path for your plugin. Be sure to also include any dependencies your plugin may have.
  "PluginPathsToCheck": [
    "C:\\Users\\...\\Documents\\GitHub\\yourplugin\\YourPlugin\\bin\\Debug\\YourPlugin.dll"
  ],
  // TShock's startup parameters. See https://tshock.readme.io/docs/command-line-parameters
  "ServerStartupParameters": "-ip 127.0.0.1 -port 7777 -maxplayers 8 -world \"C:/Users/../Documents/My Games/Terraria/Worlds/MyWorld.wld\""
}
```

Then just run `LiveRefresher.exe` in place of `TerrariaServer.exe`.
