using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<User> GetById(int userId);
        IDataResult<List<User>> GetAll();
        IDataResult<List<User>> GetByFirstName(string firstName);
        IDataResult<List<User>> GetByLastName(string lastName);
        IDataResult<List<User>> GetByEmail(string email);
        IResult Add(User user);
        IResult Delete(User user);
        IResult Update(User user);
    }
}
