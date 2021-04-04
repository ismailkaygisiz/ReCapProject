using Business.Abstract;
using Core.Aspects.Autofac.Authorization;
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
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;
        private IRentalService _rentalService;
        private ICarImageService _carImageService;

        public CarManager(ICarDal carDal, IRentalService rentalService, ICarImageService carImageService)
        {
            _carDal = carDal;
            _rentalService = rentalService;
            _carImageService = carImageService;
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

            // This actions deletes the rentals and images of the car
            _rentalService.GetByCarId(car.Id).Data.ForEach(r => _rentalService.Delete(r));
            _carImageService.GetByCarId(car.Id).Data.ForEach(c => _carImageService.Delete(c));

            _carDal.Delete(car);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByBrandId(int brandId)
        {
            var result = _carDal.GetCarDetails(c => c.BrandId == brandId);
            GetCarImages(result);

            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByColorId(int colorId)
        {
            var result = _carDal.GetCarDetails(c => c.ColorId == colorId);
            GetCarImages(result);

            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByDailyPrice(int min, int max)
        {
            var result = _carDal.GetCarDetails(c => c.DailyPrice >= min && c.DailyPrice <= max);
            GetCarImages(result);

            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<CarDetailDto> GetById(int id)
        {
            var result = _carDal.GetCarDetailsById(id);
            var images = _carDal.GetCarImages(result.Id);

            result.ImagePaths = images;

            return new SuccessDataResult<CarDetailDto>(result);
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetByModelYear(int modelYear)
        {
            var result = _carDal.GetCarDetails(c => c.ModelYear == modelYear);
            GetCarImages(result);

            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            var result = _carDal.GetCarDetails();
            GetCarImages(result);

            return new SuccessDataResult<List<CarDetailDto>>(result);
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorAndBrand(int colorId, int brandId)
        {
            List<CarDetailDto> result = new List<CarDetailDto>();

            var carsByBrand = GetByBrandId(brandId).Data;
            var carsByColor = GetByColorId(colorId).Data;

            foreach (var carBrand in carsByBrand)
            {
                foreach (var carColor in carsByColor)
                {
                    if (carColor.Id == carBrand.Id)
                    {
                        result.Add(carColor);
                    }
                }
            }

            GetCarImages(result);
            return new SuccessDataResult<List<CarDetailDto>>(result);
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

        private void GetCarImages(List<CarDetailDto> carDetailDtos)
        {
            foreach (var carDetailDto in carDetailDtos)
            {
                carDetailDto.ImagePaths = _carDal.GetCarImages(carDetailDto.Id);
            }
        }
    }
}
