using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.IntermediarioJS;
using ControleFrotasDLL.BLL.Libraries.Login;
using ControleFrotasDLL.DAL.Database.SQL;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using X.PagedList;

namespace ControleFrotasDLL.DAL.Repositories
{
    public class RegistroRepository : IRegistroRepository
    {

        private ControleFrotasContext _banco;
        private IConfiguration _conf;
        private LoginCliente _login;

        public RegistroRepository(ControleFrotasContext banco, IConfiguration configuration, LoginCliente loginCliente)
        {
            _banco = banco;
            _conf = configuration;
            _login = loginCliente;
        }

        public async Task Cadastrar(RegistroDespesa registro)
        {
            _banco.Add(registro);
           await _banco.SaveChangesAsync();
            ItemsRegistro items;

            var lista_itens = JsonConvert.DeserializeObject<List<ItemsRegistroJS>>(registro.ListaProdutos);
            for (int i = 0; i < lista_itens.Count; i++)
            {
                items = new ItemsRegistro
                {
                    RegistroId = registro.Id,
                    DespesaId = int.Parse(lista_itens[i].CodigoItem.ToString()),
                    PrecoUnitario = double.Parse(lista_itens[i].PrecoUnitario.ToString().Replace(",", ".")),
                    QuantidadeItem = double.Parse(lista_itens[i].QuantidadeItem.ToString()),

                };
                _banco.Add(items);
              await  _banco.SaveChangesAsync();
            }
        }

        public async Task<IPagedList<RegistroDespesa>> ObterTodosRegistros(int? pagina)
        {
            int RegistroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int numeroPagina = pagina ?? 1;
            return (_login.Tipo() == null) ? await _banco.RegistrosDespesas.Include(a=>a.Motorista.Cliente).Include(a => a.VeiculoCliente).Include(a=>a.VeiculoCliente.Veiculo).ToPagedListAsync<RegistroDespesa>(numeroPagina, RegistroPorPagina) :
              await  _banco.RegistrosDespesas.Include(a=>a.Motorista.Cliente).Include(a => a.VeiculoCliente).Include(a => a.VeiculoCliente.Veiculo).Where(a=>_login.Tipo()==a.VeiculoCliente.ClienteId).ToPagedListAsync<RegistroDespesa>(numeroPagina, RegistroPorPagina);
        }

        public List<RegistroDespesa> ObterTodosRegistros()
        {
            return _banco.RegistrosDespesas.ToList();
        }

        public decimal ObterTotalPeriodo(DateTime? inicio, DateTime? fim)
        {
            if (inicio == null || fim == null)
            {
                return _banco.RegistrosDespesas.Include(a=>a.VeiculoCliente.Cliente).Where(a=>a.VeiculoCliente.ClienteId==_login.Tipo()).Sum(a => Convert.ToDecimal(a.Total));
            }
            else
            {
                IEnumerable<RegistroDespesa> registro = _banco.RegistrosDespesas.Include(a=>a.VeiculoCliente.Cliente).Where(a =>a.VeiculoCliente.ClienteId==_login.Tipo() &&  a.Data >= inicio && a.Data <= fim);
                return registro.Sum(a => Convert.ToDecimal(a.Total));
            }
        }

    }
}
