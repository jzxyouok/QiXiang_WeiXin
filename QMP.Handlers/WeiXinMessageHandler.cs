using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using QMP.BLL;
using QMP.Handlers.WeiXinServices;
using QMP.Models;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;

namespace QMP.Handlers
{
    public class WeiXinMessageHandler : MessageHandler<WeiXinMessageContext>
    {
        //private string appId = string.Empty;
        public WeiXinMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;

            //if (!string.IsNullOrEmpty(postModel.AppId))
            //{
            //    appId = postModel.AppId;//通过第三方开放平台发送过来的请求
            //}

            //在指定条件下，不使用消息去重
            base.OmitRepeatedMessageFunc = requestMessage =>
            {
                var textRequestMessage = requestMessage as RequestMessageText;
                if (textRequestMessage != null && textRequestMessage.Content == "容错")
                {
                    return false;
                }
                return true;
            };
        }

        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }

        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            /* 所有没有被处理的消息会默认返回这里的结果，
            * 因此，如果想把整个微信请求委托出去（例如需要使用分布式或从其他服务器获取请求），
            * 只需要在这里统一发出委托请求，如：
            * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
            * return responseMessage;
            */

            //var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            //responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            //return responseMessage;
            return null;
        }

        /// <summary>
        /// 处理菜单点击事件
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            #region 

       
                ForecastService fs = new ForecastService();
                fs.ForecastReply(requestMessage);
            
            #endregion

            return null;



        }


       

        #region 定位服务
        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {

            LocationService ls = new LocationService();
            ls.AddLocation(requestMessage);

            //var responseMessage = this.CreateResponseMessage<ResponseMessageText>();//ResponseMessageText也可以是News等其他类型
            //responseMessage.Content = "此句为正常返回。\r\n" + DateTime.Now.ToString("yyyy年M月d日 H时m分s秒") + "。";
            //return responseMessage;
            return null;
        }


    

        #endregion


        #region 关注事件

        /// <summary>
        /// 处理关注事件
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {

         
            return null;

        }

    
        #endregion



        #region 取消关注事件
        /// <summary>
        /// 处理取消关注事件
        /// </summary>
        /// <param name="requestMessage">请求消息</param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {

           
            return null;


        }

        #endregion

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {

            try
            {
                //var responseMessage = this.CreateResponseMessage<ResponseMessageText>();//ResponseMessageText也可以是News等其他类型
                //responseMessage.Content ="sdfads";
                //return responseMessage;
                //OfficialAccounts_BLL obll = new OfficialAccounts_BLL();
                //OfficialAccounts model = obll.Get(a => a.OriginalID == requestMessage.ToUserName);


                //if (!AccessTokenContainer.CheckRegistered(model.AppID))//检查是否已经注册
                //{
                //    AccessTokenContainer.Register(model.AppID, model.AppSecret);//如果没有注册则进行注册
                //}
                //Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson uij = Senparc.Weixin.MP.AdvancedAPIs.UserApi.Info(model.AppID, requestMessage.FromUserName);

                //StringBuilder sb = new StringBuilder();
                //sb.Append("内容：" + requestMessage.Content + "\r\n");
                //sb.Append("发送人：" + uij.nickname + "\r\n");
                //sb.Append("地址：" + uij.province + uij.city + uij.country + "\r\n");
                //sb.Append("时间：" + DateTime.Now.ToString("yyyy年M月d日 H时m分s秒") + "。");

                //var responseMessage = this.CreateResponseMessage<ResponseMessageText>();//ResponseMessageText也可以是News等其他类型
                //responseMessage.Content = sb.ToString();
                //return responseMessage;
                if (requestMessage.Content.Contains("加入"))
                {
                    TargetService ts = new TargetService();
                    ts.ApplyJoin(requestMessage);
                }
                else
                {
                    TextRequestService ts = new TextRequestService();

                    ts.ForecastReply(requestMessage);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;

                //var responseMessage = this.CreateResponseMessage<ResponseMessageText>();//ResponseMessageText也可以是News等其他类型
                //responseMessage.Content = ex.ToString();
                //return responseMessage;
            }

           
        }
    }
}