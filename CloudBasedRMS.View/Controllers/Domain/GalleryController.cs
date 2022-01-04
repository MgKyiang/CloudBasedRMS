using CloudBasedRMS.View.Controllers.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CloudBasedRMS.View.Controllers
{
 public   class GalleryController:ControllerAuthorizeBase
    {
        public ActionResult Index(GalleryViewModel obj)
        {
            obj.Images = Directory.EnumerateFiles(Server.MapPath("~/uploadeimg/"))
                .Select(fn => "~/uploadeimg/" + Path.GetFileName(fn));
            var objfile = new DirectoryInfo(Server.MapPath("~/uploadeimg/"));
            var file = objfile.GetFiles("*.*");
            return View(obj);
        }

        #region Upload Drag & Drop
        [HttpPost]
        public ActionResult Upload()
        {
            bool isSavedSuccessfully = true;
            string fname = "";
            try
            {
                foreach (string filename in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[filename];
                    fname = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/uploadeimg"));
                        string pathstring = Path.Combine(path.ToString());
                        string filename1 = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        bool isexist = Directory.Exists(pathstring);
                        if (!isexist)
                        {
                            Directory.CreateDirectory(pathstring);
                        }
                        string uploadpath = string.Format("{0}\\{1}", pathstring, filename1);
                        file.SaveAs(uploadpath);
                    }
                }
            }
            catch (Exception)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
             return Json(new {Message = fname });
            }
            else
            {
              return Json(new{Message = "Error in Saving file" });
            }
        }
        #endregion
    }
}
