using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.IntermediarioJS;
using ControleFrotasDLL.BLL.Libraries.Login;
using ControleFrotasDLL.DAL.Database.SQL;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        private ControleFrotasContext _banco;
        private IConfiguration _conf;
        private LoginCliente _login;

        public AluguelRepository(ControleFrotasContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;
        }

        public async Task<Aluguel> Cadastrar(Aluguel aluguel)
        {
            Aluguel items;

            var lista_itens = JsonConvert.DeserializeObject<List<Aluguel>>(aluguel.ListaProdutos);

            items = new Aluguel
            {
                DataInicio = aluguel.DataInicio,
                DataPrevista = aluguel.DataPrevista,
                ValorPrevisto = aluguel.ValorPrevisto,
                AluguelSeguroId = lista_itens[0].AluguelSeguroId,
                AluguelVeiculoId = lista_itens[0].AluguelVeiculoId,
                AluguelClienteId = aluguel.AluguelClienteId,
                AluguelMotoristaId=aluguel.AluguelMotoristaId,
                Status = 0,

            };
            _banco.Add(items);
           
           await _banco.SaveChangesAsync();
            return items;

        }

        public async Task<IPagedList<Aluguel>> ObterTodosAlugueis(int? pagina)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;

            AlugueisNaoRealizados();

            return (_login.Tipo() == null) ? await _banco.Alugueis.Include(a => a.ClienteJuridico.Cliente).Include(a => a.ClienteJuridico).Include(a => a.Seguro).Include(a => a.VeiculoEmpresa).Include(a => a.VeiculoEmpresa.Veiculo).Where(a => a.Status == 0).ToPagedListAsync<Aluguel>(numeroPagina, RegistroPorPagina) :
               await _banco.Alugueis.Include(a => a.ClienteJuridico.Cliente).Include(a => a.ClienteJuridico).Include(a => a.Seguro).Include(a => a.VeiculoEmpresa).Include(a => a.VeiculoEmpresa.Veiculo).Where(a => _login.Tipo() == a.AluguelClienteId && a.Status == 0).ToPagedListAsync<Aluguel>(numeroPagina, RegistroPorPagina);
        }

        private void AlugueisNaoRealizados()
        {
            List<Aluguel> alugueis = _banco.Alugueis.Where(a => a.FormaPagamento == null).ToList();
            _banco.RemoveRange(alugueis);
            _banco.SaveChanges();
        }

        public IEnumerable<Aluguel> ObterTodosAlugueis()
        {
            AlugueisNaoRealizados();
            return _banco.Alugueis.Include(a => a.ClienteJuridico.Cliente).Include(a => a.ClienteJuridico).Include(a => a.VeiculoEmpresa).Include(a=>a.Seguro).Include(a => a.VeiculoEmpresa.Veiculo).Where(a => a.Status.Equals(0)).ToList();
        }

        public Aluguel ObterAluguel(int Id)
        {
            return  _banco.Alugueis.Include(a => a.VeiculoEmpresa.Veiculo).Include(a => a.Seguro).FirstOrDefault(a => a.Id == Id);
        }

        public void Transacao(Aluguel aluguel, AluguelPagarMe aluguelPagarMe)
        {
            aluguel.FormaPagamento = aluguelPagarMe.FormaPagamento;
            aluguel.TransactionId = aluguelPagarMe.TransactionId;
            aluguel.DadosTransaction = aluguelPagarMe.DadosTransaction;
            _banco.Update(aluguel);
            _banco.SaveChanges();
        }

       

    }
}
