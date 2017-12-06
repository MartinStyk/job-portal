using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;
using PresentationLayer.Helpers;
using PresentationLayer.ViewModel;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        public UserFacade UserFacade { get; set; }
        public EmployerFacade EmployerFacade { get; set; }
        public SkillSelectListHelper SkillSelectListHelper { get; set; }


        // GET Account/RegisterUser
        public async Task<ActionResult> RegisterUser()
        {
            var model = new UserCreateViewModel
            {
                AllSkills = await SkillSelectListHelper.Get()
            };

            return View(model);
        }

        // POST Account/RegisterUser
        [HttpPost]
        public async Task<ActionResult> RegisterUser(UserCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await UserFacade.Register(model.UserCreateDto);

                    var authTicket = new FormsAuthenticationTicket(1, model.UserCreateDto.Email, DateTime.Now,
                        DateTime.Now.AddMinutes(30), false, "User");
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    return RedirectToAction("Index", "Home");
                }
                catch (ArgumentException)
                {
                    ModelState.AddModelError("Username", "Account with that username already exists!");
                    return View();
                }
            }

            model.AllSkills = await SkillSelectListHelper.Get();
            return View(model);
        }

        public ActionResult RegisterEmployer()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterEmployer(EmployerCreateDto employerCreateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await EmployerFacade.Register(employerCreateDto);

                    var authTicket = new FormsAuthenticationTicket(1, employerCreateDto.Email, DateTime.Now,
                        DateTime.Now.AddMinutes(30), false, "Employer");
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authCookie);

                    return RedirectToAction("Index", "Home");
                }
                catch (ArgumentException)
                {
                    ModelState.AddModelError("Username", "Account with that username already exists!");
                    return View();
                }
            }

            return View(employerCreateDto);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            (bool successUser, string rolesUser) = UserFacade.Login(model.Email, model.Password);
            (bool successEmployer, string rolesEmployer) = EmployerFacade.Login(model.Email, model.Password);

            if (successUser || successEmployer)
            {
                //FormsAuthentication.SetAuthCookie(model.Username, false);

                var authTicket = new FormsAuthenticationTicket(1, model.Email, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, successUser ? rolesUser : rolesEmployer);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);

                var decodedUrl = "";
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    decodedUrl = Server.UrlDecode(returnUrl);
                }

                if (Url.IsLocalUrl(decodedUrl))
                {
                    return Redirect(decodedUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Wrong username or password!");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
