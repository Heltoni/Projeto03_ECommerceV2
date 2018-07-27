using Projeto03_ECommerce.Db;
using Projeto03_ECommerce.Models;
using Projeto03_ECommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto03_ECommerce.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult CriarPedido()
        {
            var listaClientes = Dados.ListarClientes();
            ViewBag.Clientes = new SelectList(listaClientes, "ClienteId", "Nome");

            return View();
        }

        [HttpPost]
        public ActionResult CriarPedido(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                Dados.IncluirPedido(pedido);
                return RedirectToAction("Index");
            }
            //é necessário levar a lista de clientes
            //por meio do ViewBag, sempre que for direcionado
            //para a view de cadastro de pedidos.
            return CriarPedido();
        }

        //Listando os pedidos por cliente
        [Authorize]
        public ActionResult ListarPedidos(int? id)
        {
            var listaClientes = Dados.ListarClientes();
            if(listaClientes == null || listaClientes.Count == 0)
            {
                ViewBag.MensagemErro = "Nenhum cliente cadastrado para receber pedidos!";
                return View("Erro");
            }

            ViewBag.Clientes = new 
                SelectList(listaClientes, "ClienteId", "Nome");

            return View(Dados.ListarPedidosLinq(id));
        }

    }
}