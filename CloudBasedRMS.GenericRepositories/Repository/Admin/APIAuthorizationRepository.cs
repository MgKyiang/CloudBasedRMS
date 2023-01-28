using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
 public   class APIAuthorizationRepository : Repository<APIAuthorization>, IAPIAuthorizationRepository
    {
        public ApplicationDbContext AppDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public APIAuthorizationRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
