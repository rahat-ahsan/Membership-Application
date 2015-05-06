using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Memberships.Entities;
using Memberships.Models;
using Memberships.Areas.Admin.Models;

namespace Memberships.Areas.Admin.Controllers
{
    public class ProductContentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ProductContent
        public async Task<ActionResult> Index()
        {
            return View(await db.ProductContents.ToListAsync());
        }

        // GET: Admin/ProductContent/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductContent productContent = await db.ProductContents.FindAsync(id);
            if (productContent == null)
            {
                return HttpNotFound();
            }
            return View(productContent);
        }

        // GET: Admin/ProductContent/Create
        public ActionResult Create()
        {
            var model = new ProductContentModel
            {
                Contents = db.Contents.ToList(),
                Products = db.Products.ToList()
            };
            return View(model);
        }

        // POST: Admin/ProductContent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,ContentId")] ProductContent productContent)
        {
            if (ModelState.IsValid)
            {
                db.ProductContents.Add(productContent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productContent);
        }

        // GET: Admin/ProductContent/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductContent productContent = await db.ProductContents.FindAsync(id);
            if (productContent == null)
            {
                return HttpNotFound();
            }
            return View(productContent);
        }

        // POST: Admin/ProductContent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,ContentId")] ProductContent productContent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productContent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productContent);
        }

        // GET: Admin/ProductContent/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductContent productContent = await db.ProductContents.FindAsync(id);
            if (productContent == null)
            {
                return HttpNotFound();
            }
            return View(productContent);
        }

        // POST: Admin/ProductContent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductContent productContent = await db.ProductContents.FindAsync(id);
            db.ProductContents.Remove(productContent);
            await db.SaveChangesAsync();
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
