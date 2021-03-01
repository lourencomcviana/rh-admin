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
        // não vai pro dto
        [Required]
        private String Senha { get; set; }
        [Required]
        public DateTime DataCadastro { get; set; }

            /**
             Nome e Sobrenome (obrigatório);
- E-mail válido corporativo (obrigatório);
- Número de chapa (único e obrigatório);
- Telefone (não obrigatório / pode ter mais do que 1);
- Nome do Líder (*um líder também é um funcionário);
- Senha (deve ser armazenada criptografada)
             */
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