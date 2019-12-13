using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Validacao.CalculoCPFCNPJ
{
    /*
     * Classe que contém os cálculos para validação do CPF e CNPJ 
     */

    public class CalculoDigitos
    {
        public static int CalcularDigitos(string strings, int[] tamanho)
        {
            int soma = 0;
            for (int indice = strings.Length - 1, digito; indice >= 0; indice--)
            {
                digito = int.Parse(strings.Substring(indice, 1));
                soma += digito * tamanho[tamanho.Length - strings.Length + indice];
            }
            soma = 11 - soma % 11;
            return soma > 9 ? 0 : soma;
        }
    }
}
