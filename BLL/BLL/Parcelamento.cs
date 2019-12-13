using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL
{
    public class Parcelamento
    {
            public int Numero { get; set; }
            public decimal Valor { get; set; }
            public decimal ValorPorParcela { get; set; }
            public bool Juros { get; set; }
        }
    }
