using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto03_ECommerce.Models
{
    public class Pagamento
    {
        public int PagamentoID { get; set; }

        [Required]
        [Display(Name = "Número do Cartão")]
        public string NumeroCartao { get; set; }

        [Display(Name = "Número do Pedido")]
        public string NumeroPedido { get; set; }

        [Display(Name = "Valor do Pagamento")]
        public double ValorPagto { get; set; }        
    }
}