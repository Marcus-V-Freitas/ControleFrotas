using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IDespesaRepository
    {
        //Crud
        Task Cadastrar(Despesa despesa);
        Task Atualizar(Despesa despesa);
        Task Excluir(int Id);
        Despesa ObterDespesa(int Id);
        Task<IPagedList<Despesa>> ObterTodasDespesas(int? pagina);
        IEnumerable<Despesa> ObterTodasDespesas();
    }
}
