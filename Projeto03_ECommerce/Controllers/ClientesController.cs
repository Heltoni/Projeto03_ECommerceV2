using Projeto03_ECommerce.Db;
using Projeto03_ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto03_ECommerce.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Incluir(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                Dados.IncluirCliente(cliente);
                return RedirectToAction("Listar");
            }
            return View();
        }

        [Authorize]
        public ActionResult Listar()
        {
            var lista = Dados.ListarClientes();
            return View(lista);
        }

        [Authorize]
        public ActionResult Detalhes(int? id)
        {
            if(id == null)
            {
                ViewBag.MensagemErro = "Nenhum parâmetro informado na URL!!!";
                return View("Erro");
            }

            var cliente = Dados.BuscarCliente(id);
            if (cliente == null)
            {
                ViewBag.MensagemErro = "Cliente não encontrado!!!";
                return View("Erro");
            }
            else
            {
                return View(cliente);
            }
        }

        [Authorize]
        public ActionResult Alterar(int? id)
        {
            if (id == null)
            {
                ViewBag.MensagemErro = "Nenhum parâmetro informado na URL!!!";
                return View("Erro");
            }

            var cliente = Dados.BuscarCliente(id);
            if (cliente == null)
            {
                ViewBag.MensagemErro = "Cliente não encontrado!!!";
                return View("Erro");
            }
            else
            {
                return View(cliente);
            }
        }

        [HttpPost]
        public ActionResult Alterar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                Dados.AlterarCliente(cliente);
                return RedirectToAction("Listar");
            }
            return Alterar(cliente.ClienteId);
        }

        [Authorize]
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                ViewBag.MensagemErro = "Nenhum parâmetro informado na URL!!!";
                return View("Erro");
            }

            var cliente = Dados.BuscarCliente(id);
            if (cliente == null)
            {
                ViewBag.MensagemErro = "Cliente não encontrado!!!";
                return View("Erro");
            }
            else
            {
                return View(cliente);
            }
        }

        [HttpPost]
        public ActionResult Excluir(int? id,Cliente cliente)
        {
            cliente.ClienteId = (int)id;
            Dados.RemoverCliente(cliente);
            return RedirectToAction("Listar");
        }

        //Requisições Ajax
        [Authorize]
        public ActionResult PesquisarClientes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PesquisarClientes(string busca)
        {
            List<Cliente> lista = Dados.ListarClientes(busca);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListaClientes", lista);
            }
            else
            {
                return View(lista);
            }            
        }
    }
}