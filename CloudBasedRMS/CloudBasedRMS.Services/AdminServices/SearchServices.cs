using CloudBasedRMS.Core;
using CloudBasedRMS.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Services.AdminServices
{
 public class SearchServices: BaseServices
    {
        public ICategoryRepository Categoryrepo { get; private set; }
        
        public SearchServices()
        {
            Categoryrepo = _unitOfWork.Categories;
        }
        public SearchResults Find(string item)
        {
            SearchResults SR = new SearchResults();
            SR.Suggestions.Add(item);
            return SR;
        }
    }
}
