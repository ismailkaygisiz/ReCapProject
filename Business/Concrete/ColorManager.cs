using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using Core.Aspects.Autofac.Authorization;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;
        private ICarService _carService;

        public ColorManager(IColorDal colorDal, ICarService carService)
        {
            _colorDal = colorDal;
            _carService = carService;
        }

        public IDataResult<Color> GetByName(string colorName)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorName == colorName));
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Add(Color color)
        {
            IResult result = BusinessRules.Run(
                CheckIfBrandNameIsAlreadyExists(color.ColorName)
            );

            if (result != null)
            {
                return result;
            }

            _colorDal.Add(color);
            return new SuccessResult();
        }

        [SecuredOperation("Admin")]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Delete(Color color)
        {
            IResult result = BusinessRules.Run(
                CheckIfColorIdIsNotExists(color.Id)
            );

            if (result != null)
            {
                return result;
            }

            _carService.GetCarsByColorId(color.Id).Data.ForEach(c => _carService.Delete(c));

            _colorDal.Delete(color);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.Id == colorId));
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(ColorValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IColorService.Get")]
        public IResult Update(Color color)
        {
            IResult result = BusinessRules.Run(
                CheckIfColorIdIsNotExists(color.Id),
                CheckIfBrandNameIsAlreadyExists(color.ColorName)
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

        private IResult CheckIfBrandNameIsAlreadyExists(string colorName)
        {
            var result = _colorDal.Get(b => b.ColorName == colorName);
            if (result != null)
            {
                return new ErrorResult("Renk Mevcut");
            }

            return new SuccessResult();
        }
    }
}
