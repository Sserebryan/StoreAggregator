using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using DAL.Models.IdentityModels;
using WEB.ViewModels.Account;
using WEB.Providers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenProvider _tokenProvider;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Route("[action]")]
        [Authorize]
        public string Values(int id)
        {
            return "value";
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<Object> Register([FromBody]RegisterViewModel model, string returnUrl = null)
        {

            var user = new User();
            user.UserName = model.Email;
            user.Email = model.Email;

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<Object> Login([FromBody]LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            var res = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (res.Succeeded)
            {
                return _tokenProvider.GenerateToken(model);
            }

            return null ;

        }
    }
}
