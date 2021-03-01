using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rh_admin.Models
{
    public class Funcionario
    {
        [RegularExpression("\\d+")] [Key] public string NumeroChapa { get; set; }

        [Required] public string Nome { get; set; }

        [Required] public string Sobrenome { get; set; }


        [Required] public string Email { get; set; }

        public ICollection<Telefone> Telefones { get; set; }

        public Funcionario Lider { get; set; }

        [Required] public string Senha { get; set; }

        [Required] public string Salt { get; set; }

        [Required] public DateTime DataCadastro { get; set; }
    }

    public class Telefone
    {
        /**
         * ddd+telefone
         */
        [RegularExpression("\\d{10,11}")]
        [Key]
        public string Numero { get; set; }

        [Required] public Funcionario Funcionario { get; set; }
    }
}