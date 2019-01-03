using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcExpressAdmin.Models;

namespace MvcExpressAdmin.Controllers
{
    public class rssNewsController : PerSiteController
    {
        private NewsDataContext db = new NewsDataContext();

        // GET: rssNews
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult LoadData()
        {
            try
            {
                //Creating instance of DatabaseContext class
                using (NewsDataContext _context = new NewsDataContext())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

                    //Paging Size (10,20,50,100)  
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all Customer data  
                    var customerData = (from tempcustomer in _context.rssNews
                                        select tempcustomer);

                    //Sorting  
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        customerData = customerData.OrderBy(o=>o.Title);
                    }

                    //Search  
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        customerData = customerData.Where(m => m.Title.Equals(searchValue))  ;
                    }

                    //total number of rows count   
                    recordsTotal = customerData.Count();
                    //Paging   
                    var data = customerData.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data  
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        // GET: rssNews/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rssNew rssNew = db.rssNews.Find(id);
            if (rssNew == null)
            {
                return HttpNotFound();
            }
            return View(rssNew);
        }

        // GET: rssNews/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: rssNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rssID,NewspaperMenuId,Title,IconRss,IconSave,Link,Description,Summary,rssDate,DateInput,HotNews,PopularNews,MoreNews,Effect,MaNV")] rssNew rssNew)
        {
            if (ModelState.IsValid)
            {
                db.rssNews.Add(rssNew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rssNew);
        }

        // GET: rssNews/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rssNew rssNew = db.rssNews.Find(id);
            if (rssNew == null)
            {
                return HttpNotFound();
            }
            return View(rssNew);
        }

        // POST: rssNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rssID,NewspaperMenuId,Title,IconRss,IconSave,Link,Description,Summary,rssDate,DateInput,HotNews,PopularNews,MoreNews,Effect,MaNV")] rssNew rssNew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rssNew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rssNew);
        }

        // GET: rssNews/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            rssNew rssNew = db.rssNews.Find(id);
            if (rssNew == null)
            {
                return HttpNotFound();
            }
            return View(rssNew);
        }

        // POST: rssNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            rssNew rssNew = db.rssNews.Find(id);
            db.rssNews.Remove(rssNew);
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
