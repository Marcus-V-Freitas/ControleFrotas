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
     * Classe que valida o formato da Placa introduzido pelo usuário
     */

   public class Placa:ValidationAttribute
    {
        private Veiculo _veiculo;

        protected override ValidationResult IsValid(object Placa, ValidationContext validationContext)
        {
             _veiculo = (Veiculo)validationContext.ObjectInstance;

            if (Placa == null)
            {
                return new ValidationResult(Mensagem.MSG_E001);
            }

            var apenasDigitos = new Regex(@"[^0-9a-zA-Z]+");
            _veiculo.Placa = apenasDigitos.Replace(Convert.ToString(Placa), "");

            string _Placa = _veiculo.Placa;
            _veiculo.Placa = _veiculo.Placa.ToUpper();

            if (_Placa.Length != 7 || Regex.IsMatch(_Placa.Substring(0, 3), "[0-9]+") || Regex.IsMatch(_Placa.Substring(3), @"^[ a-zA-z á]*$"))
            {
                return new ValidationResult(Mensagem.MSG_E007);
            }
            return ValidationResult.Success;

        }
    }
}
