using System;
using CloudBasedRMS.Core;
using System.Collections.Generic;
using System.Linq;
namespace CloudBasedRMS.GenericRepositories
{
    public   class AddressRepository:Repository<Address>,IAddressRepository
    {
        public ApplicationDbContext ApplicationDbContext{
            get { return _dbContext as ApplicationDbContext; }
        }
        public AddressRepository(ApplicationDbContext _dbContext) : base(_dbContext){
        }
        ////define Customize method
        //public Address GetCityName(string AddressID)
        //{
        //    return ApplicationDbContext.Address.Where(x => x.AddressID == AddressID).ToList();
        //}
        
    }
}
