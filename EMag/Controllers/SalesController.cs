using DataAccess;
using EMag.Helpers;
using EMag.Models.Sales;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMag.Controllers
{
    [CustomAutorize]
    [AdminAuthorize]
    public class SalesController : Controller
    {
        private readonly ProductsRepository _productsRepository;
        private readonly SalesRepository _salesRepository;
        public SalesController()
        {
            _productsRepository = new ProductsRepository();
            _salesRepository = new SalesRepository();
        }
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            ListSalesVM model = new ListSalesVM
            {
                Sales = new List<SalesModel>()
            };

            List<Sale> sales = _salesRepository.GetAll();

            foreach (var sale in sales)
            {
                SalesModel p = new SalesModel
                {
                    ID = sale.ID,
                    CustomerFullName = sale.Customer.FirstName + " " + sale.Customer.LastName,
                    ProductName = sale.Product.Name,
                    Price = sale.Price,
                    Quantity = sale.Quantity,
                    Date = sale.Date
                };
                model.Sales.Add(p);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Buy(BuyModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "There was a problem while buying that item.";
                return RedirectToAction("Index", "Products");
            }
            Product product = _productsRepository.GetById(model.ProductID);
            Sale sale = new Sale
            {
                ProductID = model.ProductID,
                CustomerID = SessionHelper.Current.UserID,
                Quantity = model.Quantity,
                Date = DateTime.Now,
                Price = product.Price * model.Quantity
            };
            _salesRepository.Create(sale);
            return RedirectToAction("Index", "Products");
        }
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMessage"] = "There was a problem while showing the details.";
                return RedirectToAction("Index");
            }
            Sale sale = _salesRepository.GetById(id.Value);
            SalesModel model = new SalesModel
            {
                CustomerFullName = sale.Customer.FirstName + " " + sale.Customer.LastName,
                Date = sale.Date,
                ID = sale.ID,
                Price = sale.Price,
                ProductName = sale.Product.Name,
                Quantity = sale.Quantity
            };
            return View(model);
        }
    }
}