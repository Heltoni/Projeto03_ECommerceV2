using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto03_ECommerce.ViewModel
{
    public class ClientePedidoViewModel
    {
        [Required]
        [Display(Name ="Cliente")]
        public string NomeCliente { get; set; }

        [Required]
        [Display(Name = "Data do Pedido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataPedido { get; set; }

        [Required]
        [Display(Name = "Num. Pedido")]
        public string NumeroPedido { get; set; }

        public ClientePedidoViewModel(
                            string NomeCliente, 
                            DateTime DataPedido, 
                            string NumeroPedido)
        {
            this.NomeCliente = NomeCliente;
            this.DataPedido = DataPedido;
            this.NumeroPedido = NumeroPedido;
        }
    }
}