using ControleFrotasDLL.BLL;
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
    public class MotoristaRepository:IMotoristaRepository
    {
        private IConfiguration _conf;

        private ControleFrotasContext _banco;

        private LoginCliente _login;

        public MotoristaRepository(ControleFrotasContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;

        }

        public async Task Atualizar(Motorista motorista, Cliente cliente)
        {

            _banco.Update(cliente);
            _banco.Update(motorista);
          await  _banco.SaveChangesAsync();
        }

        public async Task Cadastrar(Motorista motorista, Cliente cliente)
        {
            cliente.Situacao = "P";
            _banco.Add(cliente);
            motorista.ClienteMotoristaId = cliente.Id;
            _banco.Add(motorista);
         await   _banco.SaveChangesAsync();
        }

        public async Task Excluir(int Id)
        {
            Motorista motorista = ObterMotorista(Id);
            _banco.Remove(motorista);
          await  _banco.SaveChangesAsync();
        }

        public Motorista ObterMotorista(int Id)
        {
            return  _banco.Motoristas.Include(a => a.Cliente).FirstOrDefault(x => x.ClienteMotoristaId.Equals(Id)); 
        }

        public async Task<IPagedList<Motorista>> ObterTodosMotoristas(int? pagina, string pesquisa)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            var bancoMotoristas = _banco.Motoristas.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                //Não Vazio
                bancoMotoristas = bancoMotoristas.Where(a => a.Cliente.Nome.Contains(pesquisa.Trim()));
            }

            return (_login.Tipo() == null) ? await bancoMotoristas.Include(a=>a.Cliente).Include(a => a.clienteJuridico.Cliente).Include(a=>a.clienteJuridico).ToPagedListAsync(numeroPagina, RegistroPorPagina) :
             await bancoMotoristas.Include(a => a.Cliente).Include(a => a.clienteJuridico.Cliente).Include(a=>a.clienteJuridico).Where(a => _login.Tipo().Equals(a.EmpresaId)).ToPagedListAsync(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<Motorista> ObterTodosMotoristas()
        {
            return (_login.Tipo() == null) ? _banco.Motoristas.Include(a => a.Cliente).Include(a=>a.clienteJuridico).ToList() : _banco.Motoristas.Include(a => a.Cliente).Include(a => a.clienteJuridico).Where(a => a.EmpresaId == _login.Tipo()).ToList();
        }

       public int QuantidadeTotalMotoristas()
        {
            return (_login.Tipo() == null) ? _banco.Motoristas.Count() : _banco.Motoristas.Where(a => a.EmpresaId == _login.Tipo()).Count();
        }
    }
}
