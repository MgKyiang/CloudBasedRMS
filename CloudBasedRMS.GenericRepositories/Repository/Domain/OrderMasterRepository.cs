using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace CloudBasedRMS.GenericRepositories
{
    public  class OrderMasterRepository:Repository<OrderMaster>,IOrderMasterRepository
    {
        public ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext as ApplicationDbContext; }
        }
        public OrderMasterRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
        }
        //define Customize method
        public bool Update(OrderMaster ordermasterviewmodel, List<OrderItems> orderitemsviewmodel)
        {
            try
            {
                OrderMaster ordermastermodel = ApplicationDbContext.OrderMaster.Find(ordermasterviewmodel.OrderMasterID);
                ordermastermodel.OrderNo = ordermasterviewmodel.OrderNo;
                ordermastermodel.OrderDate = ordermasterviewmodel.OrderDate;
                ordermastermodel.TableID = ordermasterviewmodel.TableID;
                ordermastermodel.IsParcel = ordermasterviewmodel.IsParcel;
                ordermastermodel.Description = ordermasterviewmodel.Description;
                ordermastermodel.UpdatedDate = ordermasterviewmodel.UpdatedDate;
                ordermastermodel.UpdatedUserID = ordermasterviewmodel.UpdatedUserID;
                ApplicationDbContext.Entry(ordermastermodel).State = EntityState.Modified;
                ApplicationDbContext.SaveChanges();
                List<OrderItems> orderitemslist = ApplicationDbContext.OrderItems.Where(x => x.OrderMasterID == ordermasterviewmodel.OrderMasterID && x.Active == true).ToList();
                foreach (OrderItems item in orderitemslist)
                {
                    item.Active = true;
                    ApplicationDbContext.Entry(item).State = EntityState.Modified;
                    ApplicationDbContext.SaveChanges();
                }
                ApplicationDbContext.OrderItems.AddRange(orderitemslist);
                ApplicationDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {             
                return false;
                throw ex;
            }
        }
        public List<OrderMaster> GetByOrderNo(string OrderNo)
        {
           return ApplicationDbContext.OrderMaster.Where(x => x.OrderNo == OrderNo && x.Active == true).ToList();
        }
    }
}
