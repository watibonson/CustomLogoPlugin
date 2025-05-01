using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Jellyfin.Plugin.CustomLogo.Controllers
{
    [Route("Plugins/CustomLogo")]
    public class LogoController : ControllerBase
    {
        [HttpGet("Logo")]
        public IActionResult GetLogo()
        {
            var path = Plugin.Instance.Configuration.LogoPath;
            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
                return NotFound();
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/png");
        }

        [HttpPost("UploadLogo")]
        public IActionResult UploadLogo([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file");

            var dir = Path.Combine(
                Plugin.Instance.ApplicationPaths.PluginsPath,
                "CustomLogo");

            Directory.CreateDirectory(dir);
            var filePath = Path.Combine(dir, file.FileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            Plugin.Instance.Configuration.LogoPath = filePath;
            Plugin.Instance.SaveConfiguration();

            return NoContent();
        }
    }
}
