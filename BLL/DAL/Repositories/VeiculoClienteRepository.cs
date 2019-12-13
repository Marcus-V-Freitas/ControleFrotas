using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Login;
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
   public class VeiculoClienteRepository:IVeiculoClienteRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        private LoginCliente _login;

        public VeiculoClienteRepository(ControleFrotasContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;
        }


        public async Task Atualizar(VeiculoCliente veiculoCliente, Veiculo veiculo)
        {

            _banco.Update(veiculo);
            _banco.Update(veiculoCliente);
           await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(VeiculoCliente veiculoCliente, Veiculo veiculo)
        {
            veiculo.Situacao = SituacaoConstant.Ativo;
            _banco.Add(veiculo);
            veiculoCliente.VeiculoClienteId = veiculo.Id;
            _banco.Add(veiculoCliente);
           await _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
            VeiculoCliente veiculoCliente = ObterVeiculoCliente(Id);
            _banco.Remove(veiculoCliente);
           await _banco.SaveChangesAsync();
        }

        public VeiculoCliente ObterVeiculoCliente(int Id)
        {
            return _banco.VeiculosCliente.Include(a => a.Veiculo).FirstOrDefault(x => x.VeiculoClienteId.Equals(Id)); 
        }

        public async Task<IPagedList<VeiculoCliente>> ObterTodosVeiculosCliente(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;


            var bancoVeiculo = _banco.VeiculosCliente.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoVeiculo = bancoVeiculo.Where(a => a.Veiculo.CategoriaVeiculo.Nome.Contains(pesquisa.Trim()));
            }

            return (_login.Tipo() == null) ? await bancoVeiculo.Include(a => a.Veiculo.CategoriaVeiculo).Include(a => a.Cliente).Include(a => a.Veiculo).Include(a => a.Veiculo.Modelo).ToPagedListAsync(numeroPagina, RegistroPorPagina) :
             await  bancoVeiculo.Include(a => a.Veiculo.CategoriaVeiculo).Include(a => a.Cliente).Include(a => a.Veiculo).Include(a => a.Veiculo.Modelo).Where(a => _login.Tipo().Equals(a.ClienteId)).ToPagedListAsync(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<VeiculoCliente> ObterTodosVeiculosCliente()
        {
            return (_login.Tipo() == null) ? _banco.VeiculosCliente.Include(a => a.Veiculo.CategoriaVeiculo).Include(a => a.Veiculo).Include(a=>a.Veiculo.Modelo).Include(a => a.Cliente).ToList() : _banco.VeiculosCliente.Include(a => a.Veiculo.CategoriaVeiculo).Include(a => a.Veiculo).Include(a => a.Veiculo.Modelo).Include(a => a.Cliente).Where(a => a.ClienteId == _login.Tipo()).ToList();
        }

        public int QuantidadeTotalVeiculosCliente()
        {
            return (_login.Tipo() == null) ? _banco.VeiculosCliente.Count() : _banco.VeiculosCliente.Where(a=>a.ClienteId==_login.Tipo()).Count();
        }


    }
}
