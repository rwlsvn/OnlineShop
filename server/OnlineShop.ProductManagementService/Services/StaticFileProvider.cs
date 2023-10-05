namespace OnlineShop.ProductManagementService.Services
{
    public class StaticFileProvider : IStaticFileProvider
    {
        public async Task WriteFileAsync(IFormFile file, string filename)
        {
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathToSave, filename);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        public void DeleteFile(string fileName)
        {
            var folderName = Path.Combine("Resources", "Images");
            var pathToDelete = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileName);

            if (File.Exists(pathToDelete))
            {
                File.Delete(pathToDelete);
            }
        }
    }
}
