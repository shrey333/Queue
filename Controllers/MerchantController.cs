using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Queue.Models;
using Queue.ViewModels;
using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Queue.Controllers
{
    public class MerchantController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly MerchantRepository merchantRepository;
        private readonly QueueRepository queueRepository;

        public MerchantController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, MerchantRepository merchantRepository, QueueRepository queueRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.merchantRepository = merchantRepository;
            this.queueRepository = queueRepository;
        }

        [HttpGet]
        [Authorize(Roles = "merchant")]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var myqueue = queueRepository.GetUserQueue(user.Email);
            dynamic mymodel = new ExpandoObject();
            mymodel.myqueue = myqueue;
            return View(mymodel);  
        }

        [HttpPost]
        [Authorize(Roles = "merchant")]
        public async Task<IActionResult> Index(string id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            queueRepository.Delete(id, user.Email);
            var myqueue = queueRepository.GetUserQueue(user.Email);
            dynamic mymodel = new ExpandoObject();
            mymodel.myqueue = myqueue;
            return View(mymodel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MerchantRegister merchantRegister)
        {
            if (ModelState.IsValid)
            {
                var merchant = new IdentityUser
                {
                    PhoneNumber = merchantRegister.PhoneNumber,
                    UserName = merchantRegister.Email,
                    Email = merchantRegister.Email,
                };

                var result = await userManager.CreateAsync(merchant, merchantRegister.Password);

                if (result.Succeeded)
                {
                    Merchant newMerchant = new Merchant
                    {
                        MerchantEmail = merchantRegister.Email,
                        MerchantFirstName = merchantRegister.MerchantFirstName,
                        MerchantLastName = merchantRegister.MerchantLastName,
                        MerchantLongitude = merchantRegister.MerchantLongitude,
                        MerchantLatitude = merchantRegister.MerchantLatitude,
                        MerchantAddress1 = merchantRegister.MerchantAddress1,
                        MerchantAddress2 = merchantRegister.MerchantAddress2,
                        MerchantPinCode = merchantRegister.MerchantPinCode,
                        MerchantShopName = merchantRegister.MerchantShopName,
                        MerchantPhoneNumber = merchantRegister.PhoneNumber
                    };
                    merchantRepository.Add(newMerchant);
                    await userManager.AddToRoleAsync(merchant, "merchant");
                    await signInManager.SignInAsync(merchant, isPersistent: false);
                    return RedirectToAction("index", "merchant");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View(merchantRegister);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MerchantLogIn merchantLogIn)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(merchantLogIn.Email, merchantLogIn.Password, merchantLogIn.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "merchant");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login attempt");
            }
            return View(merchantLogIn);
        }

        [HttpPost]
        public async Task<IActionResult> logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [Authorize(Roles = "merchant")]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var dbuser = merchantRepository.GetMerchant(user.Email);
            var merchantEdit = new MerchantEdit
            {
                MerchantShopName = dbuser.MerchantShopName,
                MerchantFirstName = dbuser.MerchantFirstName,
                MerchantLastName = dbuser.MerchantLastName,
                MerchantAddress1 = dbuser.MerchantAddress1,
                MerchantAddress2 = dbuser.MerchantAddress2,
                MerchantLatitude = dbuser.MerchantLatitude,
                MerchantLongitude = dbuser.MerchantLongitude,
                MerchantPinCode = dbuser.MerchantPinCode,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(merchantEdit);
        }

        [HttpPost]
        [Authorize(Roles = "merchant")]
        public async Task<IActionResult> Edit(MerchantEdit merchantEdit)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var dbuser = merchantRepository.GetMerchant(user.Email);
                user.Email = merchantEdit.Email;
                user.PhoneNumber = merchantEdit.PhoneNumber;
                dbuser.MerchantFirstName = merchantEdit.MerchantFirstName;
                dbuser.MerchantLastName = merchantEdit.MerchantLastName;
                dbuser.MerchantAddress1 = merchantEdit.MerchantAddress1;
                dbuser.MerchantAddress2 = merchantEdit.MerchantAddress2;
                dbuser.MerchantPinCode = merchantEdit.MerchantPinCode;
                dbuser.MerchantLongitude = merchantEdit.MerchantLongitude;
                dbuser.MerchantLatitude = merchantEdit.MerchantLatitude;
                dbuser.MerchantShopName = merchantEdit.MerchantShopName;
                merchantRepository.Update(dbuser);
                await userManager.UpdateAsync(user);
                return RedirectToAction("index", "merchant");
            }
            return View(merchantEdit);
        }

        [HttpGet]
        [Authorize(Roles = "merchant")]
        public async Task<IActionResult> Delete()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var dbuser = merchantRepository.GetMerchant(user.Email);
            var merchantEdit = new MerchantEdit
            {
                MerchantShopName = dbuser.MerchantShopName,
                MerchantFirstName = dbuser.MerchantFirstName,
                MerchantLastName = dbuser.MerchantLastName,
                MerchantAddress1 = dbuser.MerchantAddress1,
                MerchantAddress2 = dbuser.MerchantAddress2,
                MerchantLatitude = dbuser.MerchantLatitude,
                MerchantLongitude = dbuser.MerchantLongitude,
                MerchantPinCode = dbuser.MerchantPinCode,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(merchantEdit);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "merchant")]
        public async Task<IActionResult> DeleteMerchant()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            merchantRepository.Delete(user.Email);
            await userManager.DeleteAsync(user);
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
