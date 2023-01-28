using CloudBasedRMS.Core;

namespace CloudBasedRMS.GenericRepositories
{
    public  class CategoryRepository: Repository<Category>,ICategoryRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public CategoryRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
    }
}
