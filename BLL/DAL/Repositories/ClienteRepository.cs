using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Login;
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
    public class ClienteRepository : IClienteRepository
    {
        private IConfiguration _conf;
        private ControleFrotasContext _banco;
        private LoginCliente _login;


        public ClienteRepository(ControleFrotasContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;
        }

        public void AtualizarSenha(Cliente cliente)
        {
            _banco.Update(cliente);
            _banco.Entry(cliente).Property(a => a.Nome).IsModified = false;
            _banco.Entry(cliente).Property(a => a.Email).IsModified = false;
            _banco.Entry(cliente).Property(a => a.CPFCNPJ).IsModified = false;
            _banco.Entry(cliente).Property(a => a.Nascimento).IsModified = false;
            _banco.Entry(cliente).Property(a => a.Telefone).IsModified = false;
            _banco.Entry(cliente).Property(a => a.Tipo).IsModified = false;
            _banco.SaveChanges();
        }

        public void AtivarDesativar(Cliente cliente)
        {
            _banco.Update(cliente);
            _banco.Entry(cliente).Property(a => a.Senha).IsModified = false;
            _banco.SaveChanges();
        }

        public List<Cliente> ObterClientePorEmail(string email)
        {
            return _banco.Clientes.Where(a => a.Email == email).ToList();
        }

        public Cliente Login(string email, string senha)
        {
            return _banco.Clientes.Where(m => m.Email == email && m.Senha == senha && m.Situacao != SituacaoConstant.Desativado && m.Tipo!="M").FirstOrDefault();
        }

        public Cliente ObterCliente(int Id)
        {
            return _banco.Clientes.Find(Id);
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            return (_login.Tipo() == null) ? _banco.Clientes.Where(a=>a.Tipo!="M").ToList() : _banco.Clientes.Where(a => a.Id == _login.Tipo() && a.Tipo!="M").ToList();
        }

    

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoCliente = _banco.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoCliente = bancoCliente.Where(a => a.Nome.Contains(pesquisa.Trim()) || a.Email.Contains(pesquisa.Trim()));
            }

            return bancoCliente.Where(a=>a.Tipo!="M").ToPagedList<Cliente>(numeroPagina, RegistroPorPagina);
        }

        public int QuantidadeTotalClientes()
        {
            return _banco.Clientes.Where(a=>a.Tipo!="M" && a.Situacao=="A").Count();
        }

    }
}
