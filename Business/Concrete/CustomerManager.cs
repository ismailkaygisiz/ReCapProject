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
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            IResult result = BusinessRules.Run(

                );

            if (result != null)
            {
                return result;
            }

            _customerDal.Add(customer);
            return new SuccessResult();
        }

        public IResult Delete(Customer customer)
        {
            IResult result = BusinessRules.Run(
                CheckIfCustomerIdIsNotExists(customer.Id)
                );

            if (result != null)
            {
                return result;
            }

            _customerDal.Delete(customer);
            return new SuccessResult();
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IDataResult<List<Customer>> GetByCompanyName(string companyName)
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(c => c.Companyname == companyName));
        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == id));
        }

        public IDataResult<Customer> GetByUserId(int userId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == userId));
        }

        public IResult Update(Customer customer)
        {
            IResult result = BusinessRules.Run(
                CheckIfCustomerIdIsNotExists(customer.Id)
                );

            if (result != null)
            {
                return result;
            }

            _customerDal.Update(customer);
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
    }
}
