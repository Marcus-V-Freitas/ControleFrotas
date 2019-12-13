using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IUnidMedRepository
    {
        Task Cadastrar(UnidadeMedida medida);
        Task Atualizar(UnidadeMedida medida);
        Task Excluir(int Id);
        UnidadeMedida ObterUnidMed(int Id);
        Task<IPagedList<UnidadeMedida>> ObterTodasUnidMeds(int? pagina, string pesquisa);
        IEnumerable<UnidadeMedida> ObterTodasUnidMeds();
    }
}
