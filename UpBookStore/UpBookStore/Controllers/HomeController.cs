using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UpBookStore.Models;

namespace UpBookStore.Controllers
{
    // dswewds
    public class HomeController : Controller
    {
        BookStoreContext db = new BookStoreContext();
        ApplicationUser user;

        [Authorize]
        public string RoleGet()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext()
            .GetUserManager<ApplicationUserManager>();

            user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            string dime = "user";
            foreach(string i in roles)
            {
                if(i == "admin")
                {
                    dime = i;
                }
            }
            return dime;

        }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            logger.Trace("trace message");
            logger.Debug("debug message");
            logger.Info("info message");
            var books = db.Books;
            ViewBag.Books = books;
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            ViewBag.roles = RoleGet();
            return View();
        }

        List<Book> dbooks;
        const int pageSize = 8;

        
        public ActionResult Books(int? id)
        {
            int page = id ?? 0;
            dbooks = db.Books.ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", GetItemsPage(page));
            }

            var books = db.Books.ToList();
            ViewBag.Books = books;
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            ViewBag.roles = RoleGet();

            return View(GetItemsPage(page));
           
        }

        private List<Book> GetItemsPage(int page = 1)
        {
            var itemsToSkip = page * pageSize;
            dbooks = db.Books.ToList(); 
            return dbooks.OrderBy(t => t.ID).Skip(itemsToSkip).
                Take(pageSize).ToList();
        }
        [HttpPost]
        public JsonResult Books(Book book, string wherere, string wherere2) //dddddddddddddddd
        {
            try
            {
                foreach (Auftor auftor in db.Auftors.Where(auftor => auftor.FIO == wherere))
                {
                    book.Auftor_ID = auftor.ID;
                }
                foreach (Categoryi categoryiss in db.Categoryis.Where(categoryiss => categoryiss.Names == wherere2))
                {
                    book.Category_ID = categoryiss.ID;
                }
                db.Books.Add(book);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return Json("Строка возвращена с сервера");
            }
            finally
            {
                var books = db.Books.ToList();
                ViewBag.Books = books;
                var auftors = db.Auftors.ToList();
                ViewBag.Aufrots = auftors;
                var category = db.Categoryis.ToList();
                ViewBag.Category = category;
                ViewBag.roles = RoleGet();
            }

            return Json("Строка возвращена с сервера");
        }
        [HttpGet]
        public ActionResult Auftors()
        {
            var books = db.Books.ToList();
            ViewBag.Books = books;
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            ViewBag.roles = RoleGet();
            return View();
        }

        [HttpGet]
        public ActionResult DeletA(int id)
        {
            var auftor = db.Auftors.Find(id);
            db.Auftors.Remove(auftor);
            db.SaveChanges(); var books = db.Books.ToList();
            ViewBag.Books = books;
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            ViewBag.roles = RoleGet();
            return View("Auftors");
        }
        [HttpGet]
        public ActionResult DeletB(int id)
        {
            var book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges(); var books = db.Books.ToList();
            ViewBag.Books = books;
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            ViewBag.roles = RoleGet();
            return PartialView("DeletB");
        }


        [HttpPost]
        public ActionResult Auftors(Auftor auftor)
        {
            db.Auftors.Add(auftor);
            db.SaveChanges();
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            ViewBag.roles = RoleGet();
            db.SaveChanges();
            return View("Auftors");
        }
        [HttpGet]
        public ActionResult UpdateA(int id)
        {
            ViewBag.Id = id;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            return View();
        }
        [HttpPost]
        public ActionResult UpdateA(Auftor auftor)
        {
            Auftor auftor1 = db.Auftors.Find(auftor.ID);
            auftor1.FIO = auftor.FIO;
            db.SaveChanges();
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            ViewBag.roles = RoleGet();
            return View("Auftors");
        }

        [HttpGet]
        public ActionResult UpdateB(int id)
        {
            var books = db.Books.ToList();
            ViewBag.Books = books;
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            ViewBag.Id = id;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            return View();
        }
        [HttpPost]
        public ActionResult UpdateB(Book copy, string wherere)
        {
            Book book = db.Books.Find(copy.ID);
            foreach (Auftor auftorr in db.Auftors.Where(auftorr => auftorr.FIO == wherere))
            {
                book.Auftor_ID = auftorr.ID;
                book.Name = copy.Name;

            }
            db.SaveChanges();
            var books = db.Books.ToList();
            ViewBag.Books = books;
            var auftors = db.Auftors.ToList();
            ViewBag.Aufrots = auftors;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            ViewBag.roles = RoleGet();
            return View("Books");
        }
        public int categoryid = 0;
        public int MoveIndexMax = 0;
        [HttpGet]
        public ActionResult Category(int id = -1, bool flag = false)
        {
            
            if (id != -1)
            {
                categoryid = id;
                List<Book> books = new List<Book>();
                if (flag)
                {
                    books = db.Books.Where(b => b.Category_ID == id).OrderBy(n => n.Name).Take(5).Select(b => b).ToList();
                } else
                {
                    books = db.Books.Where(b => b.Category_ID == id).OrderByDescending(n => n.Name).Take(5).Select(b => b).ToList();
                }
                MoveIndexMax = db.Books.Where(b => b.Category_ID == id).Count();
                ViewBag.MoveIndexMax = MoveIndexMax;
                ViewBag.Flag = flag;
                ViewBag.FlagF = false;
                ViewBag.Books = books;
                Categoryi categoryi = db.Categoryis.Find(id);
                ViewBag.ID = categoryi;



                var category = db.Categoryis.ToList();
                ViewBag.Category = category;
                return View();
            } else
            {
               var books = db.Books.ToList();
                ViewBag.Books = books;
                var auftors = db.Auftors.ToList();
                ViewBag.Aufrots = auftors;
                var category = db.Categoryis.ToList();
                ViewBag.Category = category;
                return View("Index");
            }
            
        }



        [HttpGet]
        public ActionResult MoveIndexs(int indexs, int id, bool flag = false, string TextSearch = null, int idPoick = -1)
        {

            List<Book> books = new List<Book>();
            if (!flag && idPoick == -1)
            {
                int max = categoryid;
                books = db.Books.Where(b => b.Category_ID == id).Select(b => b).OrderByDescending(d => d.Name).Skip(indexs).Take(5).ToList();
                MoveIndexMax = db.Books.Where(b => b.Category_ID == id).Count();
                
                ViewBag.Flag = false;
                
            }
            else if (flag && idPoick == -1)
            {
                int max = categoryid;
                books = db.Books.Where(b => b.Category_ID == id).Select(b => b).OrderBy(d => d.Name).Skip(indexs).Take(5).ToList();
                MoveIndexMax = db.Books.Where(b => b.Category_ID == id ).Count();
                
                ViewBag.Flag = true;
                
            } else if (idPoick != -1)
            {
                if (idPoick == 0)
                {
                    ViewBag.FlagF = true;
                    ViewBag.OjectScrin = 0;
                    foreach (Auftor auftorr in db.Auftors.Where(auftorr => auftorr.FIO.ToUpper().StartsWith(TextSearch)))
                    {
                        books.AddRange(db.Books.Where(d => d.Auftor_ID == auftorr.ID && d.Category_ID == id).OrderBy(n => n.Name).Skip(indexs).Take(5).Select(b => b).ToList());
                    }

                }
                else
                {
                    ViewBag.FlagF = true;
                    ViewBag.OjectScrin = 1;
                    books = db.Books.Where(b => b.Name.ToUpper().StartsWith(TextSearch) && b.Category_ID == id).OrderBy(n => n.Name).Skip(indexs).Take(5).Select(b => b).ToList();
                }
            }
            ViewBag.Books = books;
            ViewBag.MoveIndexMax = MoveIndexMax;
            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            Categoryi categoryi = db.Categoryis.Find(id);
            ViewBag.ID = categoryi;
            return View("Category");
        }

        [HttpPost]
        public ActionResult Category(int id, string ClasSearch, string TextSearch)
        {
            categoryid = id;
            List<Book> books = new List<Book>();
            ViewBag.TextSearch = TextSearch;
            if (ClasSearch == "Автор")
            {
                ViewBag.OjectScrin = 0;
                List<int> di = new List<int>();
                foreach (Auftor auftorr in db.Auftors.Where(auftorr => auftorr.FIO.ToUpper().StartsWith(TextSearch)))
                {
                    di.Add(auftorr.ID);
                }
                foreach(int g in di)
                {
                    books = db.Books.Where(d => d.Auftor_ID == g && d.Category_ID == id).OrderBy(n => n.Name).Take(5).Select(b => b).ToList();

                }
            }
            else
            {
                ViewBag.OjectScrin = 1;
                books = db.Books.Where(b => b.Name.ToUpper().StartsWith(TextSearch) && b.Category_ID == id).OrderBy(n => n.Name).Take(5).Select(b => b).ToList();
            }/*
            if (false)
            {
            }
            else
            {
                books = books.OrderByDescending(n => n.Name).Select(b => b).ToList();
            }*/



            MoveIndexMax = db.Books.Where(b => b.Name.ToUpper().StartsWith(TextSearch)).Count();
            ViewBag.MoveIndexMax = MoveIndexMax;
            ViewBag.Flag = false;
            ViewBag.FlagF = true;
            ViewBag.Books = books;
            Categoryi categoryi = db.Categoryis.Find(id);
            ViewBag.ID = categoryi;

            var category = db.Categoryis.ToList();
            ViewBag.Category = category;
            return View("Category");
        }
    }
}