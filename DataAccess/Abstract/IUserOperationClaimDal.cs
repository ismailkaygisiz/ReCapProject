using Core.DataAccess;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {
        List<OperationClaimDetailDto> GetAllOperationClaimsDetails(Expression<Func<UserOperationClaim, bool>> filter = null);
        OperationClaimDetailDto GetOperationClaimDetailsById(int id);
    }
}
