using ControleFrotasDLL.BLL;
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
    public class ClienteFisicoRepository : IClienteFisicoRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

         private LoginCliente _login;

        public ClienteFisicoRepository(ControleFrotasContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;

        }


        public async Task Atualizar(ClienteFisico clienteFisico, Cliente cliente)
        {
            _banco.Update(cliente);
            _banco.Update(clienteFisico);
           await _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(ClienteFisico clienteFisico, Cliente cliente)
        {
            cliente.Situacao = "P";
            _banco.Add(cliente);
            clienteFisico.ClienteFisicoId = cliente.Id;
            _banco.Add(clienteFisico);
          await  _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
            ClienteFisico clienteFisico = ObterClienteFisico(Id);
            _banco.Remove(clienteFisico);
            await _banco.SaveChangesAsync();
        }

        public ClienteFisico ObterClienteFisico(int Id)
        {
            return _banco.ClienteFisicos.Include(a => a.Cliente).FirstOrDefault(x => x.ClienteFisicoId.Equals(Id)); 
        }

        public async Task<IPagedList<ClienteFisico>> ObterTodosClientesFisicos(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoCliente = _banco.ClienteFisicos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoCliente = bancoCliente.Where(a => a.Cliente.Nome.Contains(pesquisa.Trim()));
            }

             return (_login.Tipo() == null) ? await bancoCliente.Include(a => a.Cliente).ToPagedListAsync(numeroPagina, RegistroPorPagina):
               await bancoCliente.Include(a => a.Cliente).Where(a => _login.Tipo().Equals(a.ClienteFisicoId)).ToPagedListAsync(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<ClienteFisico> ObterTodosClientesFisicos()
        {
            return (_login.Tipo() == null) ? _banco.ClienteFisicos.Include(a => a.Cliente).ToList() : _banco.ClienteFisicos.Include(a=>a.Cliente).Where(a => a.ClienteFisicoId == _login.Tipo()).ToList();
        }
    }
}