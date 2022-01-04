using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CloudBasedRMS.View.Controllers.Common
{
    public class GenericReportViewerController : ControllerBase
    {
        public void ShowSaleBillBySaleDate()
        {
            try
            {
                bool isValid = true;
                string ReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                DateTime FromDate =Convert.ToDateTime( System.Web.HttpContext.Current.Session["rptFromDate"]);// Setting FromDate 
                DateTime ToDate =Convert.ToDateTime( System.Web.HttpContext.Current.Session["rptToDate"]);         // Setting ToDate    
                var rptSource = System.Web.HttpContext.Current.Session["rptSource"];
                if (string.IsNullOrEmpty(ReportName))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string RptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Report//" + ReportName;
                    rd.Load(RptPath);
                    if (rptSource != null && rptSource.GetType().ToString() != "System.String")
                        rd.SetDataSource(rptSource);
                    if (FromDate!=DateTime.MinValue)
                        rd.SetParameterValue("SaleBillFromDate", FromDate);
                    if (ToDate!=DateTime.MinValue)
                        rd.SetParameterValue("SaleBillToDate", ToDate);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "SaleBillReport");
                    // Clear all sessions value
                    Session["ReportName"] = null;
                    Session["rptFromDate"] = null;
                    Session["rptToDate"] = null;
                    Session["rptSource"] = null;
                }
                else
                {
                    Response.Write("<H2>Nothing Found; No Report name found</H2>");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        public void showEmployeeReport()
        {
            try
            {
                bool isValid = true;
                string ReportName = System.Web.HttpContext.Current.Session["ReportName"].ToString();    // Setting ReportName
                string RankID =System.Web.HttpContext.Current.Session["rptRankID"].ToString();// Setting rptRankID 
                var rptSource = System.Web.HttpContext.Current.Session["rptSource"];

                if (string.IsNullOrEmpty(ReportName))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    ReportDocument rd = new ReportDocument();
                    string RptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Report//" + ReportName;
                    rd.Load(RptPath);
                    if (rptSource != null && rptSource.GetType().ToString() != "System.String")
                        rd.SetDataSource(rptSource);
                    if (!string.IsNullOrEmpty(RankID))
                        rd.SetParameterValue("Rank", RankID);
                    rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "EmployeeReport");
                    // Clear all sessions value
                    Session["ReportName"] = null;
                    Session["rptRankID"] = null;
                    Session["rptSource"] = null;
                }
                else
                {
                    Response.Write("<H2>Nothing Found; No Report name found</H2>");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}