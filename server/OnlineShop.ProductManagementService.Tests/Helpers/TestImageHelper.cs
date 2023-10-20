namespace OnlineShop.ProductManagementService.Tests.Helpers
{
    internal class TestImageHelper
    {
        public static async Task<Stream> GetTestImageAsync()
        {
            var memoryStream = new MemoryStream();
            var fileStream = File.OpenRead("images/test-image.png");
            await fileStream.CopyToAsync(memoryStream);
            fileStream.Close();
            return memoryStream;
        }
    }
}
