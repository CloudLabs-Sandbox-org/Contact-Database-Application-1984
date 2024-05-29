using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var users = userlist.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString)).ToList();
                return View(users);
            }
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            User user = GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // GET: User/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            userlist.Add(user);

            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User user = GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            User existingUser = GetUserById(id);

            if (existingUser == null)
            {
                return HttpNotFound();
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            return RedirectToAction("Index");
        }


        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            User user = GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            userlist.Remove(user);

            return RedirectToAction("Index");
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            User user = GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            userlist.Remove(user);

            return RedirectToAction("Index");
        }

        private User GetUserById(int id)
        {
            return userlist.FirstOrDefault(u => u.Id == id);
        }
 

        
    }
}
