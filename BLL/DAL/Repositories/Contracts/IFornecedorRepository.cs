using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IFornecedorRepository
    {
        //CRUD
        Task Cadastrar(Fornecedor fornecedor);
        Task Atualizar(Fornecedor fornecedor);
        Fornecedor ObterFornecedor(int id);
        Task Excluir(int id);
        Task<IPagedList<Fornecedor>> ObterTodosFornecedores(int? pagina, string pesquisa);
        IEnumerable<Fornecedor> ObterTodosFornecedores();
        int QuantidadeTotalFornecedores();
    }
}
