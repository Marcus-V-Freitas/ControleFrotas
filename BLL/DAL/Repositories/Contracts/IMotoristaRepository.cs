using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
    public interface IMotoristaRepository
    {
        //CRUD
        Task Cadastrar(Motorista motorista,Cliente cliente);
        Task Atualizar(Motorista motorista, Cliente cliente);
        Task Excluir(int Id);
        Motorista ObterMotorista(int Id); 
        Task<IPagedList<Motorista>> ObterTodosMotoristas(int? pagina,string pesquisa);
        IEnumerable<Motorista> ObterTodosMotoristas();
        int QuantidadeTotalMotoristas();
    }
}
