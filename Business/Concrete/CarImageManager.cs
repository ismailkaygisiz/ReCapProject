using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Utilities.Helpers.FileHelpers;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;
        private IFileHelper _fileHelper;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Add(CarImage carImage, IFormFile file)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarImageLimitExceeded(carImage.CarId)
            );

            if (result != null)
            {
                return result;
            }

            CarImage newCarImage = CreatedFile(file, carImage);
            _carImageDal.Add(newCarImage);

            return new SuccessResult();
        }

        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Delete(CarImage carImage)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarImageIdIsNotExists(carImage.Id)
            );

            if (result != null)
            {
                return result;
            }

            DeletedFile(carImage);
            _carImageDal.Delete(carImage);

            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
        }

        [ValidationAspect(typeof(CarImageValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICarImageService.Get")]
        public IResult Update(CarImage carImage, IFormFile file)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarImageIdIsNotExists(carImage.Id),
                CheckIfCarImageLimitExceeded(carImage.CarId)
            );

            if (result != null)
            {
                return result;
            }

            var newImage = UpdatedFile(file, carImage);
            _carImageDal.Update(newImage);

            return new SuccessResult();
        }

        private CarImage CreatedFile(IFormFile file, CarImage carImage)
        {
            var newImagePath = _fileHelper.CreateFile(file).Data;

            return new CarImage
            {
                CarId = carImage.CarId,
                Date = DateTime.Now,
                ImagePath = newImagePath
            };
        }

        private void DeletedFile(CarImage carImage)
        {
            var image = _carImageDal.Get(c => c.Id == carImage.Id);

            _fileHelper.DeleteFile(image.ImagePath);
        }

        private CarImage UpdatedFile(IFormFile file, CarImage carImage)
        {
            var image = _carImageDal.Get(c => c.Id == carImage.Id);
            var newImagePath = _fileHelper.UpdateFile(file, image.ImagePath).Data;
            var time = DateTime.Now;

            image.CarId = carImage.CarId;
            image.Date = time;
            image.ImagePath = newImagePath;


            return image;
        }

        private IResult CheckIfCarImageIdIsNotExists(int carImageId)
        {
            var result = _carImageDal.Get(c => c.Id == carImageId);
            if (result == null)
            {
                return new ErrorResult(Messages.CarImageIsNotExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCarImageLimitExceeded(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }

            return new SuccessResult();
        }
    }
}
