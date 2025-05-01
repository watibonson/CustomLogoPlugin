using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Plugins;
using System;

namespace CustomLogoPlugin
{
    public class Plugin : BasePlugin<PluginConfiguration>, IPlugin
    {
        public override string Name => "Custom Logo";
        public override string Description => "Allows uploading a custom logo to replace Jellyfin branding.";

        public Plugin(IApplicationPaths applicationPaths)
            : base(applicationPaths) { }

        public override Guid Id => Guid.Parse("abcdef01-2345-6789-abcd-ef0123456789");
    }
}
