using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Agenda.Data;
using Agenda.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Agenda.Services
{
    public class CookieService: ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetUser(){
            var cookie = _httpContextAccessor.HttpContext.User.FindFirstValue("User");
            var user = JsonSerializer.Deserialize<User>(cookie);
            return user;
        }
    }
}