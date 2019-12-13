using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IVeiculoEmpresaRepository
    {
        //Crud
        Task Cadastrar(VeiculoEmpresa veiculoEmpresa, Veiculo veiculo);
        Task Atualizar(VeiculoEmpresa veiculoEmpresa, Veiculo veiculo);
        Task Excluir(int Id);
        VeiculoEmpresa ObterVeiculoEmpresa(int Id);
        Task<IPagedList<VeiculoEmpresa>> ObterTodosVeiculosEmpresa(int? pagina, string pesquisa);
        Task<IPagedList<VeiculoEmpresa>> ObterTodosVeiculosEmpresaRodizio(int? pagina, string pesquisa);
        IEnumerable<VeiculoEmpresa> ObterTodosVeiculosEmpresa();
        IEnumerable<VeiculoEmpresa> ObterTodosVeiculosEmpresaRodizio();
        void BaixaNoVeiculo(int id);
        int QuantidadeTotalVeiculosEmpresa();
    }
}
