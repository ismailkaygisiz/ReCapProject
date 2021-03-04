using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            IResult result = BusinessRules.Run(

                );

            if (result != null)
            {
                return result;
            }

            _brandDal.Add(brand);
            return new SuccessResult();
        }

        public IResult Delete(Brand brand)
        {
            IResult result = BusinessRules.Run(
                CheckIfBrandIdIsNotExists(brand.Id)
                );

            if (result != null)
            {
                return result;
            }

            _brandDal.Delete(brand);
            return new SuccessResult();
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.Id == brandId));
        }

        public IResult Update(Brand brand)
        {
            IResult result = BusinessRules.Run(
                CheckIfBrandIdIsNotExists(brand.Id)
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
    }
}