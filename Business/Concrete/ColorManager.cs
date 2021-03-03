using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            IResult result = BusinessRules.Run(

               );

            if (result != null)
            {
                return result;
            }

            _colorDal.Add(color);
            return new SuccessResult();
        }

        public IResult Delete(Color color)
        {
            IResult result = BusinessRules.Run(
               CheckIfColorIdIsNotExists(color.Id)
               );

            if (result != null)
            {
                return result;
            }

            _colorDal.Delete(color);
            return new SuccessResult();
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == colorId));
        }

        public IResult Update(Color color)
        {
            IResult result = BusinessRules.Run(
               CheckIfColorIdIsNotExists(color.Id)
               );

            if (result != null)
            {
                return result;
            }

            _colorDal.Update(color);
            return new SuccessResult();
        }

        private IResult CheckIfColorIdIsNotExists(int colorId)
        {
            var result = _colorDal.Get(c => c.Id == colorId);
            if (result == null)
            {
                return new ErrorResult(Messages.ColorIsNotExists);
            }

            return new SuccessResult();
        }
    }
}