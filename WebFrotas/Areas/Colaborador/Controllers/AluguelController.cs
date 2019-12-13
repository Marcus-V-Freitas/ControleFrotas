using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.BLL.Libraries.IntermediarioJS;
using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.BLL.Libraries.Pagamento;
using ControleFrotasDLL.BLL.Libraries.Texto;
using ControleFrotasDLL.BLL.Libraries.ViewModel;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PagarMe;

namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao]
    public class AluguelController : Controller
    {
        private IClienteJuridicoRepository _clienteJuridicoRepository;
        private IVeiculoEmpresaRepository _veiculoEmpresaRepository;
        private IAluguelRepository _aluguelRepository;
        private ILogger<AluguelController> _logger;
        private GerenciarPagarMe _pagar;
        private IMapper _mapper;
        private IMotoristaRepository _motoristaRepository;
        private ISeguroRepository _seguroRepository;

        public AluguelController(IClienteJuridicoRepository clienteJuridicoRepository,
           IAluguelRepository aluguelRepository, IVeiculoEmpresaRepository veiculoEmpresa,
            ILogger<AluguelController> logger, GerenciarPagarMe pagar, IMapper mapper, IMotoristaRepository motoristaRepository, ISeguroRepository seguroRepository)
        {
            _clienteJuridicoRepository = clienteJuridicoRepository;
            _veiculoEmpresaRepository = veiculoEmpresa;
            _aluguelRepository = aluguelRepository;
            _logger = logger;
            _pagar = pagar;
            _mapper = mapper;
            _motoristaRepository = motoristaRepository;
            _seguroRepository = seguroRepository;

        }

        public async Task<IActionResult> Index(int? pagina)
        {
            var aluguel = await _aluguelRepository.ObterTodosAlugueis(pagina);
            

            return View(aluguel);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]Aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
             var aluguelCadastro= await _aluguelRepository.Cadastrar(aluguel);
                return new RedirectToActionResult("Pagamento", "Aluguel", new { id = aluguelCadastro.Id });

            }

            CarregarDados();
            return RedirectToAction(nameof(Index));

        }


        private List<SelectListItem> CalcularParcelamento(Aluguel aluguel)
        {
            var parcelamento = _pagar.CalcularPagamentoParcelado(Convert.ToDecimal(aluguel.ValorPrevisto));


            return parcelamento.Select(a => new SelectListItem(
                String.Format(
                    "{0}x {1} {2} - TOTAL: {3}",
                    a.Numero, a.ValorPorParcela.ToString("C"), (a.Juros) ? "c/ juros" : "s/ juros", a.Valor.ToString("C")
                ),
                a.Numero.ToString()
            )).ToList();
        }

        public IActionResult Pagamento(int id)
        {
            var pagamento = new PagamentoViewModel { Aluguel =  _aluguelRepository.ObterAluguel(id) };
            DateTime data = DateTime.ParseExact(pagamento.Aluguel.DataPrevista, "yyyy/MM/dd", CultureInfo.InvariantCulture);
           pagamento.Aluguel.DataPrevista= data.ToString("dd/MM/yyyy");
            ViewBag.Parcelamentos = CalcularParcelamento(pagamento.Aluguel);
            return View(pagamento);
        }

        [HttpPost]
        public IActionResult Pagamento([FromForm]PagamentoViewModel pagamento)
        {
         
            if (ModelState.IsValid)
            {
                Parcelamento parcela = BuscarParcelamento(pagamento.Aluguel, pagamento.Parcelamento.Numero);
                try
                {
                    Transaction transacao = _pagar.GerarPagCartaoCredito(pagamento.CartaoCredito, pagamento.Aluguel, parcela);
                    ProcessarPedido(transacao, pagamento.Aluguel);
                    TempData["MSG_S"] = Mensagem.MSG_S001;
                    return RedirectToAction(nameof(Index));
                }
                catch (PagarMeException e)
                {
                    _logger.LogError(e, "PagamentoController > Index");
                    TempData["MSG_S"] = MontarMensagensDeErro(e);

                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return View();
            }
        }

        private Parcelamento BuscarParcelamento(Aluguel aluguel, int numero)
        {
         
            return _pagar.CalcularPagamentoParcelado(Convert.ToDecimal(aluguel.ValorPrevisto)).Where(a => a.Numero == numero).First();
        }


        private string MontarMensagensDeErro(PagarMeException e)
        {
            StringBuilder sb = new StringBuilder();

            if (e.Error.Errors.Count() > 0)
            {
                sb.Append("Erro no pagamento: ");
                foreach (var erro in e.Error.Errors)
                {
                    sb.Append("- " + erro.Message + "<br />");
                }
            }
            return sb.ToString();
        }

        private void ProcessarPedido(Transaction transaction, Aluguel aluguel)
        {
            TransacaoPagarMe transacaoPagarMe;
            AluguelPagarMe aluguelPagarMe;

            SalvarPedido(transaction, out transacaoPagarMe, out aluguelPagarMe, aluguel);

            _veiculoEmpresaRepository.BaixaNoVeiculo(Convert.ToInt32(aluguel.AluguelVeiculoId));
        }

        private void SalvarPedido(Transaction transaction, out TransacaoPagarMe transacaoPagarMe,
            out AluguelPagarMe aluguelPagarMe, Aluguel aluguel)
        {
            transacaoPagarMe = _mapper.Map<TransacaoPagarMe>(transaction);
            aluguelPagarMe = _mapper.Map<AluguelPagarMe>(transacaoPagarMe);
            _aluguelRepository.Transacao(aluguel, aluguelPagarMe);

        }

        private void CarregarDados()
        {
            ViewBag.Cliente = _clienteJuridicoRepository.ObterTodosClientesJuridicos();
            ViewBag.Veiculo = _veiculoEmpresaRepository.ObterTodosVeiculosEmpresaRodizio();
            ViewBag.Seguro = _seguroRepository.ObterTodosSeguros();
            ViewBag.Motorista = _motoristaRepository.ObterTodosMotoristas();
        }
    }
}