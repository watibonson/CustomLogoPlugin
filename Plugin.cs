using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Model.Plugins;
using System;
using System.Collections.Generic;

namespace Jellyfin.Plugin.CustomLogo
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public static Plugin Instance { get; private set; }

        public override string Name => "Custom Logo";
        public override Guid Id => Guid.Parse("2f14d3ec-7f4d-4dfc-b0ce-1a8d7a70d999");

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
            : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            yield return new PluginPageInfo
            {
                Name = "CustomLogo",
                EmbeddedResourcePath = GetType().Namespace + ".Views.configPage.html"
            };
        }
    }
}
