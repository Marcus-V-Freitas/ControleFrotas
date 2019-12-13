using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IRegistroRepository
    {
        Task Cadastrar(RegistroDespesa registro);
        Task<IPagedList<RegistroDespesa>> ObterTodosRegistros(int? pagina);
        List<RegistroDespesa> ObterTodosRegistros();
        decimal ObterTotalPeriodo(DateTime? inicio, DateTime? fim);
    }
}
