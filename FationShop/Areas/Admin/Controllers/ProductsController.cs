﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FationShop.Areas.Admin.Framework;
using PagedList;

namespace FationShop.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private FashionShopEntities db = new FashionShopEntities();

        // GET: Admin/Products
        public ActionResult Index(string searchString, int? page, int pageLimit = 10)
        {
            //Lưu search string vào viewbag
            ViewBag.searchString = searchString;
            return View(db.Products.Where(x => x.Name.StartsWith(searchString) || searchString == null).ToList().ToPagedList(page ?? 1, pageLimit));
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {

            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile1.FileName);
            string extension = Path.GetExtension(product.ImageFile1.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            product.Image1 = "/Areas/Admin/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Areas/Admin/Image/"), fileName);
            product.ImageFile1.SaveAs(fileName);

            string fileName2 = Path.GetFileNameWithoutExtension(product.ImageFile2.FileName);
            string extension2 = Path.GetExtension(product.ImageFile2.FileName);
            fileName2 = fileName2 + DateTime.Now.ToString("yymmssfff") + extension2;
            product.Image2 = "/Areas/Admin/Image/" + fileName2;
            fileName2 = Path.Combine(Server.MapPath("~/Areas/Admin/Image/"), fileName2);
            product.ImageFile2.SaveAs(fileName2);

            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,MetaTitle,Code,Description,Image1,Image2,MoreImage,Price,PromotionPrice,Quantity,CategoryID,Detail,Status,Size")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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