using System;
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
    public class MessagesController : Controller
    {
        //
        // GET: /Admin/Messages/

        public ActionResult List(int id = 1)
        {
            Users user = new Users_BLL().GetCurrentUser();
            Messages_BLL bll = new Messages_BLL();
            int totalCount = bll.GetCount(a => a.AccountID == user.AccountID);
            List<Messages> mlist =
                bll.GetPageListOrderBy(id, 20, a => a.AccountID == user.AccountID, a => a.SendTime,false).ToList();




            AutoMapper.Mapper.CreateMap<Messages, Messages_ViewModel>();
            List<Messages_ViewModel> newmodel = AutoMapper.Mapper.Map<List<Messages_ViewModel>>(mlist);


            foreach (Messages_ViewModel item in newmodel)
            {
                item.SendSuccessPercent = (Math.Round(Convert.ToDouble(item.SendSuccessCount) / Convert.ToDouble(item.SendTotalCount), 2) * 100).ToString() + "%";
                item.SendFailedPercent = (Math.Round(Convert.ToDouble(item.SendFailedCount) / Convert.ToDouble(item.SendTotalCount), 2) * 100).ToString() + "%";
            }



            var list = new StaticPagedList<Messages_ViewModel>(newmodel, id, 20, totalCount);
            return View(list);
        }



    }
}
