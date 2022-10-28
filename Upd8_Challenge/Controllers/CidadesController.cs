using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Upd8_Challenge.Models;

namespace Upd8_Challenge.Controllers
{
    public class CidadesController : Controller
    {
        private upd8_testeEntities db = new upd8_testeEntities();

        // GET: Cidades
        public ActionResult Index()
        {
            return View(db.CIDADE.ToList());
        }

        // GET: Cidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CIDADE cIDADE = db.CIDADE.Find(id);
            if (cIDADE == null)
            {
                return HttpNotFound();
            }
            return View(cIDADE);
        }

        // GET: Cidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cidades/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CidadeId,CidadeName")] CIDADE cIDADE)
        {
            if (ModelState.IsValid)
            {
                db.CIDADE.Add(cIDADE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cIDADE);
        }

        // GET: Cidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CIDADE cIDADE = db.CIDADE.Find(id);
            if (cIDADE == null)
            {
                return HttpNotFound();
            }
            return View(cIDADE);
        }

        // POST: Cidades/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CidadeId,CidadeName")] CIDADE cIDADE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cIDADE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cIDADE);
        }

        // GET: Cidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CIDADE cIDADE = db.CIDADE.Find(id);
            if (cIDADE == null)
            {
                return HttpNotFound();
            }
            return View(cIDADE);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CIDADE cIDADE = db.CIDADE.Find(id);
            db.CIDADE.Remove(cIDADE);
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
