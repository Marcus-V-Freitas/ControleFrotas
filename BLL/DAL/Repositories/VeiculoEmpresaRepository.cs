using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Login;
using ControleFrotasDLL.BLL.Libraries.Validacao;
using ControleFrotasDLL.DAL.Database.SQL;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories
{
   public class VeiculoEmpresaRepository:IVeiculoEmpresaRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        public VeiculoEmpresaRepository(ControleFrotasContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _conf = configuration;
        }


        public async Task Atualizar(VeiculoEmpresa veiculoEmpresa, Veiculo veiculo)
        {
            _banco.Update(veiculo);
            _banco.Update(veiculoEmpresa);
           await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(VeiculoEmpresa veiculoEmpresa, Veiculo veiculo)
        {
            veiculo.Situacao = SituacaoConstant.Ativo;
            _banco.Add(veiculo);
            veiculoEmpresa.VeiculoEmpresaId = veiculo.Id;
            _banco.Add(veiculoEmpresa);
           await _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
           VeiculoEmpresa veiculo = ObterVeiculoEmpresa(Id);
            _banco.Remove(veiculo);
           await _banco.SaveChangesAsync();
        }

        public VeiculoEmpresa ObterVeiculoEmpresa(int Id)
        {
            return _banco.VeiculosEmpresa.Include(a => a.Veiculo).FirstOrDefault(x => x.VeiculoEmpresaId.Equals(Id));
        }

        public async Task<IPagedList<VeiculoEmpresa>> ObterTodosVeiculosEmpresa(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoVeiculo = _banco.VeiculosEmpresa.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoVeiculo = bancoVeiculo.Where(a => a.Veiculo.CategoriaVeiculo.Nome.Contains(pesquisa.Trim()));
            }

            return await bancoVeiculo.Include(a => a.Veiculo.CategoriaVeiculo).Include(a => a.Veiculo).Include(a => a.Veiculo.Modelo).ToPagedListAsync(numeroPagina, RegistroPorPagina);
        }

        public async Task<IPagedList<VeiculoEmpresa>> ObterTodosVeiculosEmpresaRodizio(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoVeiculo = _banco.VeiculosEmpresa.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoVeiculo = bancoVeiculo.Where(a => a.Veiculo.CategoriaVeiculo.Nome.Contains(pesquisa.Trim()));
            }

            Rodizio.Dias();

            return await bancoVeiculo.Include(a => a.Veiculo).Include(a=>a.Veiculo.CategoriaVeiculo).Include(a => a.Veiculo.Modelo).Where(x => !x.Veiculo.Placa.Substring(x.Veiculo.Placa.Length - 1, 1).Equals(Rodizio.Digito1) && !x.Veiculo.Placa.Substring(x.Veiculo.Placa.Length - 1, 1).Equals(Rodizio.Digito2)).ToPagedListAsync(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<VeiculoEmpresa> ObterTodosVeiculosEmpresaRodizio()
        {
            Rodizio.Dias();
            return _banco.VeiculosEmpresa.Include(a => a.Veiculo).Include(a => a.Veiculo.CategoriaVeiculo).Where(a => a.Veiculo.Situacao == "A").Where(x => !x.Veiculo.Placa.Substring(x.Veiculo.Placa.Length - 1, 1).Equals(Rodizio.Digito1) && !x.Veiculo.Placa.Substring(x.Veiculo.Placa.Length - 1, 1).Equals(Rodizio.Digito2)).ToList();
        }

        public IEnumerable<VeiculoEmpresa> ObterTodosVeiculosEmpresa()
        {
            return _banco.VeiculosEmpresa.Include(a => a.Veiculo.CategoriaVeiculo).Include(a => a.Veiculo).Where(a => a.Veiculo.Situacao=="A").ToList();
        }

        public void BaixaNoVeiculo(int Id)
        {
            var veiculo = ObterVeiculoEmpresa(Id);
            veiculo.Veiculo.Situacao = SituacaoConstant.Pendente;
            _banco.SaveChanges();
        }

        public int QuantidadeTotalVeiculosEmpresa()
        {
            return _banco.VeiculosEmpresa.Count();
        }
    }
}
