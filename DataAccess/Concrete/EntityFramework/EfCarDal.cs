using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Cars
                             join brand in context.Brands on car.BrandId equals brand.Id into temp1
                             from t1 in temp1.DefaultIfEmpty()
                             join color in context.Colors on car.ColorId equals color.Id into temp2
                             from t2 in temp2.DefaultIfEmpty()
                             select new CarDetailDto
                             {
                                 Id = car.Id,
                                 BrandName = t1.BrandName,
                                 CarName = car.Description,
                                 ColorName = t2.ColorName,
                                 DailyPrice = car.DailyPrice,
                                 ModelYear = car.ModelYear
                             };

                return result.ToList();
            }
        }
    }
}
