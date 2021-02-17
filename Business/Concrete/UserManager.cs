using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult();
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<List<User>> GetByEmail(string email)
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(u => u.Email == email));
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

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }
    }
}
