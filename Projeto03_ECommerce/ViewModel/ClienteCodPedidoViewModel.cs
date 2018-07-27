using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto03_ECommerce.ViewModel
{
    public class ClienteCodPedidoViewModel
    {
        public int PedidoId { get; set; }
        public string PedidoCliente { get; set; }

        public ClienteCodPedidoViewModel(
                int PedidoId, 
                string PedidoCliente)
        {
            this.PedidoId = PedidoId;
            this.PedidoCliente = PedidoCliente;
        }
    }
}