using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapProjectContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from contextRental in filter == null ? context.Rentals : context.Rentals.Where(filter)
                    join contextCar in context.Cars on contextRental.CarId equals contextCar.Id
                    join contextBrand in context.Brands on contextCar.BrandId equals contextBrand.Id
                    join contextColor in context.Colors on contextCar.ColorId equals contextColor.Id
                    join contextCustomer in context.Customers on contextRental.CustomerId equals contextCustomer.Id
                    join contextUser in context.Users on contextCustomer.UserId equals contextUser.Id
                    select new RentalDetailDto()
                    {
                        Id = contextRental.Id,
                        CarName = contextCar.Description,
                        BrandName = contextBrand.BrandName,
                        ColorName = contextColor.ColorName,
                        CustomerFullName = contextUser.FirstName + " " + contextUser.LastName,
                        RentDate = contextRental.RentDate,
                        ReturnDate = contextRental.ReturnDate
                    };
               
                return result.ToList();
            }
        }
    }
}
