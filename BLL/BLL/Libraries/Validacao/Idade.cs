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
    public class Idade : ValidationAttribute
    {
      private Cliente _cliente;

    protected override ValidationResult IsValid(object Nascimento, ValidationContext validationContext)
    {
            _cliente = (Cliente)validationContext.ObjectInstance;

            if (Nascimento == null)
            {
                return new ValidationResult(Mensagem.MSG_E001);
            }
        var apenasDigitos = new Regex(@"[^\d]");
        string tamanho = apenasDigitos.Replace((string)Nascimento, "");


        string[] dataSplit = _cliente.Nascimento.Split('/');

        if (tamanho.Length != 8)
        {
            return new ValidationResult(Mensagem.MSG_E010);
        }

        _cliente.Nascimento = string.Empty;

        for (int i = 2; i >= 0; i--)
        {
           _cliente.Nascimento += (i != 0) ? string.Format(dataSplit[i] + "/") : string.Format(dataSplit[i]);
        }

        if (!DateTime.TryParse(_cliente.Nascimento, out DateTime nascimento))
        {
            return new ValidationResult(Mensagem.MSG_E010);
        }

        nascimento = DateTime.ParseExact(_cliente.Nascimento, "yyyy/MM/dd", CultureInfo.InvariantCulture);

        Int32 Yn = DateTime.Now.Year, Yb = nascimento.Year, Mn = DateTime.Now.Month, Mb = nascimento.Month, Dn = DateTime.Now.Day, Db = nascimento.Day;

        var idade = Yn - Yb + (31 * (Mn - Mb) + (Dn - Db)) / 372;

        if (idade < 21)
        {
            return new ValidationResult(Mensagem.MSG_E014);
        }
        return ValidationResult.Success;
    }
}
}
