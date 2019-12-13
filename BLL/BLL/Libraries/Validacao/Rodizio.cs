using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Validacao
{
    public class Rodizio
    {
        /*
         * Classe usada para verificar de acordo com a data de hoje, 
         * quais são os veículos que estão em rodizio ou não
         */
        public static string Digito1 { get; set; }

        public static string Digito2 { get; set; }

        public static void Dias()
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                Digito1 = "1";
                Digito2 = "2";
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                Digito1 = "3";
                Digito2 = "4";
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
            {
                Digito1 = "5";
                Digito2 = "6";
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                Digito1 = "7";
                Digito2 = "8";
            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                Digito1 = "9";
                Digito2 = "0";
            }
            else
            {
                Digito1 = "Liberado";
                Digito2 = "Liberado";
            }
        }
    }
}
