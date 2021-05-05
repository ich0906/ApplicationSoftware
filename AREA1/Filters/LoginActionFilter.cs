using AREA1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Tool;

namespace AREA1.Filters {
    public class LoginActionFilter : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context) {

            UserModel userInfo = SessionExtensionTool.GetObject<UserModel>(context.HttpContext.Session, "userInfo");

            if(userInfo == null) {
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                     { "controller", "Login" },
                     { "action", "Login" },
                     { "alertLogin", 2}
                });
            }
        }
    }
}
