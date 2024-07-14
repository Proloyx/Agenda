using Agenda.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;

namespace Agenda.Services
{
    public class ValidateUserFilter:IAuthorizationFilter
    {
        public ValidateUserFilter(){
            
        }
        public void OnAuthorization(AuthorizationFilterContext context){
            try
            {
                var claim = context.HttpContext.User;
                if(!claim.Identity.IsAuthenticated) {
                    context.Result = new RedirectToActionResult("Logout","Session",null);
                    return;
                }
            
                var user = JsonSerializer.Deserialize<User>(claim.FindFirst("user")?.Value);
                if (user == null){
                    context.Result = new RedirectToActionResult("Logout","Session",null);
                    return;
                }
            }
            catch (Exception e)
            {
                context.Result = new RedirectToActionResult("Logout","Session",null);
                return;
            }
        }
    }
}