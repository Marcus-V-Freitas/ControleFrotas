using ControleFrotasDLL.BLL;
using ControleFrotasDLL.DAL.Database.SQL;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        public ColaboradorRepository(ControleFrotasContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _conf = configuration;
        }

        public async Task Atualizar(Colaborador colaborador)
        {
           Colaborador banco = ObterColaborador(colaborador.Id);
            banco.Nome = colaborador.Nome;
            banco.Email = colaborador.Email;
            _banco.Entry(banco).Property(a => a.Senha).IsModified = false;
            _banco.Entry(banco).Property(a => a.Tipo).IsModified = false;
            await _banco.SaveChangesAsync();
        }

        public async Task AtualizarSenha(Colaborador colaborador)
        {
            _banco.Update(colaborador);
            _banco.Entry(colaborador).Property(a => a.Nome).IsModified = false;
            _banco.Entry(colaborador).Property(a => a.Email).IsModified = false;
            _banco.Entry(colaborador).Property(a => a.Tipo).IsModified = false;
            await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(Colaborador colaborador)
        {
            _banco.Add(colaborador);
           await _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
            Colaborador colaborador = ObterColaborador(Id);
            _banco.Remove(colaborador);
          await _banco.SaveChangesAsync();

        }

        public async Task<Colaborador> Login(string email, string senha)
        {
            return await _banco.Colaboradores.Where(m => m.Email == email && m.Senha == senha).FirstOrDefaultAsync();
        }

        public Colaborador ObterColaborador(int Id)
        {
            return _banco.Colaboradores.Find(Id);
        }

        public List<Colaborador> ObterColaboradorPorEmail(string email)
        {
            return _banco.Colaboradores.Where(a => a.Email == email).ToList();
        }

        public IEnumerable<Colaborador> ObterTodosColaboradores()
        {
            return _banco.Colaboradores.Where(a => a.Tipo != "G").ToList();
        }

        public async Task<IPagedList<Colaborador>> ObterTodosColaboradores(int? pagina)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;
            return await _banco.Colaboradores.ToPagedListAsync<Colaborador>(numeroPagina, RegistroPorPagina);
        }
    }
}
