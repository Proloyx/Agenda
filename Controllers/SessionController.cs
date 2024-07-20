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
using Agenda.Interfaces;
using Agenda.Models.UserModels;
using AutoMapper;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Agenda.Controllers
{
    public class SessionController : Controller
    {
        private readonly ILogger<SessionController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SessionController(ILogger<SessionController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
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
                var login = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
                
                if (login == null)
                {
                    ModelState.AddModelError(string.Empty, "El nombre de usuario o la contraseña son incorrectos.");
                    return View("Index");
                }
                
                //Guardando el token en las cookies
                List<Claim> claims = new List<Claim> () {
                new Claim("user", JsonSerializer.Serialize(login))
                };

                ClaimsIdentity ci = new ClaimsIdentity (claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal cp = new ClaimsPrincipal (ci);
                await HttpContext.SignInAsync(cp, new AuthenticationProperties{IsPersistent=true});
                
                return RedirectToAction("Index", "Dashboard");
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

        public IActionResult Register(){
            return View();
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(UserRegister user){
            try
            {
                var email = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (email != null) {
                    ModelState.AddModelError(string.Empty, "Ya existe un usuario con ese correo.");
                    return View("Register");
                }
                var newuser = _mapper.Map<User>(user);
                await _context.Users.AddAsync(newuser);
                await _context.SaveChangesAsync();
                return View("Index");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}