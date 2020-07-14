using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpBookStore.Models;

namespace UpBookStore.Controllers
{
    public class PRController : Controller
    {
        // GET: PR
        BookStoreContext db = new BookStoreContext();
        ApplicationUser user;
        ApplicationUserManager userManager;
        public string RoleGet()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            GetUserObject();
           // if (user != null)
             //   roles = userManager.GetRoles(user.Id);
            string dime = "user";
            foreach (string i in roles)
            {
                if (i == "admin")
                {
                    dime = i;
                }
            }
            return dime;

        }

        public void GetUserObject()
        {
            userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            user = userManager.FindByEmail(User.Identity.Name);
        }
        public ActionResult Index()
        {
            GetUserObject();
            if (user != null)
            {
                ViewBag.Money = user.Money;
            }

            GetViewBag();
            return View("Index");
        }

        public void GetViewBag()
        {

            var books = db.Books;
            ViewBag.Books = books;
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
        }

        public ActionResult CashUp(int id)
        {
            GetUserObject();
            int cahs = user.Money + id;
            user.Money = cahs;
            userManager.Update(user);

            ViewBag.Money = user.Money;

            return View("Index");


        }
    }
}