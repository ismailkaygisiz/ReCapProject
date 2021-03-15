using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, ReCapProjectContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null)
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result =
                    from contextCustomer in filter == null ? context.Customers : context.Customers.Where(filter)
                    join contextUser in context.Users on contextCustomer.UserId equals contextUser.Id
                    select new CustomerDetailDto()
                    {
                        Id = contextCustomer.Id,
                        FirstName = contextUser.FirstName,
                        LastName = contextUser.LastName,
                        Email = contextUser.Email,
                        CompanyName = contextCustomer.Companyname
                    };

                return result.ToList();
            }
        }
    }
}
