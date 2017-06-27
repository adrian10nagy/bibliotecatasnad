
namespace Public.Controllers
{
    using BL.Managers;
    using DAL.Entities;
    using Public.Models;
    using System;
    using System.Web.Mvc;
    using BL.Helpers;

    public class ContController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logare()
        {
            if (Session["userId"] != null)
            {
                return RedirectToAction("Delogare");
            }

            return View();
        }

        public ActionResult Imprumuturi()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Logare");
            }

            var loans = LoansManager.GetLoansByUserId(Session["userId"].ToString().ToNullableInt().Value);

            return View(loans);
        }

        public ActionResult LogarePartiala(MainPageModel model)
        {
            return PartialView("_LoginModal");
        }

        public ActionResult Setari()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Logare");
            }

            var flags = PageQueryParam.None;

            if(Request.QueryString["passwordchange"] != null)
            {
                flags = PageQueryParam.PasswordChangeFalse;
                if(Request.QueryString["passwordchange"].ToString() == "true")
                {
                    flags = PageQueryParam.PasswordChangeTrue;
                }
            }

            var userId = Session["userId"].ToString().ToNullableInt().Value;
            ViewData["flags"] = flags;

            return View(userId);
        }

        [HttpPost]
        public ActionResult SchimbaParola(string passwordOld, string passwordNew, string passwordNewCheck, string userId)
        {
            var result = false;
            if(!string.IsNullOrEmpty(passwordOld) && !string.IsNullOrEmpty(passwordNew) && 
                !string.IsNullOrEmpty(passwordNewCheck) && userId.ToNullableInt().HasValue &&
                passwordNew.CompareTo(passwordNewCheck) == 0)
            {
                result = UsersManager.UpdateUserPassword(userId.ToNullableInt().Value, passwordOld, passwordNew, 1);
            }

            if (result)
            {
                return this.RedirectToAction("Setari", new { passwordchange = "true" });
            }

            return this.RedirectToAction("Setari", new { passwordchange = "false" });
        }

        [HttpPost]
        public ActionResult AsyncUserLogin(string userEmail, string userPass)
        {
            var user = UsersManager.GetUserForLogin(userEmail, userPass);

            if (user == null || user.Id == 0)
            {
                return Json(new { response = JSONResponse.IncorrectAuthentication });
            }

            Session["userId"] = user.Id;
            Session["userType"] = (int)user.UserType;
            Session["userFirstName"] = user.FirstName;
            Session["userLastName"] = user.LastName;

            var x = Session["userId"] == null;

            return Json(new { response = JSONResponse.Success });
        }

        [HttpPost]
        public ActionResult AsyncUserRegister(string userSurame, string userName, string userEmail, string userPass)
        {
            var exitsUsername = UsersManager.GetUserByEmail(userEmail);

            if(exitsUsername != null)
            {
                return Json(new { response = JSONResponse.EmailExists });
            }

            var user = UsersManager.Add(new User
            {
                FirstName = userName,
                LastName = userSurame,
                Email = userEmail,
                JoinDate = DateTime.Now,
                Flags = UserFlag.Default,
                Gender = Gender.Nesetată,
                Locality = new Locality { Id = 1 },
                Library = new Library { Id = 1 },
                Nationality = Nationality.Alta,
                UserType = UserType.Nesetat,
                Birthdate = DateTime.Now,
                Password = userPass,
                Username = userEmail
            }, 1);

            return AsyncUserLogin(userEmail, userPass);
        }

        public ActionResult Delogare()
        {
            Session.Remove("userId");
            Session.Remove("userType");
            Session.Remove("userFirstName");
            Session.Remove("userLastName");

            return RedirectToAction("Index", "Home");
        }
    }
}
