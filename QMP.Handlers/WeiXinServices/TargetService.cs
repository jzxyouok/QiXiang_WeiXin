using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QMP.BLL.SQL;
using QMP.Models.SQL;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.UserTag;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace QMP.Handlers.WeiXinServices
{
    public class TargetService
    {

        public void ApplyJoin(RequestMessageText requestMessage)
        {

            OfficialAccounts_BLL obll = new OfficialAccounts_BLL();
            OfficialAccounts account = obll.Get(a => a.OriginalID == requestMessage.ToUserName);


            if (!AccessTokenContainer.CheckRegistered(account.AppID))//检查是否已经注册
            {
                AccessTokenContainer.Register(account.AppID, account.AppSecret);//如果没有注册则进行注册
            }

            string rcon = requestMessage.Content;

            if (rcon.Contains("申请加入#"))
            {
                string targetname = rcon.Substring(rcon.IndexOf("#") + 1, rcon.Length - rcon.IndexOf("#") - 1);

                TagJson tagjson = UserTagApi.Get(account.AppID);
                TagJson_Tag tag = tagjson.tags.Where(a => a.name == targetname).FirstOrDefault();
                if (tag != null)
                {
                    List<string> openids = new List<string>();
                    openids.Add(requestMessage.FromUserName);
                var result=    UserTagApi.BatchTagging(account.AppID, tag.id, openids);
                    if (result.errmsg == "ok")
                    {
                        CustomApi.SendText(account.AppID, requestMessage.FromUserName, "恭喜，加入【"+ targetname + "】成功！");

                    }
                    else
                    {
                        CustomApi.SendText(account.AppID, requestMessage.FromUserName, "抱歉，加入【" + targetname + "】失败，请稍后再试！");

                    }
                }
                else
                {
                    CustomApi.SendText(account.AppID, requestMessage.FromUserName, "您发送的用户分组名称有误，请重新发送！");

                }
            }
            else if (rcon.Contains("取消加入#"))
            {
                string targetname = rcon.Substring(rcon.IndexOf("#") + 1, rcon.Length - rcon.IndexOf("#") - 1);

                TagJson tagjson = UserTagApi.Get(account.AppID);
                TagJson_Tag tag = tagjson.tags.Where(a => a.name == targetname).FirstOrDefault();
                if (tag != null)
                {
                    List<string> openids = new List<string>();
                    openids.Add(requestMessage.FromUserName);
                    var result = UserTagApi.BatchUntagging(account.AppID, tag.id, openids);
                    if (result.errmsg == "ok")
                    {
                        CustomApi.SendText(account.AppID, requestMessage.FromUserName, "恭喜，取消加入【" + targetname + "】成功！");

                    }
                    else
                    {
                        CustomApi.SendText(account.AppID, requestMessage.FromUserName,
                            "抱歉，取消加入【" + targetname + "】失败，请稍后再试！");

                    }
                }
                else
                {
                    CustomApi.SendText(account.AppID, requestMessage.FromUserName, "您发送的用户分组名称有误，请重新发送！");

                }
            }
            else
            {
                CustomApi.SendText(account.AppID, requestMessage.FromUserName, "您发送的内容有误，请重新发送！");

            }

        }
    }
}
