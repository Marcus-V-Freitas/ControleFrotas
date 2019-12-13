using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IClienteRepository
    {
        Cliente Login(string email, string senha);

        //CRUD
        void AtualizarSenha(Cliente cliente);
        Cliente ObterCliente(int Id);
        IEnumerable<Cliente> ObterTodosClientes();
        void AtivarDesativar(Cliente cliente);
        List<Cliente> ObterClientePorEmail(string email);
        IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa);
        int QuantidadeTotalClientes();
    }
}
