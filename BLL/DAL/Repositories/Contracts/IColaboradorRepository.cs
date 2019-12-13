using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories.Contracts
{
   public interface IColaboradorRepository
    {
       Task<Colaborador> Login(string email, string senha);

        //CRUD
        Task Cadastrar(Colaborador colaborador);
        Task AtualizarSenha(Colaborador colaborador);
        Task Atualizar(Colaborador colaborador);
        Task Excluir(int Id);
        Colaborador ObterColaborador(int Id);
        List<Colaborador> ObterColaboradorPorEmail(string email);
        Task<IPagedList<Colaborador>> ObterTodosColaboradores(int? pagina);
        IEnumerable<Colaborador> ObterTodosColaboradores();
        
    }
}
