using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
