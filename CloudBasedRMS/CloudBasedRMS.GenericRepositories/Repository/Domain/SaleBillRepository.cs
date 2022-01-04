﻿using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
   public class SaleBillRepository:Repository<SaleBill>,ISaleBillRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return dbContext as ApplicationDbContext; }
        }
        public SaleBillRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
    }
}
