using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface ISeguroRepository
    {
        Task Cadastrar(Seguro seguro);
        Task Atualizar(Seguro seguro);
        Task Excluir(int Id);
        Seguro ObterSeguro(int Id);
        Task<IPagedList<Seguro>> ObterTodosSegurosPorFornecedor(int? pagina, string rota, string pesquisa);
        Task<IPagedList<Seguro>> ObterTodosSeguros(int? pagina, string pesquisa);
        IEnumerable<Seguro> ObterTodosSeguros();
    }
}