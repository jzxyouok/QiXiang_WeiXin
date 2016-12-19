using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Senparc.Weixin.MP.Entities;

using QMP.BLL;
using QMP.Models;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;

namespace QMP.Handlers.WeiXinServices
{
    public class ForecastService
    {
        public void ForecastReply(RequestMessageEvent_Click requestMessage)
        {
            
            OfficialAccounts_BLL obll = new OfficialAccounts_BLL();
            OfficialAccounts account = obll.Get(a => a.OriginalID == requestMessage.ToUserName);


            if (!AccessTokenContainer.CheckRegistered(account.AppID))//检查是否已经注册
            {
                AccessTokenContainer.Register(account.AppID, account.AppSecret);//如果没有注册则进行注册
            }

            //CustomApi.SendText(account.AppID, requestMessage.FromUserName, "开始查询");
            //CustomApi.SendText(account.AppID, requestMessage.FromUserName, requestMessage.EventKey);

            if (requestMessage.EventKey == "三天预报")
            {
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
                        PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/"+account.SanTianImg,
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
                else
                {
                    CustomApi.SendText(account.AppID, requestMessage.FromUserName, "当前未发布短期预报！");

                }






            }
            else if (requestMessage.EventKey == "短时临近")
            {

                ShortNearInfos_BLL bll = new ShortNearInfos_BLL();
                if (bll.GetCount(a => a.AccountID == account.AccountID && a.EndTime > DateTime.Now) > 0)
                {
                    ShortNearInfos model =
                        bll.GetList(a => a.AccountID == account.AccountID && a.EndTime > DateTime.Now).OrderByDescending(a => a.StartTime).FirstOrDefault();
                    List<Article> alist = new List<Article>();





                    var article1 = new Article()
                    {
                        Title = account.CompanyName + ((DateTime)model.StartTime).ToString("M月d日H时m分发布"),
                        //Description = model.Content,
                        PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/" + account.ShortNearImg,
                        Url = "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id=" + model.InfoID.ToString(),
                    };
                    var article2 = new Article()
                    {
                        Title = model.Content,
                        //Description = model.Content,
                        //PicUrl = "Content/images/article/dqyb.jpg",
                        Url = "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id=" + model.InfoID.ToString(),
                    };
                    var article3 = new Article()
                    {
                        Title = "详情",
                        //Description = model.Content,
                        //PicUrl = "Content/images/article/dqyb.jpg",
                        Url = "http://weixin.qdqx.net.cn/weixin/ShortNearInfos/Details?id="+model.InfoID.ToString(),
                    };
                    alist.Add(article1);
                    alist.Add(article2);
                    alist.Add(article3);

                    CustomApi.SendNews(account.AppID, requestMessage.FromUserName, alist);

                }
                
                else
                {
                    CustomApi.SendText(account.AppID, requestMessage.FromUserName, "当前未发布短时临近预报！");

                }
               



            }

            else if (requestMessage.EventKey == "预警信号")
            {
                WarnInfos_BLL bll = new WarnInfos_BLL();
                List<WarnInfos> list =
                    bll.GetList(a => a.AccountID == account.AccountID).OrderByDescending(a => a.ReleaseTime).ToList();

                if (list.Count() > 0)
                {



                    List<Article> alist = new List<Article>();
                    var article1 = new Article()
                    {
                        Title = "当前预警信号",
                        //Description = model.Content,
                        PicUrl = "http://weixin.qdqx.net.cn" + "/Content/images/article/zhyj.jpg",
                        Url = "http://weixin.qdqx.net.cn/weixin/WarnInfos/List?accountid=" + account.AccountID
                    };
                    alist.Add(article1);




                    foreach (WarnInfos infos in list)
                    {

                        var article = new Article()
                        {

                            Title =
                                account.CompanyName + ((DateTime)infos.ReleaseTime).ToString("M月d日H时m分") + "发布" +
                                infos.WarningCategory + infos.WarningLevel + "预警",
                            //Description = model.Content,
                            PicUrl =
                                "http://weixin.qdqx.net.cn" + "/Content/images/warning/" + infos.ImageName,
                            Url = "http://weixin.qdqx.net.cn/weixin/WarnInfos/List?accountid=" + account.AccountID
                        };
                        alist.Add(article);
                    }

                    var article2 = new Article()
                    {

                        Title ="详情",
                        Url = "http://weixin.qdqx.net.cn/weixin/WarnInfos/List?accountid=" + account.AccountID
                    };
                    alist.Add(article2);


                    CustomApi.SendNews(account.AppID, requestMessage.FromUserName, alist);

                }
                else
                {
                    CustomApi.SendText(account.AppID, requestMessage.FromUserName, "当前未发布预警信号");

                }
            }
        }
    }
}
