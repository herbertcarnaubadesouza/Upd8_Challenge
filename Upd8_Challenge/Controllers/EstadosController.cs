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
    public class EstadosController : Controller
    {
        private upd8_testeEntities db = new upd8_testeEntities();

        // GET: Estados
        public ActionResult Index()
        {
            return View(db.ESTADO.ToList());
        }

        // GET: Estados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTADO eSTADO = db.ESTADO.Find(id);
            if (eSTADO == null)
            {
                return HttpNotFound();
            }
            return View(eSTADO);
        }

        // GET: Estados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estados/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EstadoId,EstadoName")] ESTADO eSTADO)
        {
            if (ModelState.IsValid)
            {
                db.ESTADO.Add(eSTADO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eSTADO);
        }

        // GET: Estados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTADO eSTADO = db.ESTADO.Find(id);
            if (eSTADO == null)
            {
                return HttpNotFound();
            }
            return View(eSTADO);
        }

        // POST: Estados/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstadoId,EstadoName")] ESTADO eSTADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eSTADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eSTADO);
        }

        // GET: Estados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTADO eSTADO = db.ESTADO.Find(id);
            if (eSTADO == null)
            {
                return HttpNotFound();
            }
            return View(eSTADO);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ESTADO eSTADO = db.ESTADO.Find(id);
            db.ESTADO.Remove(eSTADO);
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
