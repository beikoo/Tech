using DataAccess;
using EMag.Helpers;
using EMag.Models.Products;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMag.Controllers
{
    [CustomAutorize]
    public class ProductsController : Controller
    {
        private readonly ProductsRepository _productsRepository;
        public ProductsController()
        {
            _productsRepository = new ProductsRepository();
        }
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            ListProductsVM model = new ListProductsVM
            {
                Products = new List<ProductsModel>()
            };

            List<Product> products = _productsRepository.GetAll();

            foreach(var product in products)
            {
                ProductsModel p = new ProductsModel
                {
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                };

                model.Products.Add(p);
            }

            return View(model);
        }
        //
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductsModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "There was a problem while creating the product.";
                return RedirectToAction("Index");
            }
            Product product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description
            };
            try
            {
                _productsRepository.Create(product);
            } // Error ID
            catch
            {
                TempData["ErrorMessage"] = "There was a problem while creating the product.";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Product not found.";

                return RedirectToAction("Index");
            }
            Product product = _productsRepository.GetById(id.Value);
            ProductsModel model = new ProductsModel
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ProductsModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "There was a problem with editing the product.";

                return RedirectToAction("Index");
            }

            Product product = new Product
            {
                ID = model.ID,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description
            };
            try
            {
                 _productsRepository.Update(product, t => t.ID == product.ID);
            } //Error ID
            catch
            {
                TempData["ErrorMessage"] = "There was a problem while editing the product.";

            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Product not found";

                return RedirectToAction("Index");
            }
            Product product = _productsRepository.GetById(id.Value);
            ProductsModel model = new ProductsModel
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "Product not found";

                return RedirectToAction("Index");
            }
            Product product = _productsRepository.GetById(id.Value);
            ProductsModel model = new ProductsModel
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _productsRepository.Delete(id);
            } // Error ID
            catch
            {
                TempData["ErrorMessage"] = "There was a problem while deleting the product.";

            }
            return RedirectToAction("Index");
        }
    }
}