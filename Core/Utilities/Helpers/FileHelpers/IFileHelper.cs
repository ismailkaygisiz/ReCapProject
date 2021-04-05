using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Helpers.FileHelpers
{
    public interface IFileHelper
    {
        IDataResult<string> CreateFile(IFormFile file);
        IResult DeleteFile(string filePath);
        IDataResult<string> UpdateFile(IFormFile file, string filePath);
    }
}
