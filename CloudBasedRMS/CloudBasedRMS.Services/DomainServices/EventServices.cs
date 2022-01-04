using CloudBasedRMS.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudBasedRMS.Services
{
   public class EventServices:BaseServices
    {
        public IEventRepository  Event { get; private set; }
        public EventServices()
        {
            Event = _unitOfWork.Event;
        }
    }
}
