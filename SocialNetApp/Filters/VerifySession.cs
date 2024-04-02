using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocialNetApp.Controllers;
using SocialNetApp.Models;

namespace SocialNetApp.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var oUser = (Usuarios)HttpContext.Current.Session["User"];

            if(oUser == null)
            {
                if(filterContext.Controller is AccesoController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Acceso/Index");
                }
                               
            }
            else
            {
                if(filterContext.Controller is AccesoController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }

            }

            base.OnActionExecuting(filterContext);

        }
    }
}