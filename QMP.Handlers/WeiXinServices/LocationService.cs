using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QMP.BLL;
using QMP.BLL.SQL;
using QMP.Models;
using QMP.Models.SQL;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.UserTag;
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
                Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson uij = Senparc.Weixin.MP.AdvancedAPIs.UserApi.Info(account.AppID, requestMessage.FromUserName);


                //记录位置
                Subscribers_BLL sbll = new Subscribers_BLL();
                if (sbll.GetCount(a => a.AccountID == account.AccountID && a.OpenID == requestMessage.FromUserName) > 0)
                {

                    Subscribers sub = sbll.Get(a => a.OpenID == requestMessage.FromUserName);
                    sub.Longitude = requestMessage.Longitude;
                    sub.Latitude = requestMessage.Latitude;
                    sub.Precision = requestMessage.Precision;
                    sub.LocationTime = DateTime.Now;


                    sub.NickName = uij.nickname;
                    sub.Province = uij.province;
                    sub.City = uij.city;
                    sub.Country = uij.country;
                    sub.Sex = uij.sex;
                    sub.HeadImgUrl = uij.headimgurl;
                    sub.SubscribeTime = DateTime.Parse("1970-01-01 08:00").AddSeconds(uij.subscribe_time);
                    sub.OpenID = uij.openid;
                    sub.AccountID = account.AccountID;





                    sbll.Update(sub);
                }
                else
                {


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
             
                


                ////访问记录测试
                try
                {
                  

                    TagJson tags = UserTagApi.Get(account.AppID);
                  
                    TagJson_Tag tag = tags.tags.Where(a => a.name == "开发人员").FirstOrDefault();
                    if (tag != null)
                    {


                        UserTagJsonResult usertag = UserTagApi.Get(account.AppID, tag.id);
                        List<string> opendids = usertag.data.openid;

                      
                        foreach (string openid in opendids)
                        {

                            if (!opendids.Contains(uij.openid))
                            {
                                StringBuilder sbcontent = new StringBuilder();
                                sbcontent.Append(uij.nickname + "\n");
                                string sex = "男";
                                if (uij.sex == 1)
                                {
                                    sex = "男";
                                }
                                else if (uij.sex == 2)
                                {
                                    sex = "女";
                                }
                                else
                                {
                                    sex = "未知";
                                }
                                sbcontent.Append("性别：" + sex + "\n");
                                sbcontent.Append("地区：" + uij.city + "\n");
                                sbcontent.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 进入公众号\n");
                                sbcontent.Append("经度：" + requestMessage.Longitude + "\n");
                                sbcontent.Append("纬度：" + requestMessage.Latitude + "\n");
                                sbcontent.Append("注：该消息仅开发测试人员可以收到。");
                                List<Article> alist = new List<Article>();

                                var article1 = new Article()
                                {
                                    Title = uij.nickname,
                                    Description = sbcontent.ToString(),
                                    PicUrl = uij.headimgurl,
                                    Url = uij.headimgurl
                                };
                                alist.Add(article1);
                                try
                                {
                                    CustomApi.SendNews(account.AppID, openid, alist);

                                }
                                catch (Exception ex)
                                {


                                }
                            }

                            
                        }
                    }

                }
                catch (Exception ex)
                {
                   

                }
            }
            catch (Exception ex)
            {


            }

           
        }
    }
}
