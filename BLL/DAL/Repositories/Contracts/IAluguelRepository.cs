using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.IntermediarioJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
   public interface IAluguelRepository
    {
        Task<Aluguel> Cadastrar(Aluguel aluguel); 
        Task<IPagedList<Aluguel>> ObterTodosAlugueis(int? pagina);
        Aluguel ObterAluguel(int id);
        IEnumerable<Aluguel> ObterTodosAlugueis();
        void Transacao(Aluguel aluguel, AluguelPagarMe aluguelPagarMe);
    }
}
