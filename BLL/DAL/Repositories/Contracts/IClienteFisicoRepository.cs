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
    public interface IClienteFisicoRepository
    {
        Task Cadastrar(ClienteFisico clienteFisico, Cliente cliente);
        Task Atualizar(ClienteFisico clienteFisico, Cliente cliente);
        Task Excluir(int Id);
        ClienteFisico ObterClienteFisico(int Id);
        IEnumerable<ClienteFisico> ObterTodosClientesFisicos();
        Task<IPagedList<ClienteFisico>> ObterTodosClientesFisicos(int? pagina, string pesquisa);
    }
}