using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Core.Utilities.Helpers.FileHelpers
{
    public class RootFileHelper : IFileHelper
    {
        private string path = Directory.GetCurrentDirectory() + "\\wwwroot";
        private string folder = "\\images\\";
        private string defaultImage;

        public RootFileHelper()
        {
            defaultImage = (folder + "default_img.png").Replace("\\", "/");
        }

        public IDataResult<string> CreateFile(IFormFile file)
        {
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
            if (filePath.Replace("\\", "/") != defaultImage && File.Exists(path + filePath))
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
