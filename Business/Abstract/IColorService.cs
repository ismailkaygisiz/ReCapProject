using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Color>> GetAll();
        IDataResult<Color> GetById(int colorId);
        IDataResult<Color> GetByName(string colorName);
        IResult Add(Color color);
        IResult Delete(Color color);
        IResult Update(Color color);
    }
}
