using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Login;
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
    public class ClienteJuridicoRepository:IClienteJuridicoRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        private LoginCliente _login;

        public ClienteJuridicoRepository(ControleFrotasContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;

        }

        public async Task Atualizar(ClienteJuridico clienteJuridico, Cliente cliente)
        {

            _banco.Update(cliente);
            _banco.Update(clienteJuridico);
          await  _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(ClienteJuridico clienteJuridico, Cliente cliente)
        {
            cliente.Situacao = SituacaoConstant.Pendente;
            _banco.Add(cliente);
            clienteJuridico.ClienteJuridicoId = cliente.Id;
            _banco.Add(clienteJuridico);
           await _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
            ClienteJuridico clienteJuridico = ObterClienteJuridico(Id);
            _banco.Remove(clienteJuridico);
           await _banco.SaveChangesAsync();
        }

        public ClienteJuridico ObterClienteJuridico(int Id)
        {
            return _banco.ClienteJuridicos.Include(a => a.Cliente).FirstOrDefault(x => x.ClienteJuridicoId.Equals(Id)); //Include(a => a.Cliente).Include(a => a.Veiculo).Include(a => a.Veiculo.Modelo).FirstOrDefault(x => x.VeiculoClienteId.Equals(Id));
        }

        public async Task<IPagedList<ClienteJuridico>> ObterTodosClientesJuridicos(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoCliente = _banco.ClienteJuridicos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoCliente = bancoCliente.Where(a => a.Cliente.Nome.Contains(pesquisa.Trim()));
            }

            return (_login.Tipo() == null) ? await bancoCliente.Include(a => a.Cliente).ToPagedListAsync(numeroPagina, RegistroPorPagina) :
              await bancoCliente.Include(a => a.Cliente).Where(a => _login.Tipo().Equals(a.ClienteJuridicoId)).ToPagedListAsync(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<ClienteJuridico> ObterTodosClientesJuridicos()
        {
            return (_login.Tipo() == null) ? _banco.ClienteJuridicos.Include(a => a.Cliente).ToList() : _banco.ClienteJuridicos.Include(a => a.Cliente).Where(a => a.ClienteJuridicoId == _login.Tipo()).ToList();
        }
    }
}