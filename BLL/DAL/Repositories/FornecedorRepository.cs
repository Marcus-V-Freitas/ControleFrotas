using ControleFrotasDLL.BLL;
using ControleFrotasDLL.DAL.Database.SQL;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories
{
   public class FornecedorRepository:IFornecedorRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        public FornecedorRepository(ControleFrotasContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _conf = configuration;
        }


        public async Task Atualizar(Fornecedor fornecedor)
        {
            _banco.Update(fornecedor);
           await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(Fornecedor fornecedor)
        {
            _banco.Add(fornecedor);
          await  _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
            Fornecedor fornecedor = ObterFornecedor(Id);
            _banco.Remove(fornecedor);
          await  _banco.SaveChangesAsync();
        }

        public Fornecedor ObterFornecedor(int Id)
        {
            return _banco.Fornecedores.Find(Id);
        }

        public async Task<IPagedList<Fornecedor>> ObterTodosFornecedores(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoFornecedor = _banco.Fornecedores.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoFornecedor = bancoFornecedor.Where(a => a.Nome.Contains(pesquisa.Trim()));
            }

            return await bancoFornecedor.ToPagedListAsync<Fornecedor>(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<Fornecedor> ObterTodosFornecedores()
        {
            return _banco.Fornecedores;
        }

        public int QuantidadeTotalFornecedores()
        {
             return  _banco.Fornecedores.Count();
        }
}
}
