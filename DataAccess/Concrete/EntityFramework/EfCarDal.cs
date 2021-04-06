using System;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in filter == null ? context.Cars : context.Cars.Where(filter)
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join color in context.Colors on car.ColorId equals color.Id
                    select new CarDetailDto()
                    {
                        Id = car.Id,
                        BrandName = brand.BrandName,
                        CarName = car.Description,
                        ColorName = color.ColorName,
                        DailyPrice = car.DailyPrice,
                        ModelYear = car.ModelYear,
                        ImagePaths = (
                            from carImage in context.CarImages
                            where car.Id == carImage.CarId
                            select new CarImage()
                            {
                                Id = carImage.Id,
                                CarId = carImage.CarId,
                                ImagePath = carImage.ImagePath,
                                Date = carImage.Date
                            }
                        ).ToList()
                    };

                return result.ToList();
            }
        }

        public CarDetailDto GetCarDetailsById(int id)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from car in context.Cars.Where(c => c.Id == id)
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join color in context.Colors on car.ColorId equals color.Id
                    select new CarDetailDto()
                    {
                        Id = car.Id,
                        BrandName = brand.BrandName,
                        CarName = car.Description,
                        ColorName = color.ColorName,
                        DailyPrice = car.DailyPrice,
                        ModelYear = car.ModelYear,
                        ImagePaths = (
                            from carImage in context.CarImages
                            where car.Id == carImage.CarId
                            select new CarImage()
                            {
                                Id = carImage.Id,
                                CarId = carImage.CarId,
                                ImagePath = carImage.ImagePath,
                                Date = carImage.Date
                            }
                        ).ToList()
                    };

                return result.SingleOrDefault();
            }
        }
    }
}
