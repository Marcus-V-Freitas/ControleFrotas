using ControleFrotasDLL.BLL.Libraries.IntermediarioJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.ViewModel
{
    /*
     * Classe que reúne outras classes necessárias para que se haja um pagamento
     */
    public class PagamentoViewModel
    {
        public Aluguel Aluguel { get; set; }
        public CartaoCredito CartaoCredito { get; set; }
        public Parcelamento Parcelamento { get; set; }

    }
}
