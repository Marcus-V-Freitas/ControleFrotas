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
    public class CategoriaVeiculoRepository: ICategoriaVeiculoRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        public CategoriaVeiculoRepository(ControleFrotasContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _conf = configuration;
        }


        public async Task Atualizar(CategoriaVeiculo categoriaVeiculo)
        {
            _banco.Update(categoriaVeiculo);
            await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(CategoriaVeiculo categoriaVeiculo)
        {
            _banco.Add(categoriaVeiculo);
            await _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
            CategoriaVeiculo categoriaVeiculo = ObterCategoriaVeiculo(Id);
            _banco.Remove(categoriaVeiculo);
            await _banco.SaveChangesAsync();
        }

        public CategoriaVeiculo ObterCategoriaVeiculo(int Id)
        {
            return _banco.CategoriaVeiculos.Find(Id);
        }

        public async Task<IPagedList<CategoriaVeiculo>> ObterTodasCategoriasVeiculo(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoCategoriaVeiculo = _banco.CategoriaVeiculos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoCategoriaVeiculo = bancoCategoriaVeiculo.Where(a => a.Nome.Contains(pesquisa.Trim()));
            }

            return await bancoCategoriaVeiculo.ToPagedListAsync<CategoriaVeiculo>(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<CategoriaVeiculo> ObterTodasCategoriasVeiculo()
        {
            return _banco.CategoriaVeiculos.ToList();
        }
    }
}
