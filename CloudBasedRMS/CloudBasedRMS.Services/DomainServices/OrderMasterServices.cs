using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudBasedRMS.GenericRepositories;
using CloudBasedRMS.Core;

namespace CloudBasedRMS.Services
{
   public class OrderMasterServices:BaseServices
    {
        public IOrderMasterRepository OrderMaster { get; private set; }
        public ITableRepository Table { get; private set; }
        public IOrderItemsRepository OrderItems { get; set; }
        public OrderMasterServices()
        {
            OrderMaster = _unitOfWork.OrderMaster;
            Table = _unitOfWork.Table;
            OrderItems = _unitOfWork.OrderItems;
        }
        public bool Insert(OrderMaster ordermaster, List<OrderItems> orderitemscollections)
        {
            try
            {
                OrderMaster.Add(ordermaster);
                OrderItems.AddRange(orderitemscollections);
                this._unitOfWork.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        public bool Update(OrderMaster ordermaster, List<OrderItems> orderitemscollections)
        {
            try
            {
                OrderMaster.Update(ordermaster, orderitemscollections);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}
