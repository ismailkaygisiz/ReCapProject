using System.Collections.Generic;
using Core.Entities.Abstract;
using Entities.Concrete;

namespace Entities.DTOs
{
    public class CarDetailDto : IDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
        public int ModelYear { get; set; }
        public List<CarImage> ImagePaths { get; set; }
        public decimal FindeksPoint { get; set; }
    }
}
