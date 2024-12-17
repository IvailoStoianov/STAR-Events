using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using STAREvents.Data.Models;
using STAREvents.Web.ViewModels.Login;
using static STAREvents.Common.EntityValidationConstants.RoleNames;
using static STAREvents.Common.ErrorMessagesConstants.LoginErrorMessages;
using static STAREvents.Common.UrlRedirectConstants.LogInConstants;
using static STAREvents.Common.ErrorMessagesConstants.SharedErrorMessages;

namespace STAREvents.Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToPage("/Index");
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user != null)
                {
                    if (user.isDeleted)
                    {
                        ModelState.AddModelError(string.Empty, ProfileDeleted);
                        return Page();
                    }

                    var result = await _signInManager.PasswordSignInAsync(user?.UserName ?? string.Empty, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        if (user != null && await _userManager.IsInRoleAsync(user, Administrator))
                        {
                            var adminDashboardUrl = Url.Page(AdminDashboard, new { area = Administrator });
                            if (adminDashboardUrl != null)
                            {
                                return LocalRedirect(adminDashboardUrl);
                            }
                        }
                        _logger.LogInformation(UserIsLoggedIn);
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, InvalidLogInAttempt);
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, InvalidLogInAttempt);
                    return Page();
                }
            }

            return Page();
        }
    }
}

