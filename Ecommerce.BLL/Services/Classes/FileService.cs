using Ecommerce.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Services.Classes
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFile(IFormFile formFile)
        {
            if (formFile!=null && formFile.Length>0)
            {
                var fileName = Guid.NewGuid().ToString()+Path.GetExtension(formFile.FileName);
                var filePath=Path.Combine(Directory.GetCurrentDirectory(),"images",fileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                return fileName;
            }
            throw new NotImplementedException("error");
        }
    }
}
