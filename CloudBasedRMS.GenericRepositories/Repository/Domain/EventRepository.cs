using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
 public   class EventRepository:Repository<Event>,IEventRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
        public EventRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
