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

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;
        private ICarService _carService;

        public BrandManager(IBrandDal brandDal, ICarService carService)
        {
            _brandDal = brandDal;
            _carService = carService;
        }

        public IDataResult<Brand> GetByName(string brandName)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b=>b.BrandName == brandName));
        }

        [ValidationAspect(typeof(BrandValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
            IResult result = BusinessRules.Run(
                CheckIfBrandNameIsAlreadyExists(brand.BrandName)
                );

            if (result != null)
            {
                return result;
            }

            _brandDal.Add(brand);
            return new SuccessResult();
        }

        [CacheRemoveAspect("IBrandService.Get")]
        [TransactionScopeAspect]
        public IResult Delete(Brand brand)
        {
            IResult result = BusinessRules.Run(
                CheckIfBrandIdIsNotExists(brand.Id)
                );

            if (result != null)
            {
                return result;
            }

            _carService.GetCarsByBrandId(brand.Id).Data.ForEach(c=>_carService.Delete(c));

            _brandDal.Delete(brand);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId));
        }

        [ValidationAspect(typeof(BrandValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            IResult result = BusinessRules.Run(
                CheckIfBrandIdIsNotExists(brand.Id),
                CheckIfBrandNameIsAlreadyExists(brand.BrandName)
                );

            if (result != null)
            {
                return result;
            }

            _brandDal.Update(brand);
            return new SuccessResult();
        }

        private IResult CheckIfBrandIdIsNotExists(int brandId)
        {
            var result = _brandDal.Get(b => b.Id == brandId);
            if (result == null)
            {
                return new ErrorResult(Messages.BrandIsNotExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfBrandNameIsAlreadyExists(string brandName)
        {
            var result = _brandDal.Get(b => b.BrandName == brandName);
            if (result != null)
            {
                return new ErrorResult("Marka Mevcut");
            }

            return new SuccessResult();
        }
    }
}
