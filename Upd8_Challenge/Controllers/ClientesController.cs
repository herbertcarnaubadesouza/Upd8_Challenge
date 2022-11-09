using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Upd8_Challenge.Models;

namespace Upd8_Challenge.Controllers
{
    public class ClientesController : Controller
    {
        private static string basePath = "https://localhost:7053/api/";

        // GET: Clientes
        public async Task<ActionResult> Index(string searchByName, string searchByCPF, string searchByDate, string searchBySexo, string searchByAddress, string Estados, string Cidades)
        {
            await InicializarDropDown();

            ICollection<CLIENTE> clientes = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(basePath + string.Format("Clientes/?searchByCPF={0}&searchByName={1}&searchByDate={2}&searchBySexo={3}&searchByAddress={4}&Estados={5}&Cidades={6}", searchByCPF, searchByName, searchByDate, searchBySexo, searchByAddress, Estados, Cidades));

                if (response.IsSuccessStatusCode)
                    clientes = await response.Content.ReadAsAsync<ICollection<CLIENTE>>();
            }

            return View(clientes.ToList());
        }

        // GET: Clientes/Create
        public async Task<ActionResult> Create()
        {
            ICollection<CIDADE> cidades = null;

            ICollection<ESTADO> estados = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage cidadesResponse = await client.GetAsync(basePath + "Cidades");

                HttpResponseMessage estadosResponse = await client.GetAsync(basePath + "Estados");

                if (cidadesResponse.IsSuccessStatusCode && estadosResponse.IsSuccessStatusCode)
                {
                    cidades = await cidadesResponse.Content.ReadAsAsync<ICollection<CIDADE>>();
                    estados = await estadosResponse.Content.ReadAsAsync<ICollection<ESTADO>>();
                }
            }
            ViewBag.CidadeId = new SelectList(cidades, "CidadeId", "CidadeName");
            ViewBag.EstadoId = new SelectList(estados, "EstadoId", "EstadoName");

            return View();
        }

        // POST: Clientes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClienteId,ClienteName,CPF,Birth,Sexo,Endereco,EstadoId,CidadeId")] CLIENTE Cliente)
        {
            ICollection<CIDADE> cidades = null;

            ICollection<ESTADO> estados = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(basePath + "Clientes", Cliente);

                HttpResponseMessage cidadesResponse = await client.GetAsync(basePath + "Cidades");

                HttpResponseMessage estadosResponse = await client.GetAsync(basePath + "Estados");

                if (cidadesResponse.IsSuccessStatusCode && estadosResponse.IsSuccessStatusCode)
                {
                    cidades = await cidadesResponse.Content.ReadAsAsync<ICollection<CIDADE>>();
                    estados = await estadosResponse.Content.ReadAsAsync<ICollection<ESTADO>>();
                }
                response.EnsureSuccessStatusCode();
            }

            ViewBag.CidadeId = new SelectList(cidades, "CidadeId", "CidadeName", Cliente.CidadeId);
            ViewBag.EstadoId = new SelectList(estados, "EstadoId", "EstadoName", Cliente.EstadoId);

            return RedirectToAction("Index");
        }

        // GET: Clientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ICollection<CIDADE> cidades = null;

            ICollection<ESTADO> estados = null;

            CLIENTE cliente = new CLIENTE();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(basePath + $"Clientes/{id}");

                HttpResponseMessage cidadesResponse = await client.GetAsync(basePath + "Cidades");

                HttpResponseMessage estadosResponse = await client.GetAsync(basePath + "Estados");

                if (response.IsSuccessStatusCode && cidadesResponse.IsSuccessStatusCode && estadosResponse.IsSuccessStatusCode)
                {
                    cidades = await cidadesResponse.Content.ReadAsAsync<ICollection<CIDADE>>();
                    estados = await estadosResponse.Content.ReadAsAsync<ICollection<ESTADO>>();
                    cliente = await response.Content.ReadAsAsync<CLIENTE>();
                }
            }

            ViewBag.CidadeId = new SelectList(cidades, "CidadeId", "CidadeName", cliente.CidadeId);
            ViewBag.EstadoId = new SelectList(estados, "EstadoId", "EstadoName", cliente.EstadoId);

            return View(cliente);
        }

        // POST: Clientes/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClienteId,ClienteName,CPF,Birth,Sexo,Endereco,EstadoId,CidadeId")] CLIENTE cliente)
        {

            ICollection<CIDADE> cidades = null;

            ICollection<ESTADO> estados = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(basePath + $"Clientes/{cliente.ClienteId}", cliente);

                HttpResponseMessage cidadesResponse = await client.GetAsync(basePath + "Cidades");

                HttpResponseMessage estadosResponse = await client.GetAsync(basePath + "Estados");

                if (cidadesResponse.IsSuccessStatusCode && estadosResponse.IsSuccessStatusCode)
                {
                    cidades = await cidadesResponse.Content.ReadAsAsync<ICollection<CIDADE>>();
                    estados = await estadosResponse.Content.ReadAsAsync<ICollection<ESTADO>>();
                }

                response.EnsureSuccessStatusCode();
            }

            ViewBag.CidadeId = new SelectList(cidades, "CidadeId", "CidadeName", cliente.CidadeId);
            ViewBag.EstadoId = new SelectList(estados, "EstadoId", "EstadoName", cliente.EstadoId);

            return RedirectToAction("Index");
        }

        // GET: Clientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            CLIENTE cliente = new CLIENTE();

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(basePath + $"Clientes/{id}");

                if (response.IsSuccessStatusCode)
                    cliente = await response.Content.ReadAsAsync<CLIENTE>();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(basePath + $"Clientes/{id}");

                response.EnsureSuccessStatusCode();
            }

            return RedirectToAction("Index");
        }

        public async Task InicializarDropDown()
        {
            List<SelectListItem> estadosDropDown = new List<SelectListItem>();

            List<SelectListItem> cidadesDropDown = new List<SelectListItem>();

            ICollection<CIDADE> cidades = null;

            ICollection<ESTADO> estados = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage cidadesResponse = await client.GetAsync(basePath + "Cidades");

                HttpResponseMessage estadosResponse = await client.GetAsync(basePath + "Estados");

                if (cidadesResponse.IsSuccessStatusCode && estadosResponse.IsSuccessStatusCode)
                {
                    cidades = await cidadesResponse.Content.ReadAsAsync<ICollection<CIDADE>>();
                    estados = await estadosResponse.Content.ReadAsAsync<ICollection<ESTADO>>();
                }
            }            

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
    }
}
