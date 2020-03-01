using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rent.ServiceLayers
{
    public static class FileUploaderService
    {
        static public string UploadProductPhoto(IFormFile formFile)
        {
            var filename = $"{Guid.NewGuid().ToString()}"+"_"+$"{formFile.FileName}";
            var path = $@"wwwroot/Images/Products/{filename}";
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formFile.CopyTo(fs);
            }
            return filename;
        }

        static public string UploadUserPhoto(IFormFile formFile)
        {
            var filename = $"{Guid.NewGuid().ToString()}" + "_" + $"{formFile.FileName}";
            var path = $@"wwwroot/Images/Users/{filename}";
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formFile.CopyTo(fs);
            }
            return filename;
        }
    }
}
