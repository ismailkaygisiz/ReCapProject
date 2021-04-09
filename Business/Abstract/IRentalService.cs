using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<Rental> GetById(int rentalId);
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<Rental>> GetByCarId(int carId);
        IDataResult<List<Rental>> GetByCustomerId(int customerId);
        IDataResult<List<Rental>> GetByRentDate(DateTime rentDate);
        IDataResult<List<Rental>> GetByReturnDate(DateTime returnDate);
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IResult Add(Rental rental, decimal customerFindeksPoint, decimal carFindeksPoint);
        IResult Delete(Rental rental);
        IResult Update(Rental rental, decimal customerFindeksPoint, decimal carFindeksPoint);
    }
}
