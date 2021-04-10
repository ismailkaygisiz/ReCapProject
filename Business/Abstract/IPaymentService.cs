using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IDataResult<List<Payment>> GetPaymentsByCustomerId(int customerId);
        IDataResult<List<Payment>> GetAll();
        IDataResult<Payment> GetPaymentById(int id);
        IResult Add(Payment payment);
        IResult Delete(Payment payment);
        IResult Update(Payment payment);
        IResult Pay(Payment payment);
    }
}
