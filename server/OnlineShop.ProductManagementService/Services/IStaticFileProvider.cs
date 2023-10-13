namespace OnlineShop.ProductManagementService.Services
{
    public interface IStaticFileProvider
    {
        Task WriteFileAsync(IFormFile file, string filename);
        void DeleteFile(string fileName);
    }
}
