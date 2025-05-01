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

        public override Guid Id => Guid.Parse("b7e8dce1-44df-4b37-91a3-15fcf9f3b76a");
    }
}
