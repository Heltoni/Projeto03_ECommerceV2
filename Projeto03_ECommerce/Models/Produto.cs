//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projeto03_ECommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Produto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produto()
        {
            this.Itens = new HashSet<Item>();
        }
    
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "A descri��o do produto � obrigat�ria")]
        [Display(Name = "Descri��o")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A unidade � obrigat�ria")]
        [StringLength(5, MinimumLength = 2)]
        public string Unidade { get; set; }

        [Required(ErrorMessage = "O valor unit�rio � obrigat�rio")]

        [DataType(DataType.Currency)]
        [Display(Name = "Valor Unit�rio")]
        [Range(0.0,10000.0, ErrorMessage = "O valor deve estar entre 0 e 10000")]
        public double ValorUnitario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Itens { get; set; }
    }
}