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
            

            ViewBag.Money = user.Money;
            userManager.Update(user);

            return PartialView("_CashUp");


        }

        public ActionResult Box()
        {
            List<Book> books = new List<Book>();
            int cash = 0;
            foreach(var book in db.Books)
            {
                string de = HttpContext?.Request?.Cookies[book.ID.ToString()]?.Value ?? " ";
                if (de == HttpContext.User.Identity.Name)
                {
                    books.Add(book);
                    cash += 200;
                }
            }
            Session["cash"] = cash;
            Session["books"] = books;
            return View(books);
        }
        [HttpPost]
        public ActionResult Box(List<Book> boodks)
        {
            List<Book> books = (List<Book>)Session["books"];
            foreach (var book in books)
            {
                Sell sell = new Sell();
                sell.ID_Books = book.ID;
                sell.Email = HttpContext.User.Identity.Name;
                sell.Price = 200;
                db.Sells.Add(sell);
            }
            db.SaveChanges();
            GetUserObject();

            if(user.Money >= Convert.ToInt32(Session["cash"]))
            {
                user.Money = user.Money - Convert.ToInt32(Session["cash"]);
                userManager.Update(user);
            }
            ViewBag.Money = user.Money;
            GetViewBag();
            return View("Index");
        }
        public ActionResult ViewHistory()
        {
            List<Sell> sells = new List<Sell>();
            if (HttpContext.User.IsInRole("admin"))
            {
                foreach (var sell in db.Sells)
                {
                    sells.Add(sell);
                }
            } else
            {
                foreach (var sell in db.Sells.Where(d => d.Email == HttpContext.User.Identity.Name))
                {
                    sells.Add(sell);
                }
            }
            GetViewBag();
            return View(sells);
        }
    }
}