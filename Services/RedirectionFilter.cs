using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Agenda.Services
{
    public class RedirectionFilter:IAuthorizationFilter
    {
        public RedirectionFilter(){
            
        }
        public void OnAuthorization(AuthorizationFilterContext context){
            var claim = context.HttpContext.User;
            if(!claim.Identity.IsAuthenticated) {
                context.Result = new RedirectToActionResult("Index","Session",null);
                }
            return;
        }
    }
}




/*var user = JsonSerializer.Deserialize<UserRet>(claim.FindFirst("user")?.Value);

            if (user == null || !user.Roles.Any(role => role.Name == "admin")){
                context.Result = new ForbidResult();*/