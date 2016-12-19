using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QMP.BLL;
using QMP.Models;
using QMP.ViewModels;

namespace QMP.Web.Areas.Admin.Controllers
{
    [Authorize]

    public class ServiceInfosController : Controller
    {
        //
        // GET: /Admin/ServiceInfos/

        public ActionResult Add()
        {
            ViewBag.CategoryList = new SelectList(new ServiceInfos_Categorys_BLL().GetList(), "CategoryID", "CategoryName", null);
   
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ServiceInfos_ViewModel model)
        {
            AutoMapper.Mapper.CreateMap<ServiceInfos_ViewModel, ServiceInfos>();
            ServiceInfos newmodel = AutoMapper.Mapper.Map<ServiceInfos>(model);



            Users user = new Users_BLL().GetCurrentUser();
            newmodel.ServiceID = Guid.NewGuid();
            newmodel.AccountID = user.AccountID;
            newmodel.CreateTime = DateTime.Now;
            newmodel.UserID = user.UserID;
            newmodel.ReleaseTime = DateTime.Now;

            ServiceInfos_BLL bll = new ServiceInfos_BLL();


            if (bll.Add(newmodel) > 0)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "添加失败，请稍后再试！");

                return View(model);
            }

            
        }

        public ActionResult Edit(Guid id)
        {
            ViewBag.CategoryList = new SelectList(new ServiceInfos_Categorys_BLL().GetList(), "CategoryID", "CategoryName", null);
            ServiceInfos_BLL bll = new ServiceInfos_BLL();

            ServiceInfos model = bll.Get(a => a.ServiceID == id);


            AutoMapper.Mapper.CreateMap<ServiceInfos, ServiceInfos_ViewModel>();
            ServiceInfos_ViewModel newmodel = AutoMapper.Mapper.Map<ServiceInfos_ViewModel>(model);

            return View(newmodel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ServiceInfos_ViewModel model)
        {
            ServiceInfos_BLL bll = new ServiceInfos_BLL();

            ServiceInfos newmodel =bll.Get(a=>a.ServiceID==model.ServiceID);

           
            newmodel.CategoryID =model.CategoryID;
            newmodel.Content =model.Content;
            newmodel.Title =model.Title;
           

            if (bll.Update(newmodel) > 0)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "添加失败，请稍后再试！");
                ViewBag.CategoryList = new SelectList(new ServiceInfos_Categorys_BLL().GetList(), "CategoryID", "CategoryName", null);

                return View(model);
            }


        }

        public ActionResult Delete(Guid id)
        {
            ServiceInfos_BLL bll = new ServiceInfos_BLL();
            bll.Delete(a => a.ServiceID == id);
            return RedirectToAction("List");
        }

        public ActionResult List(int id = 1)
        {
            Users user = new Users_BLL().GetCurrentUser();
            ServiceInfos_BLL bll = new ServiceInfos_BLL();
            int totalCount = bll.GetCount(a => a.AccountID == user.AccountID);
            List<ServiceInfos> mlist = bll.GetPageListOrderBy(id, 20, a => a.AccountID == user.AccountID, a => a.CreateTime).ToList();



            var list = new StaticPagedList<ServiceInfos>(mlist, id, 20, totalCount);
            return View(list);
        }

        public ActionResult Details(Guid id)
        {
                  ServiceInfos_BLL bll = new ServiceInfos_BLL();

            ServiceInfos model = bll.Get(a => a.ServiceID == id);


            return View(model);

        }

    }
}
