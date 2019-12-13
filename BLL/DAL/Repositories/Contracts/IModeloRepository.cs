using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IModeloRepository
    {
        //Crud
        Task Cadastrar(Modelo modelo);
        Task Atualizar(Modelo modelo);
        Task Excluir(int Id);
        Modelo ObterModelo(int Id);
        Task<IPagedList<Modelo>> ObterTodosModelos(int? pagina,string pesquisa);
        IEnumerable<Modelo> ObterTodosModelos();
    }
}
