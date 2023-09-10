 using CoreMVC.Models;
using CoreMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller   //defoultni nazev pro prihlasovani
    {
        private UserManager<AppUser> userManager;    //zajistuje hledani uzivatele v databazi,update uzivatelu, ...
        private SignInManager<AppUser> signInManager;   //zajistuje prihlasovani
    
       
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [AllowAnonymous]  //aby jsme se vubec mohli prihlasit 
        public IActionResult Login(string returnUrl)  //returnUrl si tam vlozi system sam
        {
            LoginVM loginVM = new LoginVM();
            loginVM.ReturnUrl = returnUrl;
            return View(loginVM);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]  //ochrana proti crossSite scriptingem (urcita forma utoku)
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = await userManager.FindByNameAsync(login.UserName);
                if (appUser != null)
                {
                    await signInManager.SignOutAsync();  //odhlasi se, kdyby nekdo byl prihlaseny
                    //signInManager je service
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser,
                    login.Password, login.Remember, false);
                    if (result.Succeeded)
                    {
                        return Redirect(login.ReturnUrl ?? "/");   //??=>null tak se vraci do korenove slozky(tady je to  HOME)
                    }
                }
                ModelState.AddModelError(nameof(login.UserName), "Login Failed: Invalid UserName or password");
            }
            return View(login);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");   //poud se odhlasim tak se vratim na prihlasovaci formular home/index
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
