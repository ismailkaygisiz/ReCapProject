using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, ReCapProjectContext>,
        IUserOperationClaimDal
    {
        public List<OperationClaimDetailDto> GetAllOperationClaimsDetails(
            Expression<Func<UserOperationClaim, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from userOperationClaim in filter == null
                        ? context.UserOperationClaims
                        : context.UserOperationClaims.Where(filter)
                    join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals
                        operationClaim.Id
                    join user in context.Users on userOperationClaim.UserId equals user.Id
                    select new OperationClaimDetailDto()
                    {
                        Id = userOperationClaim.Id,
                        Claim = operationClaim.Name,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    };

                return result.ToList();
            }
        }

        public OperationClaimDetailDto GetOperationClaimDetailsById(int id)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from userOperationClaim in context.UserOperationClaims
                    join operationClaim in context.OperationClaims on userOperationClaim.OperationClaimId equals
                        operationClaim.Id
                    join user in context.Users on userOperationClaim.UserId equals user.Id
                    where id == userOperationClaim.Id
                    select new OperationClaimDetailDto()
                    {
                        Id = userOperationClaim.Id,
                        Claim = operationClaim.Name,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    };

                return result.SingleOrDefault();
            }
        }
    }
}
