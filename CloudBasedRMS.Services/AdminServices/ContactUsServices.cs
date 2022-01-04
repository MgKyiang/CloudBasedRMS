using CloudBasedRMS.Core;
using CloudBasedRMS.GenericRepositories;

namespace CloudBasedRMS.Services.AdminServices
{
 public   class ContactUsServices:BaseServices
    {
        public IContactUsRepository ContactUs { get; set; }
        public ContactUsServices()
        {
            ContactUs = unitOfWork.ContactUs;
        }
    }
}
