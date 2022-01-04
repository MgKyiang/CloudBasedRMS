using CloudBasedRMS.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Services.AdminServices
{
  public  class APIAuthorizationServices :BaseServices
    {
        public IAPIAuthorizationRepository APIAuthorizations { get; private set; }
        public APIAuthorizationServices()
        {
            APIAuthorizations = unitOfWork.APIAuthorization;
        }

        
    }
}
