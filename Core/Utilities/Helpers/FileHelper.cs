using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static IDataResult<string> CreateFile(IFormFile file)
        {
            string path = Directory.GetCurrentDirectory() + "\\wwwroot";
            string folder = "\\images\\";
            string defaultImage = (folder + "default_img.png").Replace("\\", "/");

            if (file == null)
            {
                return new SuccessDataResult<string>(defaultImage, "");
            }

            string extension = Path.GetExtension(file.FileName);
            string guid = Guid.NewGuid().ToString() + DateTime.Now.Millisecond + "_" + DateTime.Now.Hour + extension + "_" + DateTime.Now.Minute;
            string imagePath = folder + guid + extension;

            using (FileStream fileStream = File.Create(path + imagePath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
                imagePath = (imagePath).Replace("\\", "/");

                return new SuccessDataResult<string>(imagePath, "");
            }

        }

        public static IResult DeleteFile(string filePath)
        {
            string path = Directory.GetCurrentDirectory() + "\\wwwroot";
            string folder = "\\images\\";
            string defaultImage = (folder + "default_img.png").Replace("\\", "/");
            filePath = filePath.Replace("\\", "/");

            try
            {
                if (filePath != defaultImage)
                {
                    File.Delete(path + (filePath));
                }
            }
            catch (Exception)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }

        public static IDataResult<string> UpdateFile(IFormFile file, string filePath)
        {
            string imapePath = (filePath).Replace("\\", "/");
            DeleteFile(imapePath);
            var newImage = CreateFile(file);

            return newImage;
        }
    }
}
