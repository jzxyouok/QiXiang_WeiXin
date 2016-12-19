using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using QMP.BLL;
using QMP.Models;
using QMP.ViewModels;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace QMP.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ShortNearInfosController : Controller
    {
        //
        // GET: /Admin/ShortNearInfos/

        public ActionResult Add()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "未来3小时", Value = "3" });
            list.Add(new SelectListItem() { Text = "未来6小时", Value = "6" });
            list.Add(new SelectListItem() { Text = "未来12小时", Value = "12" });
            ViewBag.DuringHourList = new SelectList(list, "Value", "Text", false);
            return View();
        }
        [HttpPost]
        public ActionResult Add(ShortNearInfos_ViewModel model)
        {
            Users user = new Users_BLL().GetCurrentUser();
            model.UserID = user.UserID;
            model.InfoID = Guid.NewGuid();
            model.AccountID = user.AccountID;
           
            model.EndTime =((DateTime) model.StartTime).AddHours((int)model.DuringHour);
            model.CreateTime = DateTime.Now;
            AutoMapper.Mapper.CreateMap<ShortNearInfos_ViewModel, ShortNearInfos>();
            ShortNearInfos newmodel = AutoMapper.Mapper.Map<ShortNearInfos>(model);


            ShortNearInfos_BLL bll = new ShortNearInfos_BLL();
            bll.Add(newmodel);
         

            string appid = user.OfficialAccounts.AppID;
            if (!AccessTokenContainer.CheckRegistered(appid)) //检查是否已经注册
            {
                AccessTokenContainer.Register(appid, user.OfficialAccounts.AppSecret); //如果没有注册则进行注册
            }

            OpenIdResultJson users = UserApi.Get(appid, null);
            OpenIdResultJson_Data aa = users.data;
            int chenggong = 0;
            int shibai = 0;
            foreach (string openid in aa.openid)
            {
                try
                {

                    //List<Article> alist = new List<Article>();
                    //var article1 = new Article()
                    //{
                    //    Title =  ((DateTime)model.StartTime).ToString("H时m分发布短时临近预报"),
                    //    Description = model.Content,
                    //    PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/dslj.jpg",
                    //    //Url = "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id=" +
                    //    //            model.InfoID.ToString()
                    //};

                    //alist.Add(article1);
                    List<Article> alist = new List<Article>();
                    var article1 = new Article()
                    {
                        //Title = user.OfficialAccounts.CompanyName + ((DateTime)model.StartTime).ToString("M月d日H时m分发布短时临近预报"),
                        Title =  ((DateTime)model.StartTime).ToString("M月d日H时m分发布短时临近预报"),
                        //Description = model.Content,
                        PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/" + user.OfficialAccounts.ShortNearImg,
                        Url =
                            "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id=" +
                            model.InfoID.ToString(),
                    };
                    var article2 = new Article()
                    {
                        Title = model.Content,
                        //Description = model.Content,
                        //PicUrl = "Content/images/article/dqyb.jpg",
                        Url =
                            "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id=" +
                            model.InfoID.ToString(),
                    };
                    var article3 = new Article()
                    {
                        Title = "详情",
                        //Description = model.Content,
                        //PicUrl = "Content/images/article/dqyb.jpg",
                        Url =
                            "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id=" +
                            model.InfoID.ToString(),
                    };
                    alist.Add(article1);
                    alist.Add(article2);
                    alist.Add(article3);

                    //        CustomApi.SendNews(account.AppID, requestMessage.FromUserName, alist);
                   
                    CustomApi.SendNews(appid, openid, alist);

                    chenggong++;
                }
                catch (Exception ex)
                {
                    shibai++;
                   
                }
            }




            Messages_BLL msgbll = new Messages_BLL();
            Messages msg = new Messages();
            msg.MsgID = Guid.NewGuid();
            msg.SendTime = DateTime.Now;
            msg.Title = user.OfficialAccounts.CompanyName + ((DateTime)model.StartTime).ToString("yyyy年M月d日H时m分") + "发布短时临近预报";

            msg.SendSuccessCount = chenggong;
            msg.SendFailedCount = shibai;
            msg.SendTotalCount = aa.openid.Count();
            msg.AccountID = user.AccountID;
            msg.Content = newmodel.Content;
            msg.CreateTime = DateTime.Now;
            Users currentuser = new Users_BLL().Get(a => a.UserName == User.Identity.Name);
            msg.UserID = currentuser.UserID;
            msg.CategoryID = 3;
            msgbll.Add(msg);


            //Thread.Sleep(5000);
            // return Content(chenggong.ToString());

            ShortNearSendSussess_ViewModel svm = new ShortNearSendSussess_ViewModel();

            svm.TotalCount = msg.SendTotalCount;
            svm.SuccessCount = msg.SendSuccessCount;
            svm.FailedCount = msg.SendFailedCount;

            svm.Title = ((DateTime)model.StartTime).ToString("yyyy年M月d日H时m分") + "发布短时临近预报";

            return PartialView("_AddSuccess", svm);

        }

        public ActionResult List()
        {
            Users user = new Users_BLL().GetCurrentUser();
            ShortNearInfos_BLL bll = new ShortNearInfos_BLL();
            List<ShortNearInfos> list = bll.GetList(a => a.AccountID == user.AccountID).OrderByDescending(a=>a.CreateTime).ToList();

            AutoMapper.Mapper.CreateMap<ShortNearInfos, ShortNearInfos_ViewModel>();
            List<ShortNearInfos_ViewModel> newmodel = AutoMapper.Mapper.Map<List<ShortNearInfos_ViewModel>>(list);
            return View(newmodel);
        }

        public ActionResult Delete(Guid id)
        {
            ShortNearInfos_BLL bll = new ShortNearInfos_BLL();
            if (bll.Delete(a => a.InfoID == id) > 0)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("List");

            }
        }

        public ActionResult Edit(Guid id)
        {
            ShortNearInfos_BLL bll = new ShortNearInfos_BLL();
            ShortNearInfos model = bll.Get(a => a.InfoID == id);

            AutoMapper.Mapper.CreateMap<ShortNearInfos, ShortNearInfos_ViewModel>();
            ShortNearInfos_ViewModel vmodel = AutoMapper.Mapper.Map<ShortNearInfos_ViewModel>(model);


            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem() { Text = "未来3小时", Value = "3" });
            list.Add(new SelectListItem() { Text = "未来6小时", Value = "6" });

            ViewBag.DuringHourList = new SelectList(list, "Value", "Text", false);

            return View(vmodel);

        }
        [HttpPost]
        public ActionResult Edit(ShortNearInfos_ViewModel vmodel)
        {




            ShortNearInfos_BLL bll = new ShortNearInfos_BLL();
            ShortNearInfos model = bll.Get(a => a.InfoID == vmodel.InfoID);
            model.Content = vmodel.Content;
            model.DuringHour = vmodel.DuringHour;
            model.StartTime = vmodel.StartTime;
           
            if (bll.Update(model) > 0)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "修改失败，请稍后再试！");
                List<SelectListItem> list = new List<SelectListItem>();

                list.Add(new SelectListItem() { Text = "未来3小时", Value = "3" });
                list.Add(new SelectListItem() { Text = "未来6小时", Value = "6" });

                ViewBag.DuringHourList = new SelectList(list, "Value", "Text", false);

                return View(model);
            }

        }

    }
}
