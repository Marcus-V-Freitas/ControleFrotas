using ControleFrotasDLL.BLL.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Validacao
{
    /*
     * Classe que valida o formato do Renavam introduzido pelo usuário
     */
    public class Renavam:ValidationAttribute
    {
        private Veiculo _veiculo;

        protected override ValidationResult IsValid(object Renavam, ValidationContext validationContext)
        {
            _veiculo = (Veiculo)validationContext.ObjectInstance;

            if (Renavam == null)
            {
                return new ValidationResult(Mensagem.MSG_E001);
            }

            var apenasDigitos = new Regex(@"[^\d]");
            String _Renavam = apenasDigitos.Replace(Convert.ToString(Renavam), "");

            _veiculo.Renavam = _Renavam.ToUpper();

            if (_Renavam.Length != 11 || Regex.IsMatch(_Renavam, @"^[ a-zA-z á]*$"))
            {
                return new ValidationResult(Mensagem.MSG_E008);
            }

            return ValidationResult.Success;

        }
    }
}
