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

        [HttpGet]
        public IActionResult Index()
        {
            var type = _db.Types.Include(m=>m.Item);
            return View(type);
        }
    }
}