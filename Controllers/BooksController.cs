using Books.Models;
using Books.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        readonly ApplicationDbContext _context = new ApplicationDbContext(); // instance of database 
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var viewModel = new BookFormViewModel
            {
				Categories = _context.Categories.ToList()
			};
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _context.Categories.ToList();
                return View("Create", model);
            }

            var book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                CategoryId = model.CategoryId,
                Description = model.Description,
            };

            _context.Books.Add(book);
            _context.SaveChanges(); 

            return RedirectToAction("Index");
        }
    }
}