using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using AutoMapper.Configuration.Annotations;
using Newtonsoft.Json;

namespace rh_admin
{
    
    public  abstract class FuncionarioBaseDto
    {
        
        public virtual  String NumeroChapa { get; set; }
        public virtual  String Nome { get; set; }
        public virtual  String Sobrenome { get; set; }
        public virtual  String Email { get; set; }
        public virtual  String Lider { get; set; }
    }
    
       
    public class FuncionarioQueryDto : FuncionarioBaseDto
    {
    }
    
    public class FuncionarioDto : FuncionarioBaseDto
    {
        [RegularExpression("\\d+")]
        public override String NumeroChapa { get; set; }
        [Required]
        public override String Nome { get; set; }
        [Required]
        public override String Sobrenome { get; set; }
        [EmailAddress]
        [Required]
        public override String Email { get; set; }
        
        [ValidarCadaTelefone]
        public List<String> Telefone { get; set; }
        
        public override String Lider { get; set; }
    }
 
    
    public class FuncionarioRetornoDto : FuncionarioDto
    {
        public DateTime DataCadastro { get; protected set; }
    }
    
    public class FuncionarioSenhaDto : FuncionarioDto
    {
        [Required] public String Senha { get; set; }

    }
    
    public class ValidarCadaTelefone : ValidationAttribute
    {
        protected readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        public override bool IsValid(object value)
        {
            var list = value as IEnumerable<Object>;
            if (list == null) return true;


            foreach (var item in list)
            {
                if (!item.GetType().Equals(typeof(String)))
                {
                    return false;
                };
                var str =  item as String;
                if (!Regex.Match(str, "\\d{10,11}").Success)
                {
                    return false;
                }

            }
            return true;
        }

        // I have ommitted error message formatting
    }
}