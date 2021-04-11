using Core.Entities.Abstract;
namespace Core.Entities.DTOs
{
    public class OperationClaimDetailDto : IDto
    {
        public int Id { get; set; }
        public string Claim { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
