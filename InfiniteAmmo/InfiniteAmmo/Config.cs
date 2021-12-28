using Qurre.API.Addons;
using System.ComponentModel;

namespace InfiniteAmmo
{
    public class Config : IConfig
    {
        [Description("Plugin Name")]
        public string Name { get; set; } = "InfiniteAmmo";

        [Description("Enable the plugin?")]
        public bool IsEnable { get; set; } = true;
        [Description("Remove ammo after falling out for optimization?")]
        public bool RemoveAmmo { get; set; } = true;
        [Description("Mode: Give infinite magazines [true] or infinite ammo without reloading [false]?")]
        public bool InfiniteMode { get; set; } = true;
    }
}
