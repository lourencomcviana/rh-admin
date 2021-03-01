using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace rh_admin.Models
{
    public class Funcionario
    {
        [RegularExpression("\\d+")]
        [Key]
        public String NumeroChapa { get; set; }
        [Required]
        public String Nome { get; set; }
        [Required]
        public String Sobrenome { get; set; }
        
        
        [Required]
        public String Email { get; set; }
        
        public ICollection<Telefone> Telefones { get; set; } 
        
        public Funcionario Lider { get; set; }

        [Required]
        public String Senha { get; set; }
        
        [Required]
        public String Salt { get; set; }
        
        [Required]
        public DateTime DataCadastro { get; set; }

    }
    
    public class Telefone
    {
        /**
         * ddd+telefone
         */
        [RegularExpression("\\d{10,11}")]
        [Key]
        public String Numero { get; set; }
        [Required]
        public Funcionario Funcionario { get; set; }
        
    }
}