using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    
    public class ItemController : Controller
    {
       
        private readonly ApplicationDbContext _db;
    
        public ItemController(ApplicationDbContext db)
        {
            _db = db;
           
        }
        public IActionResult Visible()
        {
            return View();
        }

        public IActionResult Index()
        {
            IEnumerable<Item> objList = null;
            try
            {
              

                objList = _db.Items;

            }

            catch (System.Exception ex)
            {

                Console.WriteLine(ex);
            }
            return View(objList);


        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Item obj)
        {
            try
            {
                if (obj.Borrower.Length < 2)
                {
                    throw new Exception("Enter proper input");
                }

                _db.Items.Add(obj);
                _db.SaveChanges();

            }
            catch (System.Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
       
    }
}
