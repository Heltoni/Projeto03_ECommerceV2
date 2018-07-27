using Newtonsoft.Json;
using Projeto03_ECommerce.Db;
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
    public class PagamentosController : Controller
    {

        HttpClient client;

        public PagamentosController()
        {
            if (client==null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:50603/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }


        // GET: Pagamentos
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Efetuar()
        {
            var listaPedidos = Dados.ListarPedidosVM();
            ViewBag.ListaPedidos = new SelectList(listaPedidos, "NumeroPedido", "NomeCliente");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Efetuar(Pagamento pagamento)
        {
            try
            {
                pagamento.ValorPagto = Dados.SomarPedido(pagamento.NumeroPedido);
                //GERANDO UM OBJETO JSON A PARTIR DA INSTÂNCIA PAGAMENTO
                string json = JsonConvert.SerializeObject(pagamento);

                //GERANDO O OBJETO QUE REPRESENTA O FLUXO DE BYTES
                HttpContent content = new StringContent(json, Encoding.Unicode, "application/json");

                //ENVIA O OBJETO PARA O WEBSERVICE
                var response = await client.PostAsync("api/pagamentos", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
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
    }
}