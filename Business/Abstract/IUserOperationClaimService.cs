using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim userOperationClaim);
        IResult Delete(UserOperationClaim userOperationClaim);
        IResult Update(UserOperationClaim userOperationClaim);
        IDataResult<List<UserOperationClaim>> GetAll();
        IDataResult<List<UserOperationClaim>> GetByUserId(int userId);
        IDataResult<List<UserOperationClaim>> GetByClaimId(int claimId);
        IDataResult<UserOperationClaim> GetById(int id);
        IDataResult<List<OperationClaimDetailDto>> GetAllOperationClaimsDetails();
        IDataResult<List<OperationClaimDetailDto>> GetOperationClaimsDetailsByUserId(int userId);
        IDataResult<List<OperationClaimDetailDto>> GetOperationClaimsDetailsByClaimId(int claimId);
        IDataResult<OperationClaimDetailDto> GetOperationClaimDetailsById(int id);
    }
}
