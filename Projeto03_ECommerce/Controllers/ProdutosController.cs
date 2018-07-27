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
    public class ProdutosController : Controller
    {
        // GET: Produtos
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Incluir(Produto produto)
        {
            if (ModelState.IsValid)
            {
                Dados.IncluirProduto(produto);
                return RedirectToAction("Listar");
            }
            return View();
        }

        [Authorize]
        public ActionResult Listar()
        {
            return View(Dados.ListarProdutos());
        }

        [Authorize]
        public ActionResult Detalhes(int id)
        {
            return View(Dados.ListarProdutos().Find(p => p.ProdutoId == id));
        }

        [Authorize]
        public ActionResult Alterar(int id)
        {
            return View(Dados.ListarProdutos().Find(p => p.ProdutoId == id));
        }

        [HttpPost]
        public ActionResult Alterar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                Dados.AlterarProduto(produto);
                //RETURN VIEW APRESENTARÁ O ID NA URL
                //RETURN VIEW É PREFERENCIAVEL SER UTILIZADO QUANDO A VIEW NÃO FAZ PARTE DE NENHUM ACTION
                //EX.: PAGINA DE ERRO
                //return View("Listar", Dados.ListarProdutos());                

                return RedirectToAction("Listar");
            }
            return Alterar(produto.ProdutoId);
        }

        [Authorize]
        public ActionResult Excluir(int id)
        {
            //return View(Dados.ListarProdutos().Find(p => p.ProdutoId == id));

            var produto = Dados.ListarProdutos().Find(p => p.ProdutoId == id);
            if (produto == null)
            {
                ViewBag.MensagemErro = "Produto não encontrado!!!";
                return View("Erro");
            }
            else
            {
                return View(produto);
            }
        }

        [HttpPost]
        public ActionResult Excluir(/*int? id,*/ Produto produto)
        {
            try
            {
                //não foi necessário informar a linha abaixo e nem passar parametro id pois alteramos na view
                //produto.ProdutoId = (int)id;
                Dados.ExcluirProduto(produto);
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                List<ItensPedidoViewModel> pedidos = Dados.ListarPedidosProduto(produto.ProdutoId);
                string resposta = "<br/><ul>";
                foreach (var pedido in pedidos)
                {
                    resposta += "<li>" + pedido.NumeroPedido + "</li>";
                }

                ViewBag.MensagemErro = ex.Message +
                    "<br/><strong>Existem pedidos para este produto:</strong>" +
                    resposta;
                ViewBag.VoltarListaProduto = true;
                return View("Erro");
            }
        }
    }
}