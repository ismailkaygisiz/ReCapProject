using Business.Abstract;
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
        public IResult Add(Rental rental)
        {
            IResult result = BusinessRules.Run(
                CheckIfReturnDateNull(rental.CarId)
                );

            if (result != null)
            {
                return result;
            }

            rental.RentDate = DateTime.Now;
            _rentalDal.Add(rental);
            return new SuccessResult();
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

        [CacheAspect]
        public IDataResult<Rental> GetByCarId(int carId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.CarId == carId));
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
        public IResult Update(Rental rental)
        {
            IResult result = BusinessRules.Run(
                CheckIfRentalIdIsNotExists(rental.Id)
                );

            if (result != null)
            {
                return result;
            }

            var newRental = GetById(rental.Id).Data;
            rental.RentDate = newRental.RentDate;

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
    }
}
