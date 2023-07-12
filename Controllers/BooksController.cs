using Books.Models;
using Books.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Microsoft.Ajax.Utilities;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        readonly ApplicationDbContext _context = new ApplicationDbContext(); // instance of database 
        // GET: Book
        public ActionResult Index()
        {
            var books = _context.Books.Include(m => m.Category).ToList();
            return View(books);
        }

        public ActionResult Create()
        {
            var viewModel = new BookFormViewModel
            {
				Categories = _context.Categories.ToList()
			};
            return View("BookForm",viewModel);
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

            if (model.Id == 0)
            {
                var book = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                };
				_context.Books.Add(book);
			}
            else
            {
                var book = _context.Books.Find(model.Id);
                if(book == null)
                    return HttpNotFound();
                book.Title = model.Title;
                book.Author = model.Author;
                book.CategoryId = model.CategoryId;
                book.Description = model.Description;
            }

            _context.SaveChanges(); 

            return RedirectToAction("Index");
        }

        public ActionResult Details (int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var book = _context.Books.Include(m => m.Category).SingleOrDefault(m => m.Id == id);
           
            if (book == null)
                return HttpNotFound();

            return View(book);
		}

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var book = _context.Books.Find(id);
			if (book == null)
				return HttpNotFound();

            var viewModel = new BookFormViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                CategoryId = book.CategoryId,
                Description = book.Description,
                Categories = _context.Categories.ToList()
			};

			return View("BookForm", viewModel);
		}
    }
}