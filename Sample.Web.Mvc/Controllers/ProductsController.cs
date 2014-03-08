using Sample.Domain;
using Sample.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Mvc.Controllers
{
    public class ProductsController : Controller
    {
        protected IProductRepository ProductRepository { get; set; }

        public ProductsController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        //
        // GET: /Products/
        public ActionResult Index()
        {
            return View(ProductRepository.GetAll());
        }

        public ActionResult CreateMany()
        {
            Random rnd = new Random();
            
            for (int i = 0; i < 10; i++)
            {
                ProductRepository.Save(new Product("Teste " + i.ToString(), i * rnd.Next()));
            }

            return Content("OK");
        }

        //
        // GET: /Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Products/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View(ProductRepository.GetById(id));
        }

        //
        // POST: /Products/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                ProductRepository.Save(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
