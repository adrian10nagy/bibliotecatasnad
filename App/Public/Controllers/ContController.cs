
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

        [HttpGet]
        public ActionResult LogarePartiala(MainPageModel model)
        {
            return PartialView("_LoginModal");
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
                Nationality = Nationality.Alta,
                UserType = UserType.Nesetat,
                Birthdate = DateTime.Now,
                Password = userPass,
                Username = userEmail
            });

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
