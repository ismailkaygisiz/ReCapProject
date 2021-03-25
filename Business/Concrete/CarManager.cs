using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(

                );

            if (result != null)
            {
                return result;
            }

            _carDal.Add(car);
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarIdIsNotExists(car.Id)
                );

            if (result != null)
            {
                return result;
            }

            _carDal.Delete(car);
            return new SuccessResult();
        }

        [SecuredOperation("Car.GetAll")]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c=>c.BrandId == brandId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c=>c.ColorId == colorId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByDailyPrice(int min, int max)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.DailyPrice >= min && c.DailyPrice <= max));
        }

        [CacheAspect]
        public IDataResult<CarDetailDto> GetById(int id)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetailsById(id));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByModelYear(int modelYear)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails( c=> c.ModelYear == modelYear));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        [ValidationAspect(typeof(CarValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarIdIsNotExists(car.Id)
                );


            if (result != null)
            {
                return result;
            }

            _carDal.Update(car);
            return new SuccessResult();
        }

        private IResult CheckIfCarIdIsNotExists(int carId)
        {
            var result = _carDal.Get(c => c.Id == carId);
            if (result == null)
            {
                return new ErrorResult(Messages.CarIsNotExists);
            }

            return new SuccessResult();
        }
    }
}
