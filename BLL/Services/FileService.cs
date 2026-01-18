using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FileService : IFileService
    {
        public async Task<string?> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

           
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "images");

            
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            
            var filePath = Path.Combine(imagesFolder, fileName);

     
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
