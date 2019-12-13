using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    //Usar X.PageList
    public interface IMarcaRepository
    {
        //Crud
        Task Cadastrar(Marca marca);
        Task Atualizar(Marca marca);
        Task Excluir(int Id);
        Marca ObterMarca(int Id);
        Task<IPagedList<Marca>> ObterTodasMarcas(int? pagina, string pesquisa);
        IEnumerable<Marca> ObterTodasMarcas();
    }
}
