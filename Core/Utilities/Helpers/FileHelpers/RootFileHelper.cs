using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Core.Utilities.Helpers.FileHelpers
{
    public class RootFileHelper : IFileHelper
    {
        public IDataResult<string> CreateFile(IFormFile file)
        {
            string path = Directory.GetCurrentDirectory() + "\\wwwroot";
            string folder = "\\images\\";
            string defaultImage = (folder + "default_img.png").Replace("\\", "/");

            if (file == null)
            {
                return new SuccessDataResult<string>(defaultImage, "");
            }

            string extension = Path.GetExtension(file.FileName);
            string guid = Guid.NewGuid().ToString() + DateTime.Now.Millisecond + "_" + DateTime.Now.Hour + "_" +
                          DateTime.Now.Minute;
            string imagePath = folder + guid + extension;

            while (File.Exists(path + imagePath))
            {
                guid = Guid.NewGuid().ToString() + DateTime.Now.Millisecond + "_" + DateTime.Now.Hour + "_" +
                       DateTime.Now.Minute;
                imagePath = folder + guid + extension;
            }

            using (FileStream fileStream = File.Create(path + imagePath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
                imagePath = imagePath.Replace("\\", "/");

                return new SuccessDataResult<string>(imagePath, "");
            }
        }

        public IResult DeleteFile(string filePath)
        {
            string path = Directory.GetCurrentDirectory() + "\\wwwroot";
            string defaultImage = "\\images\\default_img.png";

            if (filePath.Replace("/", "\\") != defaultImage && File.Exists(path + filePath))
            {
                File.Delete(path + filePath);
            }

            return new SuccessResult();
        }

        public IDataResult<string> UpdateFile(IFormFile file, string filePath)
        {
            DeleteFile(filePath);

            return CreateFile(file);
        }
    }
}
