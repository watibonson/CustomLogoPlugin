using MediaBrowser.Model.Plugins;

namespace CustomLogoPlugin
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string LogoPath { get; set; } = string.Empty;
    }
}
