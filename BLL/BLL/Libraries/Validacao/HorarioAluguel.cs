using ControleFrotasDLL.BLL.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Validacao
{
    public class HorarioAluguel : ValidationAttribute
    {
        /*
         * Classe que valida o horário introduzido pelo usuário e o converte para o formato US (yyyy/MM/dd)
         */

        protected override ValidationResult IsValid(object Data, ValidationContext validationContext)
        {
            var model = (Aluguel)validationContext.ObjectInstance;

            if (Data == null)
            {
                return new ValidationResult(Mensagem.MSG_E001);
            }


            var apenasDigitos = new Regex(@"[^\d]");
            string tamanho = apenasDigitos.Replace((string)model.DataPrevista, "");


            string[] dataSplit = model.DataPrevista.Split('/');

            if (tamanho.Length != 8)
            {
                return new ValidationResult(Mensagem.MSG_E010);
            }

            model.DataPrevista = string.Empty;

            for (int i = 2; i >= 0; i--)
            {
                model.DataPrevista += (i != 0) ? string.Format(dataSplit[i] + "/") : string.Format(dataSplit[i]);
            }

            if (!DateTime.TryParse(model.DataPrevista, out DateTime prevista))
            {
                return new ValidationResult(Mensagem.MSG_E010);
            }

            prevista = DateTime.ParseExact(model.DataPrevista, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime inicio = DateTime.ParseExact(DateTime.Now.Date.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture);

            int result = DateTime.Compare(prevista, inicio);
            if (result < 0)
            {
                return new ValidationResult(Mensagem.MSG_E011);
            }

            return ValidationResult.Success;
        }
    }
}
