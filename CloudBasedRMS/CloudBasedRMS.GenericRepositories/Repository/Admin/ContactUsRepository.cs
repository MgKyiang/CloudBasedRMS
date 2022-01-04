using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
        public ContactUsRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
