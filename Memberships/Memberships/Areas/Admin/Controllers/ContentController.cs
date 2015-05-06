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

namespace Memberships.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Content
        public async Task<ActionResult> Index()
        {
            return View(await db.Contents.ToListAsync());
        }

        // GET: Admin/Content/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = await db.Contents.FindAsync(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // GET: Admin/Content/Create
        public ActionResult Create()
        {
            var model = new Content()
            {
                ContentTypes = db.ContentTypes.ToList(),
                Parts = db.Parts.ToList(),
                Products = db.Products.ToList(),
                Chapters = db.Chapters.ToList()
            };
            return View(model);
        }

        // POST: Admin/Content/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description,Url,HTML,ContentTypeId,ChapterId,PartId")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Contents.Add(content);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(content);
        }

        // GET: Admin/Content/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = await db.Contents.FindAsync(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            content.ContentTypes = await db.ContentTypes.ToListAsync();
            content.Parts = await db.Parts.ToListAsync();
            content.Chapters = await db.Chapters.ToListAsync();
            return View(content);
        }

        // POST: Admin/Content/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description,Url,HTML,ContentTypeId,ChapterId,PartId")] Content content)
        {
            if (ModelState.IsValid)
            {
                db.Entry(content).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(content);
        }

        // GET: Admin/Content/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = await db.Contents.FindAsync(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        // POST: Admin/Content/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Content content = await db.Contents.FindAsync(id);
            db.Contents.Remove(content);
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
