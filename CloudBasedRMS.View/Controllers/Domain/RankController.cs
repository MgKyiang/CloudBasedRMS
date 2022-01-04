namespace CloudBasedRMS.View.Controllers
{
    using ViewModel;
    using Core;
    using Services;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    public class RankController:ControllerAuthorizeBase
    {

        public RankServices rankServices;
        public RankController()
        {
            rankServices = new RankServices();
        }
        // GET: Rank
        public ActionResult Index()
        {
            var data = rankServices.Rank.GetByAll().Where(x => x.Active == true).ToList();
            return View(data);
        }
        // GET: Rank/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rank/Create
        [HttpPost]
        public ActionResult Create(RankViewModel viewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var checkdata = rankServices.Rank.GetByAll().Any(x => x.Code == viewmodel.Code && x.Active == true);
                    if (!checkdata)
                    {
                        Rank model = new Rank();
                        model.RankID = Guid.NewGuid().ToString();
                        model.Code = viewmodel.Code;
                        model.Description = viewmodel.Description;
                        model.Active = true;
                        model.CreatedUserID = CurrentApplicationUser.Id;
                        model.CreatedDate = DateTime.Now;
                        rankServices.Rank.Add(model);
                        rankServices.Save();
                        Success(string.Format("<b>{0}</b> was successfully saved to the system.", viewmodel.Description), true);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Warning(string.Format("<b>{0}</b> was already existed in the system.", viewmodel.Description), true);
                        return RedirectToAction("Index");

                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Rank/Edit/5
        public ActionResult Edit(string id)
        {

            if (!string.IsNullOrEmpty(id))
            {
                Rank model = rankServices.Rank.GetByID(id);
                RankViewModel viewmodel = new RankViewModel()
                {
                    RankID = model.RankID,
                    Code = model.Code,
                    Description = model.Description
                };
                return View(viewmodel);
            }
            return View();
        }

        // POST: Rank/Edit/5
        [HttpPost]
        public ActionResult Edit(RankViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                Rank model = rankServices.Rank.GetByID(viewmodel.RankID);
                model.Code = viewmodel.Code;
                model.Description = viewmodel.Description;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedUserID = CurrentApplicationUser.Id;
                rankServices.Rank.Update(model);
                rankServices.Save();
                Success(string.Format("<b>{0}</b> was successfully updated to the system.", viewmodel.Description), true);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Rank/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Rank model = rankServices.Rank.GetByID(id);
                RankViewModel viewmodel = new RankViewModel()
                {
                    RankID = model.RankID,
                    Code = model.Code,
                    Description = model.Description
                };
                return View(viewmodel);
            }
            return View();
        }

        // POST: Rank/Delete/5
        [HttpPost]
        public ActionResult Delete(RankViewModel viewmodel)
        {
            try
            {
                if (!string.IsNullOrEmpty(viewmodel.RankID))
                {
                    Rank model = rankServices.Rank.GetByID(viewmodel.RankID);
                    model.Active = false;
                    rankServices.Rank.Update(model);
                    rankServices.Save();
                    Success(string.Format("<b>{0}</b> was successfully deleted from the system.", viewmodel.Description), true);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
