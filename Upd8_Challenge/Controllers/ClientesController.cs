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
    public class ClientesController : Controller
    {
        private upd8_testeEntities db = new upd8_testeEntities();

        // GET: Clientes
        public ActionResult Index(string searchByName, string searchByCPF, string searchByDate, string searchBySexo, string searchByAddress, string Estados, string Cidades)
        {
            InicializarDropDown();

            var clientes = db.CLIENTE.Include(c => c.CIDADE).Include(c => c.ESTADO).ToList();

            List<CLIENTE> filteredClientes = new List<CLIENTE>();


            if (clientes.Count() > 0)
            {
                if (!string.IsNullOrEmpty(searchByName) || !string.IsNullOrEmpty(searchByCPF) || !string.IsNullOrEmpty(searchByDate) || !string.IsNullOrEmpty(searchBySexo) || !string.IsNullOrEmpty(searchByAddress) || !string.IsNullOrEmpty(Estados) || !string.IsNullOrEmpty(Cidades))
                {

                    var query = "SELECT * FROM CLIENTE WHERE 1 = 1";

                    if (!string.IsNullOrEmpty(searchByName))
                    {
                        query += $" AND ClienteName = '{searchByName}'";
                    }
                    if (!string.IsNullOrEmpty(searchByCPF))
                    {
                        query += $" AND CPF = '{searchByCPF}'";
                    }
                    if (!string.IsNullOrEmpty(searchByDate))
                    {
                        var teste1 = DateTime.Parse(searchByDate);
                        query += $" AND Birth = '{DateTime.Parse(searchByDate)}'";
                    }
                    if (!string.IsNullOrEmpty(searchBySexo))
                    {
                        query += $" AND Sexo = '{ searchBySexo}'";
                    }

                    if (!string.IsNullOrEmpty(searchByAddress))
                    {
                        query += $" AND Endereco = '{searchByAddress}'";
                    }

                    if (!string.IsNullOrEmpty(Estados))
                    {
                        query += $" AND EstadoId = '{ Estados}'";                       
                    }

                    if (!string.IsNullOrEmpty(Cidades))
                    {
                        query += $" AND CidadeId = '{Cidades}'";
                    }

                    var teste = db.CLIENTE.SqlQuery(query).ToList();

                    return View(teste);
                }

                return View(clientes.ToList());
            }
            return View(clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTE);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            ViewBag.CidadeId = new SelectList(db.CIDADE, "CidadeId", "CidadeName");
            ViewBag.EstadoId = new SelectList(db.ESTADO, "EstadoId", "EstadoName");
            return View();
        }

        // POST: Clientes/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,ClienteName,CPF,Birth,Sexo,Endereco,EstadoId,CidadeId")] CLIENTE cLIENTE)
        {
            if (ModelState.IsValid)
            {
                db.CLIENTE.Add(cLIENTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CidadeId = new SelectList(db.CIDADE, "CidadeId", "CidadeName", cLIENTE.CidadeId);
            ViewBag.EstadoId = new SelectList(db.ESTADO, "EstadoId", "EstadoName", cLIENTE.EstadoId);
            return View(cLIENTE);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.CIDADE, "CidadeId", "CidadeName", cLIENTE.CidadeId);
            ViewBag.EstadoId = new SelectList(db.ESTADO, "EstadoId", "EstadoName", cLIENTE.EstadoId);
            return View(cLIENTE);
        }

        // POST: Clientes/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,ClienteName,CPF,Birth,Sexo,Endereco,EstadoId,CidadeId")] CLIENTE cLIENTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLIENTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.CIDADE, "CidadeId", "CidadeName", cLIENTE.CidadeId);
            ViewBag.EstadoId = new SelectList(db.ESTADO, "EstadoId", "EstadoName", cLIENTE.EstadoId);
            return View(cLIENTE);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            if (cLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTE);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            db.CLIENTE.Remove(cLIENTE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public void InicializarDropDown()
        {
            List<SelectListItem> estadosDropDown = new List<SelectListItem>();

            List<SelectListItem> cidadesDropDown = new List<SelectListItem>();

            var estados = db.ESTADO.ToList();

            var cidades = db.CIDADE.ToList();

            foreach (var estado in estados)
            {
                estadosDropDown.Add(new SelectListItem { Text = estado.EstadoName, Value = estado.EstadoId.ToString() });
            }

            foreach (var cidade in cidades)
            {
                cidadesDropDown.Add(new SelectListItem { Text = cidade.CidadeName, Value = cidade.CidadeId.ToString() });
            }

            ViewBag.Cidades = cidadesDropDown;

            ViewBag.Estados = estadosDropDown;
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
