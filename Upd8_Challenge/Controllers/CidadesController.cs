using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Upd8_Challenge.Models;

namespace Upd8_Challenge.Controllers
{
    public class CidadesController : Controller
    {
        private static string basePath = "https://localhost:7053/api/";

        // GET: Cidades
        public async Task<ActionResult> Index()
        {
            ICollection<CIDADE> cidades = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(basePath + "Cidades");

                if (response.IsSuccessStatusCode)
                    cidades = await response.Content.ReadAsAsync<ICollection<CIDADE>>();
            }

            return View(cidades.ToList());
        }
        

        // GET: Cidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cidades/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CidadeId,CidadeName")] CIDADE cidade)
        {

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(basePath + "Cidades", cidade);

                response.EnsureSuccessStatusCode();
            }


            return RedirectToAction("Index");
        }

        // GET: Cidades/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            CIDADE cidade = new CIDADE();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(basePath + $"Cidades/{id}");

                if (response.IsSuccessStatusCode)
                    cidade = await response.Content.ReadAsAsync<CIDADE>();
            }

            return View(cidade);
        }

        // POST: Cidades/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CidadeId,CidadeName")] CIDADE cidade)
        {

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(basePath + $"Clientes/{cidade.CidadeId}", cidade);

                response.EnsureSuccessStatusCode();
            }

            return RedirectToAction("Index");
        }

        // GET: Cidades/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            CIDADE cidade = new CIDADE();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(basePath + $"Cidades/{id}");

                if (response.IsSuccessStatusCode)
                    cidade = await response.Content.ReadAsAsync<CIDADE>();
            }

            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(basePath + $"Cidades/{id}");

                response.EnsureSuccessStatusCode();
            }

            return RedirectToAction("Index");
        }        
    }
}
