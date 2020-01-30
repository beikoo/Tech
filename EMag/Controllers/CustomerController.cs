using DataAccess;
using EMag.Helpers;
using EMag.Models;
using EMag.Models.Customer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EMag.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomersRepository customersRepository;
        public CustomerController()
        {
            customersRepository = new CustomersRepository();
        }

        public ActionResult Login()
        {
            if (SessionHelper.Current.IsAuthenticated)
            {
                return RedirectToAction("Index", "Products");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {        
            if (customersRepository.Authenticate(model.Username, HashPass(model.Password)))
            {
                Customer customer = customersRepository.GetUserByUsername(model.Username);
                SessionHelper.Current.SetCurrentUser(customer.ID, customer.Username, customer.IsAdmin);          
                return RedirectToAction("Index", "Products");
            }
            return RedirectToAction("Login");
        }

        public ActionResult Create()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            if (SessionHelper.Current.IsAuthenticated)
            {
                return RedirectToAction("Index", "Products");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "There was a problem with creating the user, please contact Administrator.";
                return RedirectToAction("Create");
            }
            Customer customer = new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Username = model.Username,
                Password = HashPass(model.Password)
            };
            try
            {
                customersRepository.Create(customer);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "There was a problem with creating the user, please contact Administrator.";
                return RedirectToAction("Create");
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            SessionHelper.Current.Logout();
            return RedirectToAction("Login");
        }

        public string HashPass(string password)
        {
            MD5 md5 = MD5.Create();
            var data = Encoding.ASCII.GetBytes(password);
            var hash = md5.ComputeHash(data);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}