using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Business.Abstract;
using Core.Business;
using Core.Entities.Concrete;
using Core.Entities.DTOs;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public IResult Add(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run(
                CheckIfClaimAlreadyExists(userOperationClaim.UserId,
                    userOperationClaim.OperationClaimId)
            );

            if (result != null)
            {
                return result;
            }

            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult();
        }

        public IResult Delete(UserOperationClaim userOperationClaim)
        {
            IResult result = BusinessRules.Run();

            if (result != null)
            {
                return result;
            }

            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult();
        }

        public IResult Update(UserOperationClaim userOperationClaim)
        {
            IResult result =
                BusinessRules.Run(
                    CheckIfClaimAlreadyExists(userOperationClaim.UserId,
                        userOperationClaim.OperationClaimId)
                );

            if (result != null)
            {
                return result;
            }

            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult();
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<UserOperationClaim>>(_userOperationClaimDal.GetAll());
        }

        public IDataResult<List<OperationClaimDetailDto>> GetAllOperationClaimsDetails()
        {
            return new SuccessDataResult<List<OperationClaimDetailDto>>(_userOperationClaimDal
                .GetAllOperationClaimsDetails());
        }

        public IDataResult<List<OperationClaimDetailDto>> GetOperationClaimsDetailsByUserId(int userId)
        {
            return new SuccessDataResult<List<OperationClaimDetailDto>>(
                _userOperationClaimDal.GetAllOperationClaimsDetails(u => u.UserId == userId));
        }

        public IDataResult<List<OperationClaimDetailDto>> GetOperationClaimsDetailsByClaimId(int claimId)
        {
            return new SuccessDataResult<List<OperationClaimDetailDto>>(
                _userOperationClaimDal.GetAllOperationClaimsDetails(u => u.OperationClaimId == claimId));
        }

        public IDataResult<OperationClaimDetailDto> GetOperationClaimDetailsById(int id)
        {
            return new SuccessDataResult<OperationClaimDetailDto>(
                _userOperationClaimDal.GetOperationClaimDetailsById(id));
        }

        public IDataResult<List<UserOperationClaim>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(
                _userOperationClaimDal.GetAll(u => u.UserId == userId));
        }

        public IDataResult<List<UserOperationClaim>> GetByClaimId(int claimId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(
                _userOperationClaimDal.GetAll(u => u.OperationClaimId == claimId));
        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {
            return new SuccessDataResult<UserOperationClaim>(_userOperationClaimDal.Get(u => u.Id == id));
        }

        private IResult CheckIfClaimAlreadyExists(int userId, int operationClaimId)
        {
            var result = GetByUserId(userId).Data;
            foreach (var userOperationClaim in result)
            {
                if (userOperationClaim.OperationClaimId == operationClaimId)
                {
                    return new ErrorResult("Claim Already Exists");
                }
            }

            return new SuccessResult();
        }
    }
}
