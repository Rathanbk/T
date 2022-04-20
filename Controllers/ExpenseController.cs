using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;
using Test.Models;
namespace Test.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public ExpenseController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
          
        }

        public IActionResult Index()
        {
         
            IEnumerable<Expense> objList = null;
            try
            {
                objList = _db.Expenses;
            }

            catch (System.Exception ex)
            {

                Console.WriteLine(ex);
            }
            return View(objList);
        }

        ////GET-Create
        //[Authorize(Policy ="HideRolePolicy")]
        public IActionResult Create()
        {
            
            return View();
        }

        //POST-Create
        [HttpPost]
        public IActionResult Create(Expense obj)
        {

            if (ModelState.IsValid)
            {   
                //obj.ExpenseId= _userManager.GetUserId()
                _db.Expenses.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET-Delete
        //[Authorize(Policy ="HideRolePolicy")]
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Delete-POST
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Expenses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        //GET Update
        //[Authorize(Policy = "HideRolePolicy")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Expenses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //POST- Update
        [HttpPost]
        //[Authorize(Policy = "HideRolePolicy")]
        public IActionResult Update(Expense obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


    }
}

