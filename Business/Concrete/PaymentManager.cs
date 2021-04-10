using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        public IDataResult<List<Payment>> GetPaymentsByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll(p => p.CustomerId == customerId));
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IDataResult<Payment> GetPaymentById(int id)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(p => p.Id == id));
        }

        public IResult Add(Payment payment)
        {
            _paymentDal.Add(payment);
            return new SuccessResult();;
        }

        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult();;
        }

        public IResult Update(Payment payment)
        {
            _paymentDal.Update(payment);
            return new SuccessResult();;
        }

        public IResult Pay(Payment payment)
        {
            return new SuccessResult("Ödeme Tamamlandı");
        }
    }
}
