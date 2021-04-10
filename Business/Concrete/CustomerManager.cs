using System;
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
using System.Linq;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;
        private IRentalService _rentalService;
        private IPaymentService _paymentService;

        public CustomerManager(ICustomerDal customerDal, IRentalService rentalService, IPaymentService paymentService)
        {
            _customerDal = customerDal;
            _rentalService = rentalService;
            _paymentService = paymentService;
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
        }

        [ValidationAspect(typeof(CustomerValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Add(Customer customer)
        {
            IResult result = BusinessRules.Run(
                CheckIfFindeksPointGraterThanValue(customer),
                CheckIfFindeksPointEqualsZero(customer)
            );

            if (result != null)
            {
                return result;
            }

            _customerDal.Add(customer);
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Delete(Customer customer)
        {
            IResult result = BusinessRules.Run(
                CheckIfCustomerIdIsNotExists(customer.Id)
            );

            if (result != null)
            {
                return result;
            }

            // This action deletes the rentals of the customer's
            _rentalService.GetByCustomerId(customer.Id).Data.ForEach(r => _rentalService.Delete(r));
            _paymentService.GetPaymentsByCustomerId(customer.Id).Data.ForEach(p => _paymentService.Delete(p));

            _customerDal.Delete(customer);
            return new SuccessResult();
        }

        [CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<Customer>> GetByCompanyName(string companyName)
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(c => c.CompanyName == companyName));
        }

        [CacheAspect]
        public IDataResult<CustomerDetailDto> GetById(int id)
        {
            return new SuccessDataResult<CustomerDetailDto>(_customerDal.GetCustomerDetails(c => c.Id == id)
                .SingleOrDefault());
        }

        [CacheAspect]
        public IDataResult<Customer> GetByUserId(int userId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == userId));
        }

        [ValidationAspect(typeof(CustomerValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer)
        {
            IResult result = BusinessRules.Run(
                CheckIfCustomerIdIsNotExists(customer.Id),
                CheckIfFindeksPointGraterThanValue(customer),
                CheckIfFindeksPointEqualsZero(customer)
            );

            if (result != null)
            {
                return result;
            }

            _customerDal.Update(customer);
            return new SuccessResult();
        }

        public IResult IncreaseFindeksPoint(Customer customer, decimal carFindeksPoint)
        {
            var _customer = _customerDal.Get(c=>c.Id == customer.Id);
            if (_customer.FindeksPoint < carFindeksPoint)
            {
                return new ErrorResult("Findeks Point Not Enough");
            }

            _customer.FindeksPoint += carFindeksPoint+ (carFindeksPoint/2);

            Update(_customer);
            return new SuccessResult();
        }

        private IResult CheckIfCustomerIdIsNotExists(int customerId)
        {
            var result = _customerDal.Get(c => c.Id == customerId);
            if (result == null)
            {
                return new ErrorResult(Messages.CustomerIsNotExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfFindeksPointEqualsZero(Customer customer)
        {
            if (customer.FindeksPoint <= 0)
            {
                customer.FindeksPoint = new Random().Next(150, 350);
            }

            return new SuccessResult();
        }

        private IResult CheckIfFindeksPointGraterThanValue(Customer customer)
        {
            if (customer.FindeksPoint >= 1900)
            {
                customer.FindeksPoint = 1900;
            }

            return new SuccessResult();
        }
    }
}
