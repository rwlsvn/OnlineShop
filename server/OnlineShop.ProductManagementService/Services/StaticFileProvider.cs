using Microsoft.Extensions.Options;
using OnlineShop.ProductManagementService.Configuration;

namespace OnlineShop.ProductManagementService.Services
{
    public class StaticFileProvider : IStaticFileProvider
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _filePath;

        public StaticFileProvider(IWebHostEnvironment env, 
            IOptions<StaticFileConfiguration> options)
        {
            _env = env;
            _filePath = options.Value.ImagePath;
        }

        public async Task WriteFileAsync(IFormFile file, string filename)
        {
            var savePath = Path.Combine(_env.WebRootPath, _filePath);

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var fullPath = Path.Combine(savePath, filename);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        public void DeleteFile(string fileName)
        {
            var deletePath = Path.Combine(_env.WebRootPath, _filePath, fileName);

            if (Directory.Exists(deletePath) && File.Exists(deletePath))
            {
                File.Delete(deletePath);
            }
        }
    }
}
