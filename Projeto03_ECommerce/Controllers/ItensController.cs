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
    public class ItensController : Controller
    {
        // GET: Itens
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult IncluirItens()
        {
            ViewBag.Pedidos = new SelectList(
                Dados.ListarTodosPedidosVM(), "PedidoId", "PedidoCliente");

            ViewBag.Produtos = new SelectList(
                Dados.ListarProdutos(), "ProdutoId","Descricao");

            return View();
        }

        [HttpPost]
        public ActionResult IncluirItens(Item item)
        {
            if (ModelState.IsValid)
            {
                Dados.IncluirItem(item);
                return RedirectToAction("Index");
            }
            return IncluirItens();
        }
        [Authorize]
        public ActionResult ListarItens(int? id)
        {
            ViewBag.Pedidos = new SelectList(
                Dados.ListarTodosPedidosVM(), "PedidoId", "PedidoCliente");

            return View(Dados.ListarItens(id));
        }

        //Requisição Ajax
        [Authorize]
        public ActionResult ListarItensAjax(int? id)
        {
            List<ItensPedidoViewModel> lista = Dados.ListarItens(id);
            if (Request.IsAjaxRequest())
            {                
                return PartialView("_ListaItens", lista);
            }
            else
            {
                ViewBag.Pedidos = new SelectList(
                    Dados.ListarTodosPedidosVM(), "PedidoId", "PedidoCliente");
                return View();
            }
        }
    }
}