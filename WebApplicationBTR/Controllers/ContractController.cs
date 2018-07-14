using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplicationBTR.Models;

namespace WebApplicationBTR.Controllers
{
    public class ContractController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Contract
        public ActionResult Index()
        {
            IEnumerable<ProductType> productTypes = db.productTypes.ToList();
            ViewBag.ProductTypes = productTypes;

            IEnumerable<Organization> organizations = db.Organizations.ToList();
            ViewBag.Organizations = organizations;

            IEnumerable<Person> people = db.People.ToList();
            ViewBag.People = people;

            IEnumerable<Contract> contracts = db.Contracts.ToList();
            foreach(var contract in contracts)
            {
                contract.InitializeDiscount();
            }
            ViewBag.Contracts = contracts;

            //IEnumerable<ProductType> productTypes = db.productTypes.ToList();
            //ViewBag.ProductTypes = productTypes;

            return View(db.Contracts.ToList());
        }

        // GET: Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: Contract/Create
        public ActionResult Create()
        {
            IEnumerable<ProductType> values = db.productTypes.ToList();

            IEnumerable<SelectListItem> items =

                from value in values

                select new SelectListItem

                {

                    Text = value.Name,

                    Value = value.Name,

                };          
            ViewData["ProductType.Name"] = items;

            IEnumerable<Organization> organizations = db.Organizations.ToList();

            IEnumerable<SelectListItem> itemsOrganizations =

                from value in organizations

                select new SelectListItem

                {

                    Text = value.Name,

                    Value = value.Name,

                };
            ViewData["Person.Organization.Name"] = itemsOrganizations;



            return View();
        }



        // POST: Contract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContractId,Price,ProductType,Organization,Person")] Contract contract)
        {
            
            if (ModelState.IsValid)
            {
                var inPerson = contract.Person;
                contract = new Contract();
                contract.ContractNumber = Convert.ToInt32(Request.Params["ContractNumber"]);
                contract.Price = Convert.ToInt32(Request.Params["Price"]);

                //LINQ добавление
                var productTypes = db.productTypes.ToList();
                var inputProductType = Request.Params["ProductType.Name"];
                var resPT = from n in productTypes
                          where n.Name == inputProductType
                          select n;
                contract.ProductType = resPT.ElementAt(0);

                var people = db.People.ToList();
                var organizations = db.Organizations.ToList();
                var inputPerson = Request.Params["Person.Name"];
                var inputOrganization = Request.Params["Person.Organization.Name"];

                var resOrgns = from n in organizations
                             where inputOrganization == n.Name
                             select n;

                var resOrg = resOrgns.ElementAt(0);

                var resPerson = from n in people
                                where n.Name == inputPerson && resOrg == n.Organization
                                select n;

                contract.Person = inPerson;
                try
                {
                    if (resPerson.ElementAt(0) != null)
                    {
                        contract.Person = resPerson.ElementAt(0);
                        contract.Person.Organization = resOrg;
                    }
                }
                catch (Exception ex)
                {
                    contract.Person.Organization = resOrg;
                }             
                db.Contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Contract/Edit/5
        public ActionResult Edit(int? id)
        {
            IEnumerable<ProductType> values = db.productTypes.ToList();

            IEnumerable<SelectListItem> items =

                from value in values

                select new SelectListItem

                {

                    Text = value.Name,

                    Value = value.Name,

                };
            ViewData["ProductType.Name"] = items;

            IEnumerable<Organization> organizations = db.Organizations.ToList();

            IEnumerable<SelectListItem> itemsOrganizations =

                from value in organizations

                select new SelectListItem

                {

                    Text = value.Name,

                    Value = value.Name,

                };
            ViewData["Person.Organization.Name"] = itemsOrganizations;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }
        [HttpPost]
        public ActionResult RefreshPeopleByOrganization()
        {
            var organizationName = Request.Params["Organization"];
            if (organizationName != null)
            {
                var resOrg = from n in db.Organizations.ToList()
                             where n.Name == organizationName
                             select n;

                IEnumerable<Person> people = db.People.ToList();

                IEnumerable<SelectListItem> itemsPeople =

                    from value in people

                    where value.Organization == resOrg.ElementAt(0)

                    select new SelectListItem
                    {

                        Text = value.Name,

                        Value = value.Name,

                    };
                ViewData["PeopleNames"] = itemsPeople;
            }
            return PartialView();
        }

        // POST: Contract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id,[Bind(Include = "ContractNumber,Price,ProductType,Organization,Person")] Contract contract)
        {           
            if (ModelState.IsValid)
            {
                Contract dbContract = db.Contracts.Find(id);
                var inPerson = contract.Person;
                contract = new Contract();
                contract.ContractNumber = Convert.ToInt32(Request.Params["ContractNumber"]);
                contract.Price = Convert.ToInt32(Request.Params["Price"]);

                //LINQ добавление
                var productTypes = db.productTypes.ToList();
                var inputProductType = Request.Params["ProductType.Name"];
                var resPT = from n in productTypes
                            where n.Name == inputProductType
                            select n;
                contract.ProductType = resPT.ElementAt(0);

                var people = db.People.ToList();
                var organizations = db.Organizations.ToList();
                var inputPerson = Request.Params["Person.Name"];
                var inputOrganization = Request.Params["Person.Organization.Name"];

                var resOrgns = from n in organizations
                               where inputOrganization == n.Name
                               select n;

                var resOrg = resOrgns.ElementAt(0);

                var resPerson = from n in people
                                where n.Name == inputPerson && resOrg == n.Organization
                                select n;

                contract.Person = inPerson;
                try
                {
                    if (resPerson.ElementAt(0) != null)
                    {
                        contract.Person = resPerson.ElementAt(0);
                        contract.Person.Organization = resOrg;
                    }
                }
                catch (Exception ex)
                {
                    contract.Person.Organization = resOrg;
                }
                if(id != null)
                    contract.ContractId = (int)id;


                dbContract.ContractNumber = contract.ContractNumber;
                dbContract.Price = contract.Price;
                dbContract.Person = contract.Person;
                dbContract.ProductType = contract.ProductType;





                
                db.Entry(dbContract).CurrentValues.SetValues(dbContract);
                db.Entry(dbContract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contract contract = db.Contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contract contract = db.Contracts.Find(id);
            db.Contracts.Remove(contract);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
