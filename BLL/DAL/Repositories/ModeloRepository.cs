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
    public class ModeloRepository : IModeloRepository
    {
        private ControleFrotasContext _banco;
        private IConfiguration _conf;

        public ModeloRepository(ControleFrotasContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _conf = configuration;
        }


        public async Task Atualizar(Modelo modelo)
        {
            _banco.Update(modelo);
           await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(Modelo modelo)
        {
            _banco.Add(modelo);
           await _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
           Modelo modelo = ObterModelo(Id);
            _banco.Remove(modelo);
           await _banco.SaveChangesAsync();
        }

        public Modelo ObterModelo(int Id)
        {
            return _banco.Modelos.Find(Id);
        }

        public async Task<IPagedList<Modelo>> ObterTodosModelos(int? pagina,string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoModelo = _banco.Modelos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoModelo = bancoModelo.Where(a => a.Nome.Contains(pesquisa.Trim()));
            }

            return await bancoModelo.Include(a => a.Marca).ToPagedListAsync<Modelo>(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<Modelo> ObterTodosModelos()
        {
            return _banco.Modelos.ToList();
        }

    }
}
