using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>()
            {
                new Car{Id = 1, BrandId = 1, ColorId = 1, DailyPrice= 1000, Description = "Dodge Challenger", ModelYear = 2019},
                new Car{Id = 2, BrandId = 1, ColorId = 2, DailyPrice= 1200, Description = "BMW 5.25 X Drive", ModelYear = 2019},
                new Car{Id = 3, BrandId = 2, ColorId = 3, DailyPrice= 2000, Description = "Audi RS8", ModelYear = 2019},
                new Car{Id = 4, BrandId = 2, ColorId = 2, DailyPrice= 2200, Description = "Mercedes AMG A180", ModelYear = 2020},
                new Car{Id = 5, BrandId = 3, ColorId = 5, DailyPrice= 4000, Description = "Porsche Panamera 4", ModelYear = 2020},
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetById(int brandId)
        {
            return _cars.Where(c => c.BrandId == brandId).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
        }
    }
}
