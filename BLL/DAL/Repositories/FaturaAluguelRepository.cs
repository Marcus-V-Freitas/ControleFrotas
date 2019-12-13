using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Constants;
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
    public class FaturaAluguelRepository : IFaturaAluguelRepository
    {
        private ControleFrotasContext _banco;
        private IConfiguration _conf;
        private LoginCliente _login;

        public FaturaAluguelRepository(ControleFrotasContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;
        }

        public async Task Cadastrar(FaturaAluguel faturaAluguel)
        {
            FaturaAluguel items;

            var lista_itens = JsonConvert.DeserializeObject<List<FaturaAluguel>>(faturaAluguel.ListaProdutos);

            items = new FaturaAluguel
            {
                AluguelId= lista_itens[0].AluguelId,
                DataInicio = DateTime.Parse(lista_itens[0].DataInicio).ToString("yyyy/MM/dd"),
                DataRetorno = DateTime.Parse(lista_itens[0].DataRetorno).ToString("yyyy/MM/dd"),
                ValorTotal= faturaAluguel.ValorTotal

            };
            _banco.Add(items);
           Aluguel aluguel= _banco.Alugueis.Include(a=>a.VeiculoEmpresa).FirstOrDefault(a => a.Id.Equals(items.AluguelId));
            aluguel.Status = 1;
            VeiculoEmpresa veiculo = _banco.VeiculosEmpresa.Include(a => a.Veiculo).FirstOrDefault(a => a.VeiculoEmpresaId.Equals(aluguel.AluguelVeiculoId));
            veiculo.Veiculo.Situacao = SituacaoConstant.Ativo;
            await _banco.SaveChangesAsync();

        }

        public async Task<IPagedList<FaturaAluguel>> ObterTodasFaturasAlugueis(int? pagina)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;
            return (_login.Tipo() == null) ? await _banco.FaturaAlugueis.Include(a => a.Aluguel).Include(a => a.Aluguel.ClienteJuridico.Cliente).ToPagedListAsync<FaturaAluguel>(numeroPagina, RegistroPorPagina) :
               await _banco.FaturaAlugueis.Include(a => a.Aluguel).Include(a => a.Aluguel.ClienteJuridico.Cliente).Where(a => _login.Tipo() == a.Aluguel.AluguelClienteId).ToPagedListAsync<FaturaAluguel>(numeroPagina, RegistroPorPagina);
        }

        public IEnumerable<FaturaAluguel> ObterTodasFaturasAlugueis()
        {
            return (_login.Tipo() == null) ? _banco.FaturaAlugueis.ToList() : _banco.FaturaAlugueis.Include(a => a.Aluguel).Where(a => a.Aluguel.AluguelClienteId == _login.Tipo()).ToList();
        }

        public int QuantidadeTotalAlugueis()
        {
            return (_login.Tipo()==null)?_banco.FaturaAlugueis.Count(): _banco.FaturaAlugueis.Include(a=>a.Aluguel).Where(a=>a.Aluguel.AluguelClienteId==_login.Tipo()).Count();
        }


        public decimal ValorTotalAlugueis()
        {
            return (_login.Tipo()==null) ? _banco.FaturaAlugueis.Sum(a => Convert.ToDecimal(a.ValorTotal)) : _banco.FaturaAlugueis.Include(a=>a.Aluguel).Where(a=>a.Aluguel.AluguelClienteId==_login.Tipo()).Sum(a => Convert.ToDecimal(a.ValorTotal));
        }


        public int QuantidadeTotalBoletoBancario()
        {
            return (_login.Tipo()==null)?_banco.Alugueis.Where(a => a.Status.Equals(1) && a.FormaPagamento == MetodoPagamentoConstant.Boleto).Count(): _banco.Alugueis.Where(a => a.Status.Equals(1) && a.FormaPagamento == MetodoPagamentoConstant.Boleto && a.AluguelClienteId==_login.Tipo()).Count();
        }

        public int QuantidadeTotalCartaoCredito()
        {
            return (_login.Tipo() == null) ? _banco.Alugueis.Where(a => a.Status.Equals(1) && a.FormaPagamento == MetodoPagamentoConstant.CartaoCredito).Count() : _banco.Alugueis.Where(a =>a.Status.Equals(1) && a.FormaPagamento == MetodoPagamentoConstant.CartaoCredito && a.AluguelClienteId == _login.Tipo()).Count();
        }
    }
}
