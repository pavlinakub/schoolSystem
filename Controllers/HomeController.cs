using CoreMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> userManager;
        public HomeController(/*ILogger<HomeController> logger,*/UserManager<AppUser> userManager)
        {
            //  _logger = logger;  
            this.userManager = userManager;   //vymena parametru a datove slozky kvuli rolim prihlasovatelu
                                                //zkontroluje usera a nastavi mu roli
        }
        [Authorize]   //prihlasovani bude pomoci username ....viewModel/loginVM
        public async Task<IActionResult> Index()    //asynchroni ....musi to vracet task
        {AppUser loggedInUser=await userManager.GetUserAsync(HttpContext.User);  //zobrazi jmeno uzivatele
            string message = $" {loggedInUser.UserName}";
            return View("Index",message);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}