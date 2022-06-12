using System.Collections.Generic;
using System.Linq;
using aleck3a_webapp.Data;
using aleck3a_webapp.Models.ViewModels;
using aleck3a_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace aleck3a_webapp.Controllers
{
    public class InstrumentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public InstrumentTypeModel InstrumentTypeM { get; set; }
        
        public InstrumentController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;

            InstrumentTypeM  = new InstrumentTypeModel()
            {
                Items = _db.Items.ToList(),
                Types = _db.Types.ToList(),
                Instrument = new Models.Instrument()
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            var instrument = _db.Instruments.Include(m=>m.Item).Include(m=>m.Type);
            return View(instrument);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(InstrumentTypeM);
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost()
        {
            if(ModelState.IsValid)
            {
                _db.Instruments.Add(InstrumentTypeM.Instrument);
                _db.SaveChanges();

                var InstrumentId = InstrumentTypeM.Instrument.Id;

                string wwwrootPath = _hostingEnvironment.WebRootPath;

                var files = HttpContext.Request.Form.Files;

                var saveinstrument = _db.Instruments.Find(InstrumentId);

                if(files.Count != 0)
                    {
                        var ImagePath = @"images\instrument\";
                        var Extension = Path.GetExtension(files[0].FileName);
                        var RelativeImagePath = ImagePath + InstrumentId + Extension;
                        var aImagePath = Path.Combine(wwwrootPath, RelativeImagePath);

                        using (var fileStream = new FileStream(aImagePath,FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        saveinstrument.ImagePath = RelativeImagePath;
                        _db.SaveChanges();
                        
                    }
                return RedirectToAction("Index");
            }
            return View(InstrumentTypeM);
        }

         [HttpGet]
        public IActionResult Edit(int id)
        {
            InstrumentTypeM.Instrument = _db.Instruments.Include(m => m.Item).SingleOrDefault(m => m.Id == id);
           if(InstrumentTypeM.Instrument == null)
           {
               return NotFound();
           }
            return View(InstrumentTypeM);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost()
        {
            if(ModelState.IsValid)
            {
                _db.Instruments.Update(InstrumentTypeM.Instrument);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(InstrumentTypeM);
        }

        [HttpPost]
         public IActionResult Delete(int id)
        {
            var instrument = _db.Instruments.Find(id);
            if(instrument == null)
            {
                return NotFound();
            }
             _db.Instruments.Remove(instrument);
             _db.SaveChanges();
            return RedirectToAction("Index");
        
        }
    }   
}