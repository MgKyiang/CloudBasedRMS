using CloudBasedRMS.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Services
{
   public class UsersMemberServices:BaseServices
    {
        //Create instance of Interface for ApplicationSettings
        public IUsersMemberRepository UsersMember { get; private set; }
        public UsersMemberServices()
        {
            UsersMember = unitOfWork.UsersMember;
        }
        //Custimization Code Here
    }
}
