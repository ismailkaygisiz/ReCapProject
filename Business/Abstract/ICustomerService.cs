using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<Customer> GetByUserId(int userId);
        IDataResult<Customer> GetById(int Id);
        IDataResult<List<Customer>> GetAll();
        IDataResult<List<Customer>> GetByCompanyName(string companyName);
        IResult Add(Customer customer);
        IResult Delete(Customer customer);
        IResult Update(Customer customer);
    }
}
