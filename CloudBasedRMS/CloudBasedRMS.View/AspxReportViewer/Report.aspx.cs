using CloudBasedRMS.Services;
using CloudBasedRMS.View.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace CloudBasedRMS.View.Views.AspxReportViewer
{
    public partial class Report : System.Web.UI.Page
    {
        private EmployeeServices employeeServices = new EmployeeServices();
        protected void Page_Load(object sender, EventArgs e)
        {
            CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            var data = employeeServices.Employee.GetByAll().Where(x => x.Active==true).ToList();
            Employee emp = new Employee();
            emp.SetDataSource(data.Select(p => new { EmployeeNo = p.EmployeeNo, EmployeeName = p.Name, DOB = p.DOB, Email = p.NRC, PhoneNo = p.PhoneNo, Rank = p.Rank.Description, Address = p.Address.City }).ToList());
            //emp.SetDataSource(data.Select(p => new { EmployeeNo = p.EmployeeNo, EmployeeName = p.Name, DOB = p.DOB, Email = p.NRC, PhoneNo = p.PhoneNo, Rank = p.Rank.Description, Address = p.Address.City }).ToList());
            CrystalReportViewer1.ReportSource = emp;
            CrystalReportViewer1.RefreshReport();
        }
    }
}