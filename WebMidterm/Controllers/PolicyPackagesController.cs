using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebMidterm.Models;

namespace WebMidterm.Controllers
{
    public class PolicyPackagesController : Controller
    {
        private midtermdbEntities db = new midtermdbEntities();

        // GET: PolicyPackages
        public ActionResult Index()
        {
            //var data = (from p in db.PolicyPackages
            //            join t in db.Transactions
            //            on p.PolicyID equals t.PolicyID into pt
            //            from c in pt.DefaultIfEmpty()
            //            select new TransactionPolicyClass
            //            {
            //                Client = c.Client,
            //                //MonthlyIncome = t.,
            //                //NewTitle = a.Title,
            //                //NewBody = a.Body,
            //                //NewCategoryName = c.CategoryName,
            //                //TERMS = a.TERMS
            //            }).ToList();
            //return View(data);
            //return View(db.Posts.ToList());
             return View(db.PolicyPackages.ToList());
        }

        // GET: PolicyPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PolicyPackage policyPackage = db.PolicyPackages.Find(id);
            if (policyPackage == null)
            {
                return HttpNotFound();
            }
            return View(policyPackage);
        }

        // GET: PolicyPackages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PolicyPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PolicyID,Package,Health,Life,Premium")] PolicyPackage policyPackage)
        {
            if (ModelState.IsValid)
            {
                db.PolicyPackages.Add(policyPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(policyPackage);
        }

        // GET: PolicyPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PolicyPackage policyPackage = db.PolicyPackages.Find(id);
            if (policyPackage == null)
            {
                return HttpNotFound();
            }
            return View(policyPackage);
        }

        // POST: PolicyPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PolicyID,Package,Health,Life,Premium")] PolicyPackage policyPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(policyPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(policyPackage);
        }

        // GET: PolicyPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PolicyPackage policyPackage = db.PolicyPackages.Find(id);
            if (policyPackage == null)
            {
                return HttpNotFound();
            }
            return View(policyPackage);
        }

        // POST: PolicyPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PolicyPackage policyPackage = db.PolicyPackages.Find(id);
            db.PolicyPackages.Remove(policyPackage);
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
