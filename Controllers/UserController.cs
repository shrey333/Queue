using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Queue.Models;
using Queue.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserRepository userRepository;
        private readonly MerchantRepository merchantRepository;
        private readonly QueueRepository queueRepository;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, UserRepository userRepository, MerchantRepository merchantRepository, QueueRepository queueRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepository = userRepository;
            this.merchantRepository = merchantRepository;
            this.queueRepository = queueRepository;
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Index()
        {
            ViewData["exists"] = "";
            var user = await userManager.GetUserAsync(HttpContext.User);
            var myqueue = queueRepository.GetMerchantQueue(user.Email);
            //var merchants = merchantRepository.GetAllMerchant();
            var getusers = userRepository.GetUser(user.Email);
            var merchants = merchantRepository.GetLocationBased(getusers.UserLatitude, getusers.UserLongitude);
            ViewData["long"] = getusers.UserLongitude;
            ViewData["lati"] = getusers.UserLatitude;
            dynamic mymodel = new ExpandoObject();
            mymodel.merchants = merchants;
            mymodel.myqueue = myqueue;
            return View(mymodel);
        }

        [HttpPost]
        [Authorize]
        [ActionName("change")]
        public async Task<IActionResult> change()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var myqueue = queueRepository.GetMerchantQueue(user.Email);
            var merchants = merchantRepository.GetLocationBased(HttpContext.Request.Form["Latitude"], HttpContext.Request.Form["Longitude"]);
            var getUser = userRepository.GetUser(user.Email);
            getUser.UserLongitude = HttpContext.Request.Form["Longitude"];
            getUser.UserLatitude = HttpContext.Request.Form["Latitude"];
            userRepository.Update(getUser);
            dynamic mymodel = new ExpandoObject();
            mymodel.merchants = merchants;
            mymodel.myqueue = myqueue;
            return View("index", mymodel);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> index(String id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            ViewData["exists"] = "";
            if(queueRepository.GetQueue(user.Email, id) != null)
            {
                ViewData["exists"] = "Your already in Queue";
            }
            else
            {
                var queueModel = new QueueModel
                {
                    UserId = user.Email,
                    MerchantId = id
                };
                queueRepository.Add(queueModel);
            }
            var myqueue = queueRepository.GetMerchantQueue(user.Email);
            var merchants = merchantRepository.GetAllMerchant();
            dynamic mymodel = new ExpandoObject();
            mymodel.merchants = merchants;
            mymodel.myqueue = myqueue;
            return View(mymodel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    PhoneNumber = userRegister.PhoneNumber,
                    UserName = userRegister.Email,
                    Email = userRegister.Email,
                };

                var result = await userManager.CreateAsync(user, userRegister.Password);

                if (result.Succeeded)
                {
                    User newUser = new User
                    {
                        UserEmail = userRegister.Email,
                        UserFirstName = userRegister.UserFirstName,
                        UserLastName = userRegister.UserLastName,
                        UserPhoneNumber = userRegister.PhoneNumber,
                        UserLatitude = "0.00000",
                        UserLongitude = "0.00000"
                    };
                    userRepository.Add(newUser);
                    await userManager.AddToRoleAsync(user, "user");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "user");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View(userRegister);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogIn userLogIn, string returnurl)
        {
            if (ModelState.IsValid) 
            {
                var result = await signInManager.PasswordSignInAsync(userLogIn.Email, userLogIn.Password, userLogIn.RememberMe, false);
                Console.WriteLine(userLogIn.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnurl) && Url.IsLocalUrl(returnurl))
                    {
                        return LocalRedirect(returnurl);
                    }
                    return RedirectToAction("index", "user");
                }
                //await Response.WriteAsync("Failed");
                ModelState.AddModelError(string.Empty, "Invalid Login attempt");
            }
            return View(userLogIn);
        }

        [HttpPost]
        public async Task<IActionResult> logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var dbuser = userRepository.GetUser(user.Email);
            var userEdit = new UserEdit
            {
                UserFirstName = dbuser.UserFirstName,
                UserLastName = dbuser.UserLastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return View(userEdit);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Edit(UserEdit userEdit)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var dbuser = userRepository.GetUser(user.Email);
                dbuser.UserFirstName = userEdit.UserFirstName;
                dbuser.UserLastName = userEdit.UserLastName;
                user.PhoneNumber = userEdit.PhoneNumber;
                dbuser.UserPhoneNumber = userEdit.PhoneNumber;
                await userManager.UpdateAsync(user);
                userRepository.Update(dbuser);
                return RedirectToAction("index", "user");
            }
            return View(userEdit);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Delete()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var dbuser = userRepository.GetUser(user.Email);
            var userRegister = new UserRegister
            {
                UserFirstName = dbuser.UserFirstName,
                UserLastName = dbuser.UserLastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return View(userRegister);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteUser()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            userRepository.Delete(user.Email);
            await userManager.DeleteAsync(user);
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        /*[HttpGet]
        public IActionResult Role()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Role(role role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = role.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "UserController");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View(role);
        }*/
    }
}
