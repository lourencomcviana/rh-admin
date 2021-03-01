using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace rh_admin.Dtos
{
    public abstract class FuncionarioBaseDto
    {
        public virtual string NumeroChapa { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Sobrenome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Lider { get; set; }
    }


    public class FuncionarioQueryDto : FuncionarioBaseDto
    {
    }

    public class FuncionarioDto : FuncionarioBaseDto
    {
        [RegularExpression("\\d+")] public override string NumeroChapa { get; set; }

        [Required] public override string Nome { get; set; }

        [Required] public override string Sobrenome { get; set; }

        [EmailAddress] [Required] public override string Email { get; set; }

        [ValidarCadaTelefone] public List<string> Telefone { get; set; }

        public override string Lider { get; set; }
    }


    public class FuncionarioRetornoDto : FuncionarioDto
    {
        public DateTime DataCadastro { get; protected set; }
    }

    public class FuncionarioCreateDto : FuncionarioDto
    {
        [Required] public string Senha { get; set; }
    }

    public class ValidarCadaTelefone : ValidationAttribute
    {
        protected readonly List<ValidationResult> validationResults = new List<ValidationResult>();

        public override bool IsValid(object value)
        {
            var list = value as IEnumerable<object>;
            if (list == null) return true;


            foreach (var item in list)
            {
                if (!item.GetType().Equals(typeof(string))) return false;
                ;
                var str = item as string;
                if (!Regex.Match(str, "\\d{10,11}").Success) return false;
            }

            return true;
        }

        // I have ommitted error message formatting
    }
}