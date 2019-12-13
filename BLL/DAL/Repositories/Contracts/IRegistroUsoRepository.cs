using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IRegistroUsoRepository
    {
        Task Cadastrar(RegistroUso registroUso);
        Task Atualizar(RegistroUso registroUso);
        Task Excluir(int Id);
        RegistroUso ObterRegistroUso(int Id);
        Task<IPagedList<RegistroUso>> ObterTodosRegistroUsos(int? pagina, string pesquisa);
        IEnumerable<RegistroUso> ObterTodosRegistroUsos();
    }
}
