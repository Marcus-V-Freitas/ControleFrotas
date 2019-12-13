using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
   public interface IFaturaAluguelRepository
    {
        Task Cadastrar(FaturaAluguel faturaAluguel);
        Task<IPagedList<FaturaAluguel>> ObterTodasFaturasAlugueis(int? pagina);
        IEnumerable<FaturaAluguel> ObterTodasFaturasAlugueis();
        decimal ValorTotalAlugueis();
        int QuantidadeTotalBoletoBancario();
        int QuantidadeTotalCartaoCredito();
        int QuantidadeTotalAlugueis();
    }
}
