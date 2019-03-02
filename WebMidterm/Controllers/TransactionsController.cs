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
    public class TransactionsController : Controller
    {
        private midtermdbEntities db = new midtermdbEntities();

        // GET: Transactions
        public ActionResult Index()
        {
            var data = (from a in db.Transactions
                        join b in db.PolicyPackages
                        on a.PolicyID equals b.PolicyID into ab
                        from c in ab.DefaultIfEmpty()
                        select new TransactionPolicyClass
                        {
                            TID = a.TID,
                            Client = a.Client,
                            MonthlyIncome = a.AnnualIncome / 12,
                            PolicyPackage = c.Package,
                            MonthlyPremium = a.MonthlyPremium,
                        }).ToList();
            return View(data);
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.packages = GetAllPackages();
            return View();
        }

        private List<SelectListItem> GetAllPackages()
        {
            var data = (from itm in db.PolicyPackages
                        select new SelectListItem
                        {
                            Value = itm.PolicyID.ToString(),
                            Text = itm.Package, // + " - " + itm.PRICE,
                        }).ToList();
            return data;
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TID,PolicyID,Client,AnnualIncome,MonthlyPremium")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                PolicyPackage pol = db.PolicyPackages.Find(transaction.PolicyID);
                // transaction.MonthlyPremium = 123;
                if (pol != null)
                {
                    Transaction ts = new Transaction()
                    {
                        //TID = sale.TID,
                        Client = transaction.Client,
                        AnnualIncome = transaction.AnnualIncome,
                        PolicyID = pol.PolicyID,
                        MonthlyPremium = (transaction.AnnualIncome / 12) * pol.Premium,
                    };
                    db.Transactions.Add(ts);
                    db.SaveChanges();
                    //transaction.PolicyID = pol.PolicyID;
                    //transaction.MonthlyPremium = (transaction.AnnualIncome / 12) * pol.Premium;
                }
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TID,PolicyID,Client,AnnualIncome,MonthlyPremium")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
