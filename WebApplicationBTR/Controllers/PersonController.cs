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
    public class PersonController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Person
        public ActionResult Index()
        {
            IEnumerable<Organization> organizations = db.Organizations.ToList();
            ViewBag.Organizations = organizations;

            IEnumerable<Person> people = db.People.ToList();
            ViewBag.People = people;

            return View(db.People.ToList());
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            IEnumerable<Organization> organizations = db.Organizations.ToList();

            IEnumerable<SelectListItem> itemsOrganizations =

                from value in organizations

                select new SelectListItem

                {

                    Text = value.Name,

                    Value = value.Name,

                };
            ViewData["Organization.Name"] = itemsOrganizations;

            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,Name,Organization")] Person person)
        {
            if (ModelState.IsValid)
            {
                var selectedOrganizationName = Request.Params["Organization.Name"];
                var selectedOrganization = from org in db.Organizations.ToList()
                                           where selectedOrganizationName == org.Name
                                           select org;                
                person.Organization = selectedOrganization.ElementAt(0);
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            IEnumerable<Organization> organizations = db.Organizations.ToList();

            IEnumerable<SelectListItem> itemsOrganizations =

                from value in organizations

                select new SelectListItem

                {

                    Text = value.Name,

                    Value = value.Name,

                };
            ViewData["Organization.Name"] = itemsOrganizations;
            ViewBag.Organizations = db.Organizations.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonId,Name,Organization")] Person person)
        {
            if (ModelState.IsValid)
            {
                
                var selectedOrganizationName = Request.Params["Organization.Name"];
                var selectedOrganization = from org in db.Organizations.ToList()
                                           where selectedOrganizationName == org.Name
                                           select org;

                var name = person.Name;
                person = db.People.Find(person.PersonId);
                person.Organization = selectedOrganization.ElementAt(0);
                person.Name = name;

                db.Entry(person).CurrentValues.SetValues(person);
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            IEnumerable<Organization> organizations = db.Organizations.ToList();

            IEnumerable<SelectListItem> itemsOrganizations =

                from value in organizations

                select new SelectListItem

                {

                    Text = value.Name,

                    Value = value.Name,

                };
            ViewData["Organization.Name"] = itemsOrganizations;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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
