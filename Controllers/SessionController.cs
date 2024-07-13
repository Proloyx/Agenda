using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Agenda.Data;
using Agenda.Models.UserModels;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace Agenda.Controllers
{
    public class SessionController : Controller
    {
        private readonly ILogger<SessionController> _logger;
        private readonly AppDbContext _context;

        public SessionController(ILogger<SessionController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin user)
        {
            try
            {
                //Buscando el usuario
                var login = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                
                if (login == null)
                {
                    ModelState.AddModelError(string.Empty, "El nombre de usuario o la contraseña son incorrectos.");
                    return View("Index");
                }

                // Creando un token Jwt con los datos del usuario
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokendesc = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(new Claim[]{
                        new Claim("user", JsonSerializer.Serialize(login))
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JwtKey"))),SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenhandler.CreateToken(tokendesc);

                //Guardando el token en las cookies
                List<Claim> claims = new List<Claim> () {
                new Claim("token", tokenhandler.WriteToken(token))
                };
                ClaimsIdentity ci = new ClaimsIdentity (claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal cp = new ClaimsPrincipal (ci);
                await HttpContext.SignInAsync(cp, new AuthenticationProperties{IsPersistent=true});
                
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        public async Task<IActionResult> Logout(){
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}