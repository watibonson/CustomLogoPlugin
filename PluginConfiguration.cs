using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.CustomLogo
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string LogoPath { get; set; }
    }
}
