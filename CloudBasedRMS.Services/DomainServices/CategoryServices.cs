using CloudBasedRMS.Core;
using CloudBasedRMS.GenericRepositories;

namespace CloudBasedRMS.Services
{
    public class CategoryServices : BaseServices
    { 
        
        //interface propteties for category repo
        public ICategoryRepository Categoryrepo { get; private set; }
        public CategoryServices()
        {
            Categoryrepo = unitOfWork.Categories;
        }
    }
}
