using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Identity.Models;
using System.Threading.Tasks;

namespace Notes.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly IIdentityServerInteractionService interactionService;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interactionService) =>
            (this.signInManager, this.userManager, this.interactionService) = (signInManager, userManager, interactionService);

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await userManager.FindByNameAsync(viewModel.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var result = await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Login error");
            return View(viewModel);
        }
    }
}
