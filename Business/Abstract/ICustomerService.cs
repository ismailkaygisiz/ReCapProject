using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<Customer> GetById(int userId);
        IDataResult<List<Customer>> GetAll();
        IDataResult<List<Customer>> GetByCompanyName(string companyName);
        IResult Add(Customer customer);
        IResult Delete(Customer customer);
        IResult Update(Customer customer);
    }
}
