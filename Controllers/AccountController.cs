using EmployManagment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace EmployManagment.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<ApplicationUser> signInManager;
        UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel uservm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                { 
                
                             UserName = uservm.UserName,

                             Email = uservm.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, uservm.Password);
                if (result.Succeeded)
                {
                 
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return RedirectToAction("Login", "Account");
            }
            return View("Register", uservm);

        }



        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel uservm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(uservm.UserName);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, uservm.password);
                    if (found)
                    {
                        List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                        await signInManager.SignInWithClaimsAsync(user, isPersistent: uservm.RememberMe, claims);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                      
                        ModelState.AddModelError(string.Empty, "Incorrect password.");
                    }
                }
                else
                {
                    
                    ModelState.AddModelError(string.Empty, "Username does not exist.");
                }
            }
            return View("Login", uservm);
        }

    }
}
