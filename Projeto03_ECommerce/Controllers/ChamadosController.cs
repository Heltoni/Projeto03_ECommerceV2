using Newtonsoft.Json;
using Projeto03_ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Projeto03_ECommerce.Controllers
{
    public class ChamadosController : Controller
    {

        HttpClient client;

        public ChamadosController()
        {
            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:50216/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public List<string> ListaAssuntos()
        {
            return new List<string>
                {
                    "Dúvida",
                    "Reclamação",
                    "Sugestão"
                };
        }

        // GET: Chamados
        public ActionResult Index()
        {
            return View();
        }

        //GET: ABRIR CHAMADO
        [Authorize]
        public ActionResult AbrirChamado()
        {

            ViewBag.ListaAssuntos = new SelectList(ListaAssuntos(), "Assunto");
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AbrirChamado(Chamado chamado)
        {
            try
            {                
                string json = JsonConvert.SerializeObject(chamado);

                //GERANDO O OBJETO QUE REPRESENTA O FLUXO DE BYTES
                HttpContent content = new StringContent(json, Encoding.Unicode, "application/json");

                //ENVIA O OBJETO PARA O WEBSERVICE
                var response = await client.PostAsync("api/chamados", content);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.MensagemSucesso = response.ReasonPhrase;
                    return View("Sucesso");
                }
                else
                {
                    ViewBag.MensagemErro = response.StatusCode + " - " + response.ReasonPhrase;
                    return View("Erro");
                }

            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = ex.Message;
                return View("Erro");
            }
        }

        [Authorize]
        public async Task<ActionResult> ListarChamados()
        {
            HttpResponseMessage response;            
            List<Chamado> chamados = new List<Chamado>();


            using (response = await client.GetAsync("api/chamados"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var ChamadosJsonString = await response.Content.ReadAsStringAsync();
                    chamados = JsonConvert.DeserializeObject<Chamado[]>(ChamadosJsonString).ToList();
                }
                else
                {
                    ViewBag.MensagemErro = response.ReasonPhrase;
                    return View("Erro");
                }
            }

            return View(chamados);
        }

        public async Task<ActionResult> ListarChamados(int? id)
        {
            HttpResponseMessage response;
            Chamado chamado = new Chamado();


            using (response = await client.GetAsync("api/chamados/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var ChamadosJsonString = await response.Content.ReadAsStringAsync();
                    chamado = JsonConvert.DeserializeObject<Chamado>(ChamadosJsonString);
                }
                else
                {
                    ViewBag.MensagemErro = response.ReasonPhrase;
                    return View("Erro");
                }
            }

            return View(chamado);
        }

        [Authorize]
        public ActionResult Responder(int? id)
        {
            if (id == null)
            {
                ViewBag.MensagemErro = "Nenhum parâmetro informado na URL!!!";
                return View("Erro");
            }

            var chamado = ListarChamados(id);
            if (chamado == null)
            {
                ViewBag.MensagemErro = "Chamado não encontrado!!!";
                return View("Erro");
            }
            else
            {
                return View(chamado);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Responder(Chamado chamado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string json = JsonConvert.SerializeObject(chamado);

                    //GERANDO O OBJETO QUE REPRESENTA O FLUXO DE BYTES
                    HttpContent content = new StringContent(json, Encoding.Unicode, "application/json");

                    //ENVIA O OBJETO PARA O WEBSERVICE
                    var response = await client.PostAsync("api/chamados", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListarChamados");
                    }
                    else
                    {
                        ViewBag.MensagemErro = response.StatusCode + " - " + response.ReasonPhrase;
                        return View("Erro");
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.MensagemErro = ex.Message;
                    return View("Erro");
                }
                
            }

            ViewBag.MensagemErro = "Modelo de dados não é válido";
            return View("Erro");
        }
    }
}