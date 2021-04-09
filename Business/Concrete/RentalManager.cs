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
using System;
using System.Collections.Generic;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        [ValidationAspect(typeof(RentalValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental, decimal customerFindeksPoint, decimal carFindeksPoint)
        {
            IResult result = BusinessRules.Run(
                CheckIfReturnDateNull(rental.CarId),
                CheckIfFindeksPointNotEnough(customerFindeksPoint, carFindeksPoint)
            );

            if (result != null)
            {
                return result;
            }

            _rentalDal.Add(rental);
            return new SuccessResult("Eklendi");
        }

        [TransactionScopeAspect]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental)
        {
            IResult result = BusinessRules.Run(
                CheckIfRentalIdIsNotExists(rental.Id)
            );

            if (result != null)
            {
                return result;
            }

            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<Rental>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId));
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == customerId));
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByRentDate(DateTime rentDate)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.RentDate == rentDate));
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetByReturnDate(DateTime returnDate)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.ReturnDate == returnDate));
        }

        [ValidationAspect(typeof(RentalValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental, decimal customerFindeksPoint, decimal carFindeksPoint)
        {
            IResult result = BusinessRules.Run(
                CheckIfRentalIdIsNotExists(rental.Id),
                CheckIfFindeksPointNotEnough(customerFindeksPoint, carFindeksPoint)
            );

            if (result != null)
            {
                return result;
            }

            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        private IResult CheckIfReturnDateNull(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate == null);
            if (result.Count > 0)
            {
                return new ErrorResult(Messages.CarIsNotReturned);
            }

            return new SuccessResult();
        }

        private IResult CheckIfRentalIdIsNotExists(int rentalId)
        {
            var result = _rentalDal.Get(r => r.Id == rentalId);
            if (result == null)
            {
                return new ErrorResult(Messages.RentalIsNotExists);
            }

            return new SuccessResult();
        }


        private IResult CheckIfFindeksPointNotEnough(decimal customerFindeksPoint, decimal carFindeksPoint)
        {
            if (carFindeksPoint > customerFindeksPoint)
            {
                return new ErrorResult("Findeks Point Not Enough");
            }

            return new SuccessResult();
        }
    }
}
