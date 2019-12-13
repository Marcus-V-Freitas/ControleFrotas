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
    public class UnidMedRepository : IUnidMedRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        public UnidMedRepository(ControleFrotasContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _conf = configuration;
        }


        public async Task Atualizar(UnidadeMedida medida)
        {
            _banco.Update(medida);
           await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(UnidadeMedida medida)
        {
            _banco.Add(medida);
          await  _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
        UnidadeMedida medida = ObterUnidMed(Id);
        _banco.Remove(medida);
         await _banco.SaveChangesAsync();
           }

    public UnidadeMedida ObterUnidMed(int Id)
    {
        return _banco.UnidadeMedidas.Find(Id);
    }

    public async Task<IPagedList<UnidadeMedida>> ObterTodasUnidMeds(int? pagina, string pesquisa)
    {
        int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
        int numeroPagina = pagina ?? 1;

        var bancoMedida = _banco.UnidadeMedidas.AsQueryable();

        if (!string.IsNullOrEmpty(pesquisa))
        {
            //Não Vazio
            bancoMedida = bancoMedida.Where(a => a.Nome.Contains(pesquisa.Trim()));
        }

        return await bancoMedida.ToPagedListAsync<UnidadeMedida>(numeroPagina, RegistroPorPagina);
    }

    public IEnumerable<UnidadeMedida> ObterTodasUnidMeds()
    {
            return _banco.UnidadeMedidas.ToList();
    }
}
        }
