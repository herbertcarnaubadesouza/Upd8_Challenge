using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Upd8_Challenge.Models;

namespace Upd8_Challenge.Controllers
{
    public class EstadosController : Controller
    {
        private static string basePath = "https://localhost:7053/api/";

        // GET: Estados
        public async Task<ActionResult> Index()
        {
            ICollection<ESTADO> estados = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(basePath + "Estados");

                if (response.IsSuccessStatusCode)
                    estados = await response.Content.ReadAsAsync<ICollection<ESTADO>>();
            }

            return View(estados.ToList());
        }        

        // GET: Estados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estados/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EstadoId,EstadoName")] ESTADO estado)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(basePath + "Estados", estado);

                response.EnsureSuccessStatusCode();
            }

            return RedirectToAction("Index");
        }

        // GET: Estados/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ESTADO estado = new ESTADO();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(basePath + $"Estados/{id}");

                if (response.IsSuccessStatusCode)
                    estado = await response.Content.ReadAsAsync<ESTADO>();
            }

            return View(estado);
        }

        // POST: Estados/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EstadoId,EstadoName")] ESTADO estado)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(basePath + $"Estados/{estado.EstadoId}", estado);

                response.EnsureSuccessStatusCode();
            }

            return RedirectToAction("Index");
        }

        // GET: Estados/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ESTADO estado = new ESTADO();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(basePath + $"Estados/{id}");

                if (response.IsSuccessStatusCode)
                    estado = await response.Content.ReadAsAsync<ESTADO>();
            }

            return View(estado);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(basePath + $"Estados/{id}");

                response.EnsureSuccessStatusCode();
            }

            return RedirectToAction("Index");
        }       
    }
}
