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
    public class SubscriptionProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/SubscriptionProduct
        public async Task<ActionResult> Index()
        {
            return View(await GetSubscriptionProductModel());
        }

        // GET: Admin/SubscriptionProduct/Details/5
        public async Task<ActionResult> Details(int subscriptionId, int productId)
        {
            SubscriptionProduct subscriptionProduct = await GetSubscriptionProduct(subscriptionId,productId);
            if (subscriptionProduct == null)
            {
                return HttpNotFound();
            }
            return View(await GetSubscriptionProductModel(subscriptionProduct));
        }

        // GET: Admin/SubscriptionProduct/Create
        public ActionResult Create()
        {
            var model = new SubscriptionProductModel
            { 
                Subscriptions = db.Subcriptions.ToList(), 
                Products = db.Products.ToList() 
            };
            return View(model);
        }

        // POST: Admin/SubscriptionProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,SubscriptionId")] SubscriptionProduct subscriptionProduct)
        {
            if (ModelState.IsValid)
            {
                db.SubscriptionProducts.Add(subscriptionProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(subscriptionProduct);
        }

        // GET: Admin/SubscriptionProduct/Edit/5
        public async Task<ActionResult> Edit(int subscriptionId, int productId)
        {
           
            SubscriptionProduct subscriptionProduct = await GetSubscriptionProduct(subscriptionId, productId);
            if (subscriptionProduct == null)
            {
                return HttpNotFound();
            }
            var model = await GetSubscriptionProductModel(subscriptionId, productId);
            return View(model);
        }

        // POST: Admin/SubscriptionProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,SubscriptionId,OldProductId,OldSubscriptionId")] SubscriptionProduct subscriptionProduct)
        {
            if (ModelState.IsValid)
            {
                var subscriptionProductOld = await GetSubscriptionProduct
                    (subscriptionProduct.OldSubscriptionId, subscriptionProduct.OldProductId);
                var subscriptionProductNew = await GetSubscriptionProduct(subscriptionProduct.SubscriptionId, subscriptionProduct.ProductId);
                if (subscriptionProductOld != null && subscriptionProductNew == null)
                { // Remove the old item db.SubscriptionProduct.Remove(subscriptionProductOld);
                    subscriptionProductNew = new SubscriptionProduct
                    {
                        SubscriptionId = subscriptionProduct.SubscriptionId,
                        ProductId = subscriptionProduct.ProductId
                    };
                    db.SubscriptionProducts.Add(subscriptionProductNew);
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            var model = await GetSubscriptionProductModel
                (subscriptionProduct.OldSubscriptionId, subscriptionProduct.OldProductId);
            return View(model);
            
        }

        // GET: Admin/SubscriptionProduct/Delete/5
        public async Task<ActionResult> Delete(int subscriptionId, int productId)
        {
            SubscriptionProduct subscriptionProduct = await GetSubscriptionProduct(subscriptionId, productId);
            if (subscriptionProduct == null)
            {
                return HttpNotFound();
            }
            return View(await GetSubscriptionProductModel(subscriptionProduct));
        }

        // POST: Admin/SubscriptionProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int subscriptionId, int productId)
        {
            SubscriptionProduct subscriptionProduct = await GetSubscriptionProduct(subscriptionId, productId);
            db.SubscriptionProducts.Remove(subscriptionProduct);
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

        private async Task<SubscriptionProduct> GetSubscriptionProduct(int subscriptionId, int productId)
        {
            return await db.SubscriptionProducts.FirstOrDefaultAsync(pi => pi.ProductId.Equals(productId) && 
                pi.SubscriptionId.Equals(subscriptionId));
        }

        private async Task<SubscriptionProductModel> GetSubscriptionProductModel( int subscriptionId, int productId)
        {
            var model = new SubscriptionProductModel 
            {
                SubscriptionId = subscriptionId, 
                ProductId = productId,
                Subscriptions = await db.Subcriptions.ToListAsync(),
                Products = await db.Products.ToListAsync()
            };
            return model;
        }

        private async Task<SubscriptionProductModel> GetSubscriptionProductModel(SubscriptionProduct subscriptionProduct) 
        {
            var model = new SubscriptionProductModel
            {
                SubscriptionId = subscriptionProduct.SubscriptionId, 
                ProductId = subscriptionProduct.ProductId, 
                SubscriptionTitle = (await db.Subcriptions.FirstOrDefaultAsync(i => i.Id.Equals(subscriptionProduct.SubscriptionId))).Title,
                ProductTitle = (await db.Products.FirstOrDefaultAsync(p => p.Id.Equals(subscriptionProduct.ProductId))).Title
            }; 
            return model;
        }

        private async Task<List<SubscriptionProductModel>> GetSubscriptionProductModel()
        { 
            var model = await (from pi in db.SubscriptionProducts 
                               select new SubscriptionProductModel
                               {
                                   SubscriptionId = pi.SubscriptionId, 
                                   ProductId = pi.ProductId, 
                                   SubscriptionTitle = db.Subcriptions.FirstOrDefault(i => i.Id.Equals(pi.SubscriptionId)).Title,
                                   ProductTitle = db.Products.FirstOrDefault(p => p.Id.Equals(pi.ProductId)).Title 
                               }).ToListAsync(); 
        
            return model; 
        }
    }
}
