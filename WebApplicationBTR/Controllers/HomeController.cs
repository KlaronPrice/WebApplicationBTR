using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationBTR.Models;
namespace WebApplicationBTR.Controllers
{
    public class HomeController : Controller
    {
        ShopContext db = new ShopContext();
        public ActionResult Index()
        {          
            IEnumerable<ProductType> productTypes = db.productTypes.ToList();
            ViewBag.ProductTypes = productTypes;

            IEnumerable<Organization> organizations = db.Organizations.ToList();
            ViewBag.Organizations = organizations;

            IEnumerable<Person> people = db.People.ToList();
            ViewBag.People = people;

            IEnumerable<Contract> contracts = db.Contracts.ToList();
            ViewBag.Contracts = contracts;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}