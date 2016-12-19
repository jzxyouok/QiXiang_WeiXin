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
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace QMP.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class WeatherInfosController : Controller
    {
        //
        // GET: /Admin/WeatherInfos/

        public ActionResult Add()
        {


            //List<WeatherInfos_Categorys> WeatherCategoryList = new WeatherInfos_Categorys_BLL().GetList().ToList();
            //ViewBag.WeatherCategoryList = new SelectList(WeatherCategoryList, "CategoryID", "CategoryName", false);


            return View();
        }
        [HttpPost]
        public ActionResult Add(WeatherInfos_ViewModel model)
        {
            Users user = new Users_BLL().GetCurrentUser();
            model.UserID = user.UserID;
            model.InfoID = Guid.NewGuid();

            model.AccountID = user.AccountID;
            
            model.CategoryID = 1;
            AutoMapper.Mapper.CreateMap<WeatherInfos_ViewModel, WeatherInfos>();
            WeatherInfos newmodel = AutoMapper.Mapper.Map<WeatherInfos>(model);


            WeatherInfos_BLL bll = new WeatherInfos_BLL();
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
                    //    Title = ((DateTime)model.CreateTime).ToString("H时m分发布三天预报"),
                    //    Description = model.Content,
                    //    PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/dqyb.jpg"
                        
                    //};

                    //alist.Add(article1);




                    List<Article> alist = new List<Article>();
                    var article1 = new Article()
                    {
                        //Title = user.OfficialAccounts.CompanyName + ((DateTime)model.CreateTime).ToString("M月d日H时m分发布三天预报"),
                        Title = user.OfficialAccounts.CompanyName+((DateTime)model.CreateTime).ToString("M月d日H时m分发布三天预报"),
                        //Description = model.Content,
                        PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/"+ user.OfficialAccounts.SanTianImg,
                        Url = ""
                    };
                    var article2 = new Article()
                    {
                        Title = model.Content,
                        //Description = model.Content,
                        //PicUrl = "Content/images/article/dqyb.jpg",
                        Url = ""
                    };

                    alist.Add(article1);
                    alist.Add(article2);

                    
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
            msg.Title = user.OfficialAccounts.CompanyName + ((DateTime)model.CreateTime).ToString("yyyy年M月d日H时m分") + "发布三天预报";

            msg.SendSuccessCount = chenggong;
            msg.SendFailedCount = shibai;
            msg.SendTotalCount = aa.openid.Count();
            msg.AccountID = user.AccountID;
            msg.Content = newmodel.Content;
            msg.CreateTime = DateTime.Now;
            Users currentuser = new Users_BLL().Get(a => a.UserName == User.Identity.Name);
            msg.UserID = currentuser.UserID;
            msg.CategoryID =1;
            msgbll.Add(msg);


           // Thread.Sleep(5000);
            // return Content(chenggong.ToString());

            WeatherSendSussess_ViewModel svm = new WeatherSendSussess_ViewModel();

            svm.TotalCount = msg.SendTotalCount;
            svm.SuccessCount = msg.SendSuccessCount;
            svm.FailedCount = msg.SendFailedCount;

            svm.Title = ((DateTime)model.CreateTime).ToString("yyyy年M月d日H时m分") + "发布三天预报";

            return PartialView("_AddSuccess", svm);

        }

        public ActionResult List()
        {
            Users user = new Users_BLL().GetCurrentUser();
            WeatherInfos_BLL bll = new WeatherInfos_BLL();
            List<WeatherInfos> list = bll.GetList(a => a.AccountID == user.AccountID).OrderByDescending(a=>a.CreateTime).ToList();

            AutoMapper.Mapper.CreateMap<WeatherInfos, WeatherInfos_ViewModel>();
            List<WeatherInfos_ViewModel> newmodel = AutoMapper.Mapper.Map<List<WeatherInfos_ViewModel>>(list);
            return View(newmodel);
        }

        public ActionResult Delete(Guid id)
        {
            WeatherInfos_BLL bll = new WeatherInfos_BLL();
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
            WeatherInfos_BLL bll = new WeatherInfos_BLL();
          WeatherInfos model=   bll.Get(a => a.InfoID == id);

          AutoMapper.Mapper.CreateMap<WeatherInfos, WeatherInfos_ViewModel>();
          WeatherInfos_ViewModel vmodel = AutoMapper.Mapper.Map<WeatherInfos_ViewModel>(model);

          //List<WeatherInfos_Categorys> WeatherCategoryList = new WeatherInfos_Categorys_BLL().GetList().ToList();
          //ViewBag.WeatherCategoryList = new SelectList(WeatherCategoryList, "CategoryID", "CategoryName", false);


          return View(vmodel);
          
        }
        [HttpPost]
        public ActionResult Edit(WeatherInfos_ViewModel vmodel)
        {
           
          


            WeatherInfos_BLL bll = new WeatherInfos_BLL();
            WeatherInfos model = bll.Get(a => a.InfoID == vmodel.InfoID);
            model.Content = vmodel.Content;
           
            if (bll.Update(model) > 0)
            {
                return RedirectToAction("List");
            }
            else
            {
                ModelState.AddModelError("", "修改失败，请稍后再试！");
                //List<WeatherInfos_Categorys> WeatherCategoryList = new WeatherInfos_Categorys_BLL().GetList().ToList();
                //ViewBag.WeatherCategoryList = new SelectList(WeatherCategoryList, "CategoryID", "CategoryName", false);

                return View(vmodel);
            }

        }

    }
}
