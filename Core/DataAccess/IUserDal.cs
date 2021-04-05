using Core.DataAccess;
using Core.Entities.Concrete;
using System.Collections.Generic;

namespace Core.DataAccess
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
