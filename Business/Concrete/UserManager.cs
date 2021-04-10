using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System.Collections.Generic;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private ICustomerService _customerService;

        public UserManager(IUserDal userDal, ICustomerService customerService)
        {
            _userDal = userDal;
            _customerService = customerService;
        }

        [ValidationAspect(typeof(UserValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user)
        {
            _customerService.Delete(_customerService.GetByUserId(user.Id).Data);

            _userDal.Delete(user);
            return new SuccessResult();
        }


        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }


        public IDataResult<User> GetUserByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }


        public IDataResult<List<User>> GetByFirstName(string firstName)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(u => u.FirstName == firstName));
        }

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId));
        }


        public IDataResult<List<User>> GetByLastName(string lastName)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(u => u.LastName == lastName));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        [ValidationAspect(typeof(UserValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User user)
        {
            var _user = _userDal.Get(u => u.Id == user.Id);
            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            _user.Status = user.Status;

            _userDal.Update(_user);
            return new SuccessResult();
        }
    }
}
