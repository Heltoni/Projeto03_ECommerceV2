using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto03_ECommerce.ViewModel
{
    public class ItensPedidoViewModel
    {
        [Display(Name = "Item")]
        public int CodigoItem { get; set; }

        [Display(Name = "Num. Pedido")]
        public string NumeroPedido { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public double Quantidade { get; set; }

        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        public double ValorTotal { get; set; }

    }
}