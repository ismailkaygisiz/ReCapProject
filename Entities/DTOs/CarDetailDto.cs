using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class CarDetailDto : IDto
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
        public int ModelYear { get; set; }
        public string ImagePath { get; set; }
    }
}
