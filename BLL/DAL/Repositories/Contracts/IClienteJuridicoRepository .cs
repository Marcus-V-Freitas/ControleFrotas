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
    public interface IClienteJuridicoRepository
    {
        Task Cadastrar(ClienteJuridico clienteJuridico, Cliente cliente);
        Task Atualizar(ClienteJuridico clienteJuridico, Cliente cliente);
        Task Excluir(int Id);
        ClienteJuridico ObterClienteJuridico(int Id);
        IEnumerable<ClienteJuridico> ObterTodosClientesJuridicos();
        Task<IPagedList<ClienteJuridico>> ObterTodosClientesJuridicos(int? pagina, string pesquisa);
    }
}