using MediaBrowser.Controller.Net;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace CustomLogoPlugin.Controllers
{
    [Route("customlogo")]
    public class LogoController : BaseApiController
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadLogo()
        {
            var file = Request.Form.Files["file"];
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var path = Path.Combine(Plugin.Instance.ConfigurationManager.ApplicationPaths.ConfigurationDirectoryPath, "customlogo.png");
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Plugin.Instance.Configuration.LogoPath = path;
            Plugin.Instance.SaveConfiguration();

            return Ok("Logo uploaded successfully.");
        }
    }
}
