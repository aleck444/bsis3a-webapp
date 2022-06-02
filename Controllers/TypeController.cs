using System.Collections.Generic;
using System.Linq;
using aleck3a_webapp.Data;
using aleck3a_webapp.Models.ViewModels;
using aleck3a_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aleck3a_webapp.Controllers
{
    public class TypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public TypeModel TypeM { get; set; }
        
        public TypeController(ApplicationDbContext db)
        {
            _db = db;

            TypeM = new TypeModel()
            {
                Items = _db.Items.ToList(),
                Type = new Models.Type()
            };
        }

        public IActionResult Index()
        {
            var type = _db.Types.Include(m=>m.Item);
            return View(type);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(TypeM);
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost()
        {
            if(ModelState.IsValid)
            {
                _db.Types.Add(TypeM.Type);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(TypeM);
        }

         [HttpGet]
        public IActionResult Edit(int id)
        {
            TypeM.Type = _db.Types.Include(m => m.Item).SingleOrDefault(m => m.Id == id);
           if(TypeM.Type == null)
           {
               return NotFound();
           }
            return View(TypeM);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost()
        {
            if(ModelState.IsValid)
            {
                _db.Types.Update(TypeM.Type);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(TypeM);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            TypeM.Type = _db.Types.Include(m => m.Item).SingleOrDefault(m => m.Id == id);
           if(TypeM.Type == null)
           {
               return NotFound();
           }
            return View(TypeM);
        }

         [HttpPost]
        [ActionName("Delete")]
         public IActionResult DeletePost(int id)
        {
            var type = _db.Types.Find(id);
            if(ModelState.IsValid)
            {
                _db.Types.Remove(type);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(TypeM);
        }
    }   
}