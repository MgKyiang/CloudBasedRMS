using CloudBasedRMS.Core;
using System.Collections.Generic;
using System.Linq;
namespace CloudBasedRMS.GenericRepositories
{
    public class AuthorizationsRepository : Repository<Authorizations>, IAuthorizationsRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public AuthorizationsRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
        public List<Authorizations> GetControllerAndAction()
        { 
            var data = ApplicationDbContext.Authorizations.Where(x => x.Active).ToList();
            var filterData = data.AsEnumerable().Select(x => new Authorizations()
            {
                ControllerName = x.ControllerName,
                ActionName = x.ActionName
            }).ToList();

            return filterData;
        }
    }
}
