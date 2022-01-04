using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.GenericRepositories
{
   public interface IUsersMemberRepository:IRepository<UsersMember>
    {
        //Here Customized Methods
        IEnumerable<UsersMember> GetByUserInMemberID(string UserInMemberID);
        UsersMember GetByUserID(string UserID);
    }
}
