using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Login;
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
    public class RegistroUsoRepository:IRegistroUsoRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        private IVeiculoClienteRepository _veiculoClienteRepository;

        private LoginCliente _login;

        public RegistroUsoRepository(ControleFrotasContext banco, IConfiguration configuration, IVeiculoClienteRepository veiculoClienteRepository, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;
            _veiculoClienteRepository = veiculoClienteRepository;
        }

        public async Task Atualizar(RegistroUso registroUso)
        {
            _banco.Update(registroUso);
            var veiculo = _veiculoClienteRepository.ObterVeiculoCliente(Convert.ToInt32(registroUso.RegistroVeiculoClienteId));
            veiculo.Veiculo.Situacao = SituacaoConstant.Ativo;
            await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(RegistroUso registroUso)
        {
            registroUso.Retorno = null;
            _banco.Add(registroUso);
           var veiculo= _veiculoClienteRepository.ObterVeiculoCliente(Convert.ToInt32(registroUso.RegistroVeiculoClienteId));
            veiculo.Veiculo.Situacao = SituacaoConstant.Pendente;
            await _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
            RegistroUso registroUso = ObterRegistroUso(Id);
            _banco.Remove(registroUso);
            await _banco.SaveChangesAsync();
        }

        public RegistroUso ObterRegistroUso(int Id)
        {
            return _banco.RegistrosUsos.Find(Id);
        }

        public async Task<IPagedList<RegistroUso>> ObterTodosRegistroUsos(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoRegistro = _banco.RegistrosUsos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoRegistro = bancoRegistro.Where(a => a.Motorista.Cliente.Nome.Contains(pesquisa.Trim()));
            }

            return (_login.Tipo() == null) ? await bancoRegistro.Include(a => a.VeiculoCliente).Include(a => a.VeiculoCliente.Veiculo).Include(a => a.Motorista).Include(a => a.Motorista.Cliente).ToPagedListAsync<RegistroUso>(numeroPagina, RegistroPorPagina) :
                await bancoRegistro.Include(a => a.VeiculoCliente).Include(a => a.VeiculoCliente.Veiculo).Include(a => a.Motorista).Include(a => a.Motorista.Cliente).Where(a => a.VeiculoCliente.ClienteId == _login.Tipo()).ToPagedListAsync<RegistroUso>(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<RegistroUso> ObterTodosRegistroUsos()
        {
            return _banco.RegistrosUsos.Include(a => a.VeiculoCliente).Include(a => a.VeiculoCliente.Veiculo).Include(a => a.Motorista).Include(a => a.Motorista.Cliente).ToList();
        }
    }
}
