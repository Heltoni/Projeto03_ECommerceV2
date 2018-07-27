using Projeto03_ECommerce.Models;
using Projeto03_ECommerce.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projeto03_ECommerce.Db
{
    public class Dados
    {
    #region Metodos para manipulação dos clientes
        //método para incluir um novo cliente
        public static void IncluirCliente(Cliente cliente)
        {
            using (var ctx = new ECommerceEntities())
            {
                ctx.Clientes.Add(cliente);
                ctx.SaveChanges();
            }
        }

        //método para listar todos os clientes
        public static List<Cliente> ListarClientes()
        {
            using(var ctx = new ECommerceEntities())
            {
                return ctx.Clientes.ToList<Cliente>();
            }
        }

        public static List<Cliente> ListarClientes(string busca)
        {
            using (var ctx = new ECommerceEntities())
            {
                if (string.IsNullOrEmpty(busca))
                {
                    return ListarClientes();
                }
                else
                {
                    return ctx.Clientes
                        .Where(c => c.Nome.Contains(busca))
                        .ToList<Cliente>();
                }                
            }
        }


        //Linq -> Language Integrated Query
        public static List<Cliente> ListarClientesLinq()
        {
            var ctx = new ECommerceEntities();
            return (from c in ctx.Clientes select c).ToList<Cliente>();            
        }


        //método para retornar um Cliente com base no ClienteId
        public static Cliente BuscarCliente(int? id)
        {
            using(var ctx = new ECommerceEntities())
            {
                return ctx
                    .Clientes
                    .Where(s => s.ClienteId == id)
                    .FirstOrDefault();
            }
        }

        //método para alterar o cliente
        public static void AlterarCliente(Cliente cliente)
        {
            using(var ctx = new ECommerceEntities())
            {
                ctx.Entry<Cliente>(cliente).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void ExcluirProduto(Produto produto)
        {
            try
            {
                using (var ctx = new ECommerceEntities())
                {
                    ctx.Entry<Produto>(produto).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Produto não pode ser excluído!");
            }
        }

        //método para remover o cliente
        public static void RemoverCliente(Cliente cliente)
        {
            using(var ctx = new ECommerceEntities())
            {
                ctx.Entry<Cliente>(cliente).State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }
        #endregion

    #region Métodos para manipulação dos produtos
        //método para incluir um novo produto
        public static void IncluirProduto(Produto produto)
        {
            using(var ctx = new ECommerceEntities())
            {
                ctx.Produtos.Add(produto);
                ctx.SaveChanges();
            }
        }

        //método para listar todos os produtos
        public static List<Produto> ListarProdutos()
        {
            using(var ctx = new ECommerceEntities())
            {
                return ctx.Produtos.ToList<Produto>();
            }
        }

        //método para alterar um produto
        public static void AlterarProduto(Produto produto)
        {
            using(var ctx = new ECommerceEntities())
            {
                ctx.Entry<Produto>(produto).State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }

    #endregion

    #region Métodos para manipulação dos pedidos
        public static void IncluirPedido(Pedido pedido)
        {
            using(var ctx = new ECommerceEntities())
            {
                ctx.Pedidos.Add(pedido);
                ctx.SaveChanges();
            }
        }

        public static List<ClientePedidoViewModel> ListarPedidosLinq(int? id)
        {
            var ctx = new ECommerceEntities();

            var resultado = from c in ctx.Clientes
                            join p in ctx.Pedidos
                            on c.ClienteId equals p.ClienteId
                            where (id == null ? c.ClienteId > 0 : c.ClienteId == id)
                            select new
                            {
                                CodigoCliente = c.ClienteId,
                                NomeCliente = c.Nome,
                                DataPedido = p.DataPedido,
                                NumeroPedido = p.NumeroPedido
                            };

            List<ClientePedidoViewModel> lista =
                new List<ClientePedidoViewModel>();
            foreach (var item in resultado)
            {
                lista.Add(new ClientePedidoViewModel(
                    item.NomeCliente,
                    item.DataPedido,
                    item.NumeroPedido));
            }
            return lista;
        }

        //METODO UTILIZADO PARA EFETUAR PAGAMENTOS NO WEBSERVICE
        public static List<ClientePedidoViewModel> ListarPedidosVM()
        {
            using(var ctx = new ECommerceEntities())
            {
                var resultado = ctx.Clientes
                    .Join(
                        ctx.Pedidos,
                        c => c.ClienteId,
                        p => p.ClienteId,
                        (c, p) => new
                        {
                            CodigoCliente = c.ClienteId,
                            NomeCliente = c.Nome + " - " + p.NumeroPedido,
                            DataPedido = p.DataPedido,
                            NumeroPedido = p.NumeroPedido
                        });

                List<ClientePedidoViewModel> lista = 
                    new List<ClientePedidoViewModel>();
                foreach (var item in resultado)
                {
                    lista.Add(new ClientePedidoViewModel(
                        item.NomeCliente, 
                        item.DataPedido, 
                        item.NumeroPedido));
                }
                return lista;
            }
        }

        public static List<ClienteCodPedidoViewModel> ListarTodosPedidosVM()
        {
            using (var ctx = new ECommerceEntities())
            {
                var resultado = ctx.Clientes
                    .Join(
                        ctx.Pedidos,
                        c => c.ClienteId,
                        p => p.ClienteId,
                        (c, p) => new
                        {
                            PedidoId = p.PedidoId,
                            PedidoCliente = c.Nome + " - " + p.NumeroPedido
                        });

                List<ClienteCodPedidoViewModel> lista =
                    new List<ClienteCodPedidoViewModel>();
                foreach (var item in resultado)
                {
                    lista.Add(new ClienteCodPedidoViewModel(
                        item.PedidoId,
                        item.PedidoCliente));
                }
                return lista;
            }
        }
        #endregion

    #region Métodos para manipulação dos itens dos pedidos
        public static void IncluirItem(Item item)
        {
            using(var ctx = new ECommerceEntities())
            {
                ctx.Itens.Add(item);
                ctx.SaveChanges();
            }
        }

        public static List<ItensPedidoViewModel> ListarItens(int? idPedido)
        {
            var ctx = new ECommerceEntities();

            var lista = from item in ctx.Itens
                        join pedido in ctx.Pedidos
                        on item.PedidoId equals pedido.PedidoId
                        join produto in ctx.Produtos
                        on item.ProdutoId equals produto.ProdutoId
                        where (idPedido == null ? pedido.PedidoId > 0
                            : pedido.PedidoId == idPedido)
                        select new
                        {
                            CodigoItem = item.ItemId,
                            NumeroPedido = pedido.NumeroPedido,
                            Descricao = produto.Descricao,
                            Quantidade = item.Quantidade,
                            ValorTotal = produto.ValorUnitario * item.Quantidade
                        };
            List<ItensPedidoViewModel> pedidos = 
                new List<ItensPedidoViewModel>();

            foreach (var item in lista)
            {
                pedidos.Add(new ItensPedidoViewModel()
                {
                    CodigoItem = item.CodigoItem,
                    NumeroPedido = item.NumeroPedido,
                    Descricao = item.Descricao,
                    Quantidade = item.Quantidade,
                    ValorTotal = item.ValorTotal
                });
            }

            return pedidos;
        }

        public static List<ItensPedidoViewModel> ListarPedidosProduto(int? idProduto)
        {
            var ctx = new ECommerceEntities();

            var lista = from item in ctx.Itens
                        join pedido in ctx.Pedidos
                        on item.PedidoId equals pedido.PedidoId
                        join produto in ctx.Produtos
                        on item.ProdutoId equals produto.ProdutoId                        
                        where (idProduto == null ? produto.ProdutoId > 0
                            : produto.ProdutoId == idProduto)
                        select new
                        {                            
                            NumeroPedido = pedido.NumeroPedido                            
                        };
            List<ItensPedidoViewModel> pedidos =
                new List<ItensPedidoViewModel>();

            foreach (var item in lista)
            {
                pedidos.Add(new ItensPedidoViewModel()
                {
                    NumeroPedido = item.NumeroPedido
                });
            }

            return pedidos;
        }

        //METODO PARA RETORNAR A SOMA DOS ITENS DE UM PEDIDO
        public static double SomarPedido(string numPedido)
        {
            var ctx = new ECommerceEntities();
            var somaPedido = (from pedido in ctx.Pedidos
                              join item in ctx.Itens
                              on pedido.PedidoId equals item.PedidoId
                              join produto in ctx.Produtos
                              on item.ProdutoId equals produto.ProdutoId
                              where pedido.NumeroPedido.Equals(numPedido)
                              select new
                              {
                                  Valor = produto.ValorUnitario * item.Quantidade
                              }
                              ).Sum(v => v.Valor);

            return somaPedido;
        }

        #endregion
    }
}