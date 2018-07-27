using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto03_ECommerce.Models
{
    public class NovoUsuario
    {

        [Required]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha deve conter no mínimo 8 digitos e no máximo 20")]
        public string Senha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Senha",ErrorMessage = "As senhas não conferem")]
        public string Confirma { get; set; }

    }
}