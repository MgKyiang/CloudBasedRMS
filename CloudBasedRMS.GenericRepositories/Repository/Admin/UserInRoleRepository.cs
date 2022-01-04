using CloudBasedRMS.Core;

namespace CloudBasedRMS.GenericRepositories
{
    public class UserInRoleRepository : Repository<UserInRole>, IUserInRoleRepository
    {
        public UserInRoleRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
    }
}
