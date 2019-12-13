using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ControleFrotasDLL.BLL.Libraries.Texto
{
    /*
     * Classe usada para permitir a manipulação dos dados de acordo com a necessidade
     */
    public class Mascara
    {
        /*
         * Substitui os seguintes caracteres por outros que serão válidos na situação atual
         */
        public static string Remover(string valor)
        {
            return valor.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace("R$", "").Replace(",", "").Replace(" ", "").Replace("/", "-");
        }

        public static string RemoverData(string valor)
        {
            return valor.Replace("/", "");
        }
        /*
         * PagarMe
         * - O PagarMe recebe o valor no seguinte formato: 3310, que representa R$ 33,10
         */
        public static int ConverterValorPagarMe(double valor)
        {
            string valorString = valor.ToString("C");
            valorString = Remover(valorString);
            int valorInt = int.Parse(valorString);

            return valorInt;
        }

        public static decimal ConverterPagarMeIntToDecimal(int valor)
        {
            //10000 -> "10000" -> "100.00" -> 100.00
            string valorPagarMeString = valor.ToString();
            string valorDecimalString = valorPagarMeString.Substring(0, valorPagarMeString.Length - 2) + "," + valorPagarMeString.Substring(valorPagarMeString.Length - 2);

            var dec = decimal.Parse(valorDecimalString);

            return dec;
        }


    }
}