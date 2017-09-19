using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using DAL.Models.IdentityModels;
using Emailing;
using Newtonsoft.Json.Converters;
using WEB.Attributes;
using WEB.Extensions;
using WEB.ViewModels.Account;
using WEB.Providers;
using WEB.ViewModels;

namespace WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiValidationFilter]
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenProvider _tokenProvider;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager, ITokenProvider tokenProvider, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model, string returnUrl = null)
        {
            var user = new User
            {
                UserName = model.Name,
                Email = model.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Email, code, Request.Scheme);
                var emailTemplate = GetEmailConfirmTemplate(user, callbackUrl);
                await _emailSender.SendMessage(emailTemplate);
                return new OkResult();
            }

            AddErrors(result);

            return new ValidationFailedResult(ModelState);
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }
            
            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, $"Cannot find user with email {resetPasswordViewModel.Email}");
            }

            var passwordResetCode = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);

            if (passwordResetCode.Succeeded)
            {
                return new ApiOkResult("User changed password succesfully");
            }
            
            AddErrors(passwordResetCode);
            
            return new ValidationFailedResult(ModelState);
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);

                if (user == null)
                {
                    ModelState.AddModelError(String.Empty,
                        $"User with this email {forgotPasswordViewModel.Email} not found");
                    return new ValidationFailedResult(ModelState);
                }

                if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError(String.Empty,
                        $"Email {forgotPasswordViewModel.Email} isn't confirmed");
                    return new ValidationFailedResult(ModelState);
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = Url.ResetPasswordCallbackLink(user.Email, code, Request.Scheme);



                var emailTemplate = GetEmailPasswordResetTemplate(user, callbackUrl);

                await _emailSender.SendMessage(emailTemplate);
                
                return new ApiOkResult($"Link to reset password was send to email {forgotPasswordViewModel.Email}");
            }

            return new ValidationFailedResult(ModelState);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> SendVerificationEmail([FromBody]EmailVerificationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty ,$"Unable to load user with Email '{model.Email}'.");
                return new ValidationFailedResult(ModelState);
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Email, code, Request.Scheme);
            var emailTemplate = GetEmailConfirmTemplate(user, callbackUrl);
            
            await _emailSender.SendMessage(emailTemplate);
            return new ApiOkResult("Verification email sent. Please check your email");
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                
                ModelState.AddModelError(String.Empty,
                    $"UserId and code should be not empty");
                return new ValidationFailedResult(ModelState);
            }
            var user = await _userManager.FindByEmailAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError(String.Empty,
                    $"Unable to load user with ID '{userId}'.");
                return new ValidationFailedResult(ModelState);
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return new ApiOkResult("Email is confirmed");
            }

            return new BadRequestResult();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
                var res = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (res.Succeeded)
                {
                    var token = _tokenProvider.GenerateToken(model);
                    return new ApiOkResult(token);
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to login");
            return new UnauthorizedFailedResult(ModelState);

        }

        private EmailTemplate GetEmailConfirmTemplate(User user, String callbackUrl)
        {
            return new EmailTemplate
            {
                Content = $"Link to confirm email {callbackUrl}",
                Receiver = user.Email,
                ReceiverName = user.UserName,
                Subject = "Confirm email"
            };
        }
        
        private EmailTemplate GetEmailPasswordResetTemplate(User user, String callbackUrl)
        {
            return new EmailTemplate
            {
                Content = $"Link to reset password {callbackUrl}",
                Receiver = user.Email,
                ReceiverName = user.UserName,
                Subject = "Reset password"
            };
        }
    }
}
