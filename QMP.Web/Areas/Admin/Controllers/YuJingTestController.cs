using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using QMP.BLL.SQL;
using QMP.Models.SQL;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.UserTag;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;

namespace QMP.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class YuJingTestController : Controller
    {
        // GET: Admin/YuJingTest
        public ActionResult Index()
        {
            Guid accountid = (Guid)new Users_BLL().GetCurrentUser().AccountID;
            OfficialAccounts_BLL bll = new OfficialAccounts_BLL();
            OfficialAccounts oa = bll.Get(a => a.AccountID == accountid);
          
            if (!AccessTokenContainer.CheckRegistered(oa.AppID)) //检查是否已经注册
            {
                AccessTokenContainer.Register(oa.AppID, oa.AppSecret); //如果没有注册则进行注册
            }


            TagJson tags = UserTagApi.Get(oa.AppID);

            TagJson_Tag tag = tags.tags.Where(a => a.name == "测试组").FirstOrDefault();
            if (tag != null)
            {


                UserTagJsonResult usertag = UserTagApi.Get(oa.AppID, tag.id);
                List<string> opendids = usertag.data.openid;


                string warnlevel = "蓝色";
                string shijian = DateTime.Now.ToString("yyyy年MM月dd日HH时mm分");


                foreach (string openid in opendids)
                {

                    var yb = new
                    {
                        //使用new 方式，构建数据，包括value, color两个固定属性。
                        first =
                        new
                        {
                            value = "流亭双埠站风速超过一级阈值",
                            color = ColorToCode(warnlevel)
                        },
                        keyword1 = new { value = "流亭双埠站", color = ColorToCode(warnlevel) },
                        keyword2 = new { value = shijian, color = ColorToCode(warnlevel) },
                        keyword3 = new { value = "超过一级阈值", color = ColorToCode(warnlevel) },
                        remark = new { value = "请注意关注！", color = ColorToCode(warnlevel) }
                    };
                    //string url = "http://weixin.qdqx.net.cn/weixin/WarnInfos/List?accountid=" + user.AccountID;

                    SendTemplateMessageResult sendResult = TemplateApi.SendTemplateMessage(oa.AppID, openid, "_rSnwSpde1e73lYQ2woG_QLrmEPqjsUD1UfjAYAeeXc", null, yb);



                }
            }

            return View();
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
    }
}