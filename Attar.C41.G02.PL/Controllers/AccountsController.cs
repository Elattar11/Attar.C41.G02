using Attar.C41.G02.DAL.Models;
using Attar.C41.G02.PL.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Attar.C41.G02.PL.Controllers
{
    public class AccountsController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountsController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}
        #region Sign Up

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user is null)
                {
					user = new ApplicationUser()
					{
						FName = model.FirstName,
						LName = model.LastName,
						UserName = model.Username,
						Email = model.Email,
						IsAgre = model.IsAgree

					};

                    var result =  await _userManager.CreateAsync(user , model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

				}

                ModelState.AddModelError(string.Empty, "Username already exist");
                
            }

            return View(model);
        }
        #endregion

        #region Sign In
        public IActionResult SignIn()
        {
            return View();
        }
        #endregion
    }
}
