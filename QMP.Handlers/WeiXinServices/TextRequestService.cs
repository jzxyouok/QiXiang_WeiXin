using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QMP.BLL;
using QMP.BLL.SQL;
using QMP.Models;
using QMP.Models.SQL;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace QMP.Handlers.WeiXinServices
{
     public class TextRequestService
    {

         public void ForecastReply(RequestMessageText requestMessage)
         {

             OfficialAccounts_BLL obll = new OfficialAccounts_BLL();
             OfficialAccounts account = obll.Get(a => a.OriginalID == requestMessage.ToUserName);


             if (!AccessTokenContainer.CheckRegistered(account.AppID))//检查是否已经注册
             {
                 AccessTokenContainer.Register(account.AppID, account.AppSecret);//如果没有注册则进行注册
             }
             WeatherInfos_BLL bll = new WeatherInfos_BLL();


             if (bll.GetCount(
                 a => a.AccountID == account.AccountID && a.WeatherInfos_Categorys.CategoryName == "三天预报") > 0)
             {
                 WeatherInfos model =
                     bll.GetList(
                         a => a.AccountID == account.AccountID && a.WeatherInfos_Categorys.CategoryName == "三天预报")
                         .OrderByDescending(a => a.CreateTime)
                         .FirstOrDefault();

                 //CustomApi.SendText(account.AppID, requestMessage.FromUserName, model.Content);

                 List<Article> alist = new List<Article>();





                 var article1 = new Article()
                 {
                     Title = account.CompanyName + ((DateTime)model.CreateTime).ToString("M月d日H时m分发布"),
                     //Description = model.Content,
                     PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/dqyb.jpg",
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


                 CustomApi.SendNews(account.AppID, requestMessage.FromUserName, alist);

             }
         }
    }
}
