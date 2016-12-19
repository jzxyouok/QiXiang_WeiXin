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
    public class LocationService
    {
        public void AddLocation(RequestMessageEvent_Location requestMessage)
        {
            OfficialAccounts_BLL obll = new OfficialAccounts_BLL();
            OfficialAccounts account = obll.Get(a => a.OriginalID == requestMessage.ToUserName);


            if (!AccessTokenContainer.CheckRegistered(account.AppID))//检查是否已经注册
            {
                AccessTokenContainer.Register(account.AppID, account.AppSecret);//如果没有注册则进行注册
            }


            try
            {

               



                















                //记录位置
                Subscribers_BLL sbll = new Subscribers_BLL();
                if (sbll.GetCount(a => a.AccountID == account.AccountID && a.OpenID == requestMessage.FromUserName) > 0)
                {
                    Subscribers sub = sbll.Get(a => a.OpenID == requestMessage.FromUserName);
                    sub.Longitude = requestMessage.Longitude;
                    sub.Latitude = requestMessage.Latitude;
                    sub.Precision = requestMessage.Precision;
                    sub.LocationTime = DateTime.Now;




                    //WeatherInfos_BLL wbll = new WeatherInfos_BLL();

                    //if (wbll.GetCount(
                    //    a => a.AccountID == account.AccountID && a.WeatherInfos_Categorys.CategoryName == "三天预报") > 0)
                    //{
                    //    WeatherInfos model =
                    //        wbll.GetList(
                    //            a => a.AccountID == account.AccountID && a.WeatherInfos_Categorys.CategoryName == "三天预报")
                    //            .OrderByDescending(a => a.CreateTime)
                    //            .FirstOrDefault();

                    //    //CustomApi.SendText(account.AppID, requestMessage.FromUserName, model.Content);

                    //    if (sub.ReceivedLastSanDayID != model.InfoID)
                    //    {
                    //        List<Article> alist = new List<Article>();
                    //        var article1 = new Article()
                    //        {
                    //            Title = account.CompanyName + ((DateTime)model.CreateTime).ToString("M月d日H时m分发布"),
                    //            //Description = model.Content,
                    //            PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/dqyb.jpg",
                    //            Url = ""
                    //        };
                    //        var article2 = new Article()
                    //        {
                    //            Title = model.Content,
                    //            //Description = model.Content,
                    //            //PicUrl = "Content/images/article/dqyb.jpg",
                    //            Url = ""
                    //        };

                    //        alist.Add(article1);
                    //        alist.Add(article2);

                    //        CustomApi.SendNews(account.AppID, requestMessage.FromUserName, alist);

                    //        sub.ReceivedLastSanDayID = model.InfoID;
                    //        sub.ReceivedLastSanDayTime = DateTime.Now;
                    //    }

                    //}

                    //ShortNearInfos_BLL stbll = new ShortNearInfos_BLL();
                    //if (stbll.GetCount(a => a.AccountID == account.AccountID && a.EndTime > DateTime.Now) > 0)
                    //{
                    //    ShortNearInfos model =
                    //        stbll.GetList(a => a.AccountID == account.AccountID && a.EndTime > DateTime.Now).OrderByDescending(a => a.StartTime).FirstOrDefault();

                    //    if (sub.ReceivedLastShortNearID != model.InfoID)
                    //    {

                    //        List<Article> alist = new List<Article>();
                    //        var article1 = new Article()
                    //        {
                    //            Title = account.CompanyName + ((DateTime) model.StartTime).ToString("M月d日H时m分发布"),
                    //            //Description = model.Content,
                    //            PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/dslj.jpg",
                    //            Url =
                    //                "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id=" +
                    //                model.InfoID.ToString(),
                    //        };
                    //        var article2 = new Article()
                    //        {
                    //            Title = model.Content,
                    //            //Description = model.Content,
                    //            //PicUrl = "Content/images/article/dqyb.jpg",
                    //            Url =
                    //                "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id=" +
                    //                model.InfoID.ToString(),
                    //        };
                    //        var article3 = new Article()
                    //        {
                    //            Title = "详情",
                    //            //Description = model.Content,
                    //            //PicUrl = "Content/images/article/dqyb.jpg",
                    //            Url =
                    //                "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id=" +
                    //                model.InfoID.ToString(),
                    //        };
                    //        alist.Add(article1);
                    //        alist.Add(article2);
                    //        alist.Add(article3);

                    //        CustomApi.SendNews(account.AppID, requestMessage.FromUserName, alist);

                    //        sub.ReceivedLastShortNearID = model.InfoID;
                    //        sub.ReceivedLastShortNearTime = DateTime.Now;
                    //    }


                    //}
                



                    sbll.Update(sub);
                }
                else
                {
                    Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson uij = Senparc.Weixin.MP.AdvancedAPIs.UserApi.Info(account.AppID, requestMessage.FromUserName);


                    Subscribers sub = new Subscribers();

                    sub.SubscribeID = Guid.NewGuid();
                    sub.NickName = uij.nickname;
                    sub.Province = uij.province;
                    sub.City = uij.city;
                    sub.Country = uij.country;
                    sub.Sex = uij.sex;
                    sub.HeadImgUrl = uij.headimgurl;
                    sub.SubscribeTime = DateTime.Parse("1970-01-01 08:00").AddSeconds(uij.subscribe_time);
                    sub.OpenID = uij.openid;
                    sub.AccountID = account.AccountID;
                    sub.CreateTime = DateTime.Now;

                    
                    sub.Longitude = requestMessage.Longitude;
                    sub.Latitude = requestMessage.Latitude;
                    sub.Precision = requestMessage.Precision;
                    sub.LocationTime = DateTime.Now;



                    sbll.Add(sub);
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }
    }
}
