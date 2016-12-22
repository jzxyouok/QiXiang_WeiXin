using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QMP.BLL;
using QMP.BLL.SQL;
using QMP.Models;
using QMP.Models.SQL;
using QMP.ViewModels;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace QMP.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class WarnInfosController : Controller
    {
        //
        // GET: /Admin/WarnInfo/

        public ActionResult Add()
        {


            //WarnInfos_BLL bll = new WarnInfos_BLL();

            //ViewBag.WarnNameList = new SelectList(new WarnInfos_BLL().GetList(), "RoleID", "RoleName", null);
            string sql = "select distinct (Name) from warninfos_categorys";
            QMPEntities db = new QMPEntities();
            List<string> list = db.Database.SqlQuery<string>(sql).ToList();

            ViewBag.WarnNameList = list;




            //List<WarnInfos_States> WarnStateList = new WarnInfos_States_BLL().GetList().OrderBy(a => a.StateID).ToList();
            //ViewBag.WarnStateList = new SelectList(WarnStateList, "StateID", "StateName", false);

            return View();
        }

        [HttpPost]
        public ActionResult Add(WarnInfos_ViewModel model, FormCollection fc)
        {
            
            
            AutoMapper.Mapper.CreateMap<WarnInfos_ViewModel, WarnInfos>();
            WarnInfos newmodel = AutoMapper.Mapper.Map<WarnInfos>(model);

            Users user = new Users_BLL().GetCurrentUser();
            newmodel.InfoID = Guid.NewGuid();
            newmodel.UserID = user.UserID;
            newmodel.AccountID = user.AccountID;
            newmodel.CreateTime = DateTime.Now;
            string shijian = ((DateTime)model.ReleaseTime).ToString("yyyy年M月d日H时m分");

            int colorid = int.Parse(fc["CategoryID"].ToString());

            WarnInfos_Categorys cate = new WarnInfos_Categorys_BLL().Get(a => a.CategoryID == colorid);
            newmodel.WarningCategory = cate.Name;
            newmodel.WarningLevel = cate.Color;
            newmodel.ImageName = cate.ImageName;

            newmodel.Content = model.Content.Trim() + "\r\n" + "防御指南：" + "\r\n" + model.FangYu.Trim();

            WarnInfos_BLL bll = new WarnInfos_BLL();
            //发布之前先解除同类预警
            bll.Delete(a => a.AccountID == newmodel.AccountID && a.WarningCategory == newmodel.WarningCategory);
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

            string warncategory = cate.Name;
            string warnlevel = cate.Color;

            foreach (string openid in aa.openid)
            {


                var yb = new
                {
                    //使用new 方式，构建数据，包括value, color两个固定属性。
                    first =
                        new
                        {
                            value = user.OfficialAccounts.CompanyName + "发布" + warncategory + warnlevel + "预警信号",
                            color = ColorToCode(warnlevel)
                        },
                    alarm_unit = new { value = user.OfficialAccounts.CompanyName, color = ColorToCode(warnlevel) },
                    alarm_type = new { value = warncategory, color = ColorToCode(warnlevel) },
                    alarm_level = new { value = warnlevel, color = ColorToCode(warnlevel) },
                    alarm_time = new { value = shijian, color = ColorToCode(warnlevel) },
                    remark = new { value = user.OfficialAccounts.CompanyName + shijian + "发布" + warncategory + warnlevel + "预警信号。", color = ColorToCode(warnlevel) }
                };
                string url = "http://weixin.qdqx.net.cn/weixin/WarnInfos/List?accountid=" + user.AccountID;

                SendTemplateMessageResult sendResult = TemplateApi.SendTemplateMessage(appid, openid, user.OfficialAccounts.WarnReleaseTemplateID,  url, yb);



                if (sendResult != null)
                {
                    chenggong++;
                }
                else
                {
                    shibai++;
                }


            }




            Messages_BLL msgbll = new Messages_BLL();
            Messages msg = new Messages();
            msg.MsgID = Guid.NewGuid();
            msg.SendTime = DateTime.Now;
            msg.Title = user.OfficialAccounts.CompanyName + shijian + "发布" + cate.Name + cate.Color + "预警信号";

            msg.SendSuccessCount = chenggong;
            msg.SendFailedCount = shibai;
            msg.SendTotalCount = aa.openid.Count();
            msg.AccountID = user.AccountID;
            msg.Content = newmodel.Content;
            msg.CreateTime = DateTime.Now;
            Users currentuser = new Users_BLL().Get(a => a.UserName == User.Identity.Name);
            msg.UserID = currentuser.UserID;
            msg.CategoryID = 2;
            msgbll.Add(msg);



            WarnSendSussess_ViewModel wsvm = new WarnSendSussess_ViewModel();

            wsvm.TotalCount = msg.SendTotalCount;
            wsvm.SuccessCount = msg.SendSuccessCount;
            wsvm.FailedCount = msg.SendFailedCount;

            wsvm.Title = warncategory + warnlevel + "预警信号";

            return PartialView("_AddSussess", wsvm);

           


        }



        public ActionResult List(int id = 1)
        {
            Users user = new Users_BLL().GetCurrentUser();
            WarnInfos_BLL bll = new WarnInfos_BLL();
            int totalCount = bll.GetCount(a => a.AccountID == user.AccountID);
            List<WarnInfos> mlist =
                bll.GetPageListOrderBy(id, 20, a => a.AccountID == user.AccountID, a => a.CreateTime,false).ToList();



            var list = new StaticPagedList<WarnInfos>(mlist, id, 20, totalCount);
            return View(list);
        }



        public ActionResult GetWarnNames()
        {

            string sql = "select distinct (Name) from warninfos_categorys";
            QMPEntities db = new QMPEntities();
            List<string> list = db.Database.SqlQuery<string>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult _GetWarnColors(string name)
        {

            string sql = "select CategoryID,Color from WarnInfos_Categorys where Name = '" + name.Trim() + "'";
            QMPEntities db = new QMPEntities();
            List<WarnInfos_Colors_ViewModel> list = db.Database.SqlQuery<WarnInfos_Colors_ViewModel>(sql).ToList();


            return PartialView(list);
        }
        public ActionResult _GetWarnFangYu(string cate,string level)
        {

            WarnInfos_Categorys_BLL bll = new WarnInfos_Categorys_BLL();
           WarnInfos_Categorys model= bll.Get(a => a.Name == cate.Trim() && a.Color == level.Trim());
           return Content(model.ZhiNan);
        }



        public static string ColorToCode(string color)
        {
            string colorcode = string.Empty;
            switch (color)
            {
                case "蓝色":
                    colorcode = "#0000ff";
                    break;
                case "黄色":
                    colorcode = "#ffd800";
                    break;
                case "橙色":
                    colorcode = "#ff9900";
                    break;
                case "红色":
                    colorcode = "#ff0000";
                    break;
                default:
                    colorcode = "#0000ff";
                    break;
            }

            return colorcode;
        }

        
        public ActionResult Relieve(Guid id)
        {
            WarnInfos_BLL bll = new WarnInfos_BLL();
            WarnInfos model = bll.Get(a => a.InfoID == id);
            WarnInfos_Relieve_ViewModel vm = new WarnInfos_Relieve_ViewModel();
            vm.InfoID = model.InfoID;
            vm.Category = model.WarningCategory;
            vm.Level = model.WarningLevel;
           
            return View(vm);
        }

        [HttpPost]
        public ActionResult ConfirmRelieve(WarnInfos_Relieve_ViewModel vmodel)
        {
            WarnInfos_BLL bll = new WarnInfos_BLL();
            WarnInfos model = bll.Get(a => a.InfoID == vmodel.InfoID);
            Users user = new Users_BLL().GetCurrentUser();
            string appid = user.OfficialAccounts.AppID;
            if (!AccessTokenContainer.CheckRegistered(appid)) //检查是否已经注册
            {
                AccessTokenContainer.Register(appid, user.OfficialAccounts.AppSecret); //如果没有注册则进行注册
            }
            OpenIdResultJson users = UserApi.Get(appid, null);
            OpenIdResultJson_Data aa = users.data;


            int chenggong = 0;
            int shibai = 0;

            string shijian = vmodel.RelieveTime.ToString("yyyy年M月d日H时m分");

            string warncategory = model.WarningCategory;
            string warnlevel = model.WarningLevel;

            foreach (string openid in aa.openid)
            {

                if (string.IsNullOrEmpty(vmodel.Content))
                {
                    vmodel.Content = string.Empty;
                }
                var yb = new
                {
                    //使用new 方式，构建数据，包括value, color两个固定属性。
                    first =
                        new
                        {
                            value = user.OfficialAccounts.CompanyName + "解除" + warncategory + warnlevel + "预警信号",
                            color = ColorToCode(warnlevel)
                        },
                    keyword1 = new { value = user.OfficialAccounts.CompanyName, color = ColorToCode(warnlevel) },
                    keyword2 = new { value = warncategory, color = ColorToCode(warnlevel) },
                    keyword3 = new { value = warnlevel, color = ColorToCode(warnlevel) },
                    keyword4 = new { value = shijian, color = ColorToCode(warnlevel) },
                    remark = new {value= vmodel.Content, color = ColorToCode(warnlevel) }

                };


                string url = "http://weixin.qdqx.net.cn/weixin/WarnInfos/List?accountid=" + user.AccountID;

                SendTemplateMessageResult sendResult = TemplateApi.SendTemplateMessage(appid, openid, user.OfficialAccounts.WarnRelieveTemplateID, url, yb);



                if (sendResult != null)
                {
                    //return Content(sendResult.msgid.ToString());
                    chenggong++;
                }
                else
                {
                    //return Content("发送失败");
                    shibai++;
                }
            }




            Messages_BLL msgbll = new Messages_BLL();
            Messages msg = new Messages();
            msg.MsgID = Guid.NewGuid();
            msg.SendTime = DateTime.Now;
            msg.Title = user.OfficialAccounts.CompanyName + shijian + "解除" + warncategory + warnlevel + "预警信号";
            msg.SendSuccessCount = chenggong;
            msg.SendFailedCount = shibai;
            msg.SendTotalCount = aa.openid.Count();
            msg.AccountID = user.AccountID;
            msg.Content = user.OfficialAccounts.CompanyName + shijian + "解除" + warncategory + warnlevel + "预警信号";
            msg.CreateTime = DateTime.Now;
            Users currentuser = new Users_BLL().Get(a => a.UserName == User.Identity.Name);
            msg.UserID = currentuser.UserID;
            msg.CategoryID = 2;
            
            
            
            msgbll.Add(msg);



         


          //解除

            bll.Delete(a => a.AccountID == model.AccountID && a.WarningCategory == model.WarningCategory);

            


             WarnSendSussess_ViewModel wsvm = new WarnSendSussess_ViewModel();

            wsvm.TotalCount = msg.SendTotalCount;
            wsvm.SuccessCount = msg.SendSuccessCount;
            wsvm.FailedCount = msg.SendFailedCount;

            wsvm.Title = warncategory + warnlevel + "预警信号";

            return PartialView("_RelieveSussess", wsvm);



            //return RedirectToAction("List");
        }

        public ActionResult Delete(Guid id)
        {
            WarnInfos_BLL bll = new WarnInfos_BLL();
            bll.Delete(a => a.InfoID == id);
            return RedirectToAction("List");
        }

    }
}
