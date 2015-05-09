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
            return View(await GetProductContentModel());
        }

        // GET: Admin/ProductContent/Details/5
        public async Task<ActionResult> Details(int contentId, int productId)
        {
            
            ProductContent productContent = await GetProductContent(contentId,productId);
            if (productContent == null)
            {
                return HttpNotFound();
            }
            var model = await GetProductContentModel(productContent);
            return View(model);
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
        public async Task<ActionResult> Edit(int contentId, int productId)
        {
            
            ProductContent productContent = await db.ProductContents.FirstOrDefaultAsync(pc => pc.ContentId.Equals(contentId)
                && pc.ProductId.Equals(productId));
            if (productContent == null)
            {
                return HttpNotFound();
            }
            var model = new ProductContentModel
            {
                ContentId = contentId,
                ProductId = productId,
                Contents = await db.Contents.ToListAsync(), 
                Products = await db.Products.ToListAsync() 
            };
            return View(model);
        }

        // POST: Admin/ProductContent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,ContentId,OldProductId,OldContentId")] ProductContent productContent)
        {
            if (ModelState.IsValid)
            {
                var productContentOld = await GetProductContent(productContent.OldContentId, productContent.OldProductId);
                var productContentNew = await GetProductContent(productContent.ContentId, productContent.ProductId);
                if (productContentOld != null && productContentNew == null)
                { // Remove the old item db.ProductContent.Remove(productContentOld);
                    productContentNew = new ProductContent 
                    { 
                        ContentId = productContent.ContentId, 
                        ProductId = productContent.ProductId 
                    };
                    db.ProductContents.Add(productContentNew); await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            var model = await GetProductContentModel(productContent.OldContentId, productContent.OldProductId);
            return View(model);
        }

        // GET: Admin/ProductContent/Delete/5
        public async Task<ActionResult> Delete(int contentId, int productId)
        {
            ProductContent productContent = await GetProductContent(contentId, productId);
            if (productContent == null)
            {
                return HttpNotFound();
            }
            var model = await GetProductContentModel(productContent);
            return View(model);
        }

        // POST: Admin/ProductContent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int contentId, int productId)
        {
            ProductContent productContent = await GetProductContent(contentId, productId); 
            db.ProductContents.Remove(productContent); 
            await db.SaveChangesAsync(); return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<ProductContent> GetProductContent(int contentId, int productId)
        {
            return await db.ProductContents.FirstOrDefaultAsync(pi => pi.ProductId.Equals(productId) && 
                    pi.ContentId.Equals(contentId));
        }

        private async Task<ProductContentModel> GetProductContentModel(int contentId, int productId)
        {
            var productContent = new ProductContentModel 
            {
                ContentId = contentId,
                ProductId = productId, 
                Contents = await db.Contents.ToListAsync(), 
                Products = await db.Products.ToListAsync()
            };
            return productContent;
        }

        private async Task<ProductContentModel> GetProductContentModel(ProductContent productContent)
        { 
            var model = new ProductContentModel ()
            { 
                ContentId = productContent.ContentId, 
                ProductId = productContent.ProductId, 
                ContentTitle = (await db.Contents.FirstOrDefaultAsync(i => i.Id.Equals(productContent.ContentId))).Title, 
                ProductTitle = (await db.Products.FirstOrDefaultAsync(p => p.Id.Equals(productContent.ProductId))).Title 
            };
                return model; 
        }

        private async Task<List<ProductContentModel>> GetProductContentModel()
        {
            var model = await (from pi in db.ProductContents 
                               select new ProductContentModel 
                               {
                                   ContentId = pi.ContentId, 
                                   ProductId = pi.ProductId, 
                                   ContentTitle = db.Contents.FirstOrDefault(i => i.Id.Equals(pi.ContentId)).Title, 
                                   ProductTitle = db.Products.FirstOrDefault(p => p.Id.Equals(pi.ProductId)).Title 
                               }).ToListAsync();
            return model;
        }


    }
}
