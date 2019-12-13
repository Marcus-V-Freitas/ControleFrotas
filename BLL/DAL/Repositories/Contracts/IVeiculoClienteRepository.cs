using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IVeiculoClienteRepository
    {
        //Crud
        Task Cadastrar(VeiculoCliente veiculoCliente, Veiculo veiculo);
        Task Atualizar(VeiculoCliente veiculoCliente, Veiculo veiculo);
        Task Excluir(int Id);
        VeiculoCliente ObterVeiculoCliente(int Id);
        Task<IPagedList<VeiculoCliente>> ObterTodosVeiculosCliente(int? pagina, string pesquisa);
        IEnumerable<VeiculoCliente> ObterTodosVeiculosCliente();
        int QuantidadeTotalVeiculosCliente();
    }
}
