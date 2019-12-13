using ControleFrotasDLL.BLL;
using ControleFrotasDLL.DAL.Database.SQL;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories
{
   public class SeguroRepository:ISeguroRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        public SeguroRepository(ControleFrotasContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _conf = configuration;
        }


        public async Task Atualizar(Seguro seguro)
        {
            _banco.Update(seguro);
           await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(Seguro seguro)
        {
            _banco.Add(seguro);
          await  _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
            Seguro seguro = ObterSeguro(Id);
            _banco.Remove(seguro);
            await _banco.SaveChangesAsync();
        }

        public Seguro ObterSeguro(int Id)
        {
            return  _banco.Seguros.Find(Id);
        }

        public async Task<IPagedList<Seguro>> ObterTodosSeguros(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoSeguro = _banco.Seguros.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoSeguro = bancoSeguro.Where(a => a.Nome.Contains(pesquisa.Trim()));
            }

            return await bancoSeguro.Include(a => a.Fornecedor).ToPagedListAsync<Seguro>(numeroPagina, RegistroPorPagina);
        }

        public async Task<IPagedList<Seguro>> ObterTodosSegurosPorFornecedor(int? pagina, string rota, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

           
             var bancoSeguro = _banco.Seguros.AsQueryable();

            if (!string.IsNullOrEmpty(rota))
            {
                bancoSeguro = bancoSeguro.Where(a => a.FornecedorId == Convert.ToInt32(rota));
            }
            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoSeguro = bancoSeguro.Where(a => a.Nome.Contains(pesquisa.Trim()));
                bancoSeguro = bancoSeguro.Where(a=>a.FornecedorId==Convert.ToInt32(rota));
            }

            return await bancoSeguro.Include(a => a.Fornecedor).ToPagedListAsync<Seguro>(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<Seguro> ObterTodosSeguros()
        {
            return _banco.Seguros.Include(a=>a.Fornecedor);
        }
    }
}