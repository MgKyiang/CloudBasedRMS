using System;
using CloudBasedRMS.GenericRepositories;

namespace CloudBasedRMS.Services
{
  public  class RankServices:BaseServices
    {
        public IRankRepository   Rank { get; set; }
        public RankServices()
        {
            Rank = unitOfWork.Rank;
        }
    }
}
