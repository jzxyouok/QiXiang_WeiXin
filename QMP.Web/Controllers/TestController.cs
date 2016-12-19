using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMP.Models;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace QMP.Web.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {


           
            List<Article> alist = new List<Article>();
            var article1 = new Article()
            {
                Title = "当前预警信号",
                //Description = model.Content,
                PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/zhyj.jpg",
                //Url = "http://weixin.qdqx.net.cn/weixin/WarnInfos/List?accountid=" + account.AccountID
            };
            alist.Add(article1);





            var article = new Article()
            {

                Title = "sdfasd",
                //Description = model.Content,
                PicUrl = System.Web.HttpUtility.UrlEncode("http://weixin.qdqx.net.cn" + "/Content/images/warning/寒潮红.png"),
                //Url = "http://weixin.qdqx.net.cn/weixin/WarnInfos/List?accountid=" + account.AccountID
            };
            alist.Add(article);



            if (!AccessTokenContainer.CheckRegistered("wx6118f98f46ed61ea"))//检查是否已经注册
            {
                AccessTokenContainer.Register("wx6118f98f46ed61ea", "d4624c36b6795d1d99dcf0547af5443d");//如果没有注册则进行注册
            }



            CustomApi.SendNews("wx6118f98f46ed61ea", "o4lDQv4_zHKPECaQ8otSDQIV6GH8", alist);
            CustomApi.SendText("wx6118f98f46ed61ea", "o4lDQv4_zHKPECaQ8otSDQIV6GH8", System.Web.HttpUtility.UrlEncode("http://weixin.qdqx.net.cn" + "/Content/images/warning/寒潮红.png"));
            return View();
        }

    }
}
