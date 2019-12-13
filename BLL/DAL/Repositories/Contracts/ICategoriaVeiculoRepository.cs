using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
        public interface ICategoriaVeiculoRepository
    {
        Task Cadastrar(CategoriaVeiculo categoriaVeiculo);
        Task Atualizar(CategoriaVeiculo categoriaVeiculo);
        Task Excluir(int id);
        CategoriaVeiculo ObterCategoriaVeiculo(int id);
        Task<IPagedList<CategoriaVeiculo>> ObterTodasCategoriasVeiculo(int? pagina, string pesquisa);
        IEnumerable<CategoriaVeiculo> ObterTodasCategoriasVeiculo();
    }
}
