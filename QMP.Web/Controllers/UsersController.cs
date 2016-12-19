
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HaiRui.Web;
using QMP.BLL;
using QMP.BLL.SQL;
using QMP.Models;
using QMP.Models.SQL;
using QMP.ViewModels;

namespace QMP.Web.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginUserModel model, string ReturnUrl)
        {
            Users_BLL bll = new Users_BLL();
            if (bll.CheckExist(model.UserName, model.PassWord))
            {


                FormsAuthentication.SetAuthCookie(model.UserName, false);
                //if (((Url.IsLocalUrl(returnUrl) && (returnUrl.Length > 1))&& (returnUrl.StartsWith("/") && !returnUrl.StartsWith("//"))) && !returnUrl.StartsWith(@"/\"))
                if (!string.IsNullOrEmpty(ReturnUrl))
                {

                    return Redirect(ReturnUrl);
                }
                else
                {
                    //return RedirectToAction("Index", "Home");
                    string url = Url.Content(Request.UrlReferrer.AbsoluteUri);


                    string action = url.Substring(url.LastIndexOf("/") + 1, url.Length - url.LastIndexOf("/") - 1);
                    if (action == "Login" || action == "login")
                    {
                        return RedirectToAction("Index", "Home",new{Area="Admin"});

                    }
                    else
                    {
                        return Redirect(Url.Content(Request.UrlReferrer.AbsoluteUri));

                    }

                }
                //  return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "用户名或密码错误，请重新输入！");
            return View(model);
        }
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult CheckUser(string UserName)
        {
            Users_BLL bll = new Users_BLL();
            if (bll.GetCount(a => a.UserName == UserName) > 0)
            {
                bool result = false;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool result = true;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUserModel model)
        {
            Users_BLL bll = new Users_BLL();
            Users user = new Users();
            user.UserID = Guid.NewGuid();
            user.UserName = model.UserName;

            user.Password = SecurityHelper.MD5(model.PassWord);

            user.CreateTime = DateTime.Now;

            if (bll.Add(user) > 0)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "注册失败，请重新输入！");
            return View(model);
        }

    }
}
