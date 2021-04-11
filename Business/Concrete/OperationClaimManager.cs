using System.Collections.Generic;
using Business.Abstract;
using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        public IResult Add(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(
                CheckIfOperationClaimNameIsExists(operationClaim.Name)
            );

            if (result != null)
            {
                return result;
            }

            _operationClaimDal.Add(operationClaim);
            return new SuccessResult();
        }

        public IResult Delete(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run();

            if (result != null)
            {
                return result;
            }

            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult();
        }

        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(
                CheckIfOperationClaimNameIsExists(operationClaim.Name)
            );

            if (result != null)
            {
                return result;
            }

            _operationClaimDal.Update(operationClaim);
            return new SuccessResult();
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(o => o.Id == id));
        }

        public IDataResult<OperationClaim> GetByName(string name)
        {
            return new SuccessDataResult<OperationClaim>(
                _operationClaimDal.Get(o => o.Name.ToLower() == name.ToLower()));
        }

        private IResult CheckIfOperationClaimNameIsExists(string name)
        {
            var result = GetByName(name).Data;

            if (result != null)
            {
                return new ErrorResult("Operation Claim Is Already Exists");
            }

            return new SuccessResult();
        }
    }
}
