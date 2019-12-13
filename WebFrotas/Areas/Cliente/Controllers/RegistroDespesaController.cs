using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebFrotas.Areas.Cliente.Controllers
{
    [Area(nameof(Cliente))]
    [ClienteAutorizacao]
    public class RegistroDespesaController : Controller
    {

        private IClienteRepository _clienteRepository;
        private IVeiculoClienteRepository _VeiculoClienteRepository;
        private IRegistroRepository _registroRepository;
        private IDespesaRepository _despesaRepository;
        private IMotoristaRepository _motoristaRepository;

        public RegistroDespesaController(IClienteRepository clienteFisicoRepository, IVeiculoClienteRepository veiculoClienteRepository,
             IRegistroRepository registroRepository, IDespesaRepository despesaRepository, IMotoristaRepository motoristaRepository)
        {
            _clienteRepository = clienteFisicoRepository;
            _VeiculoClienteRepository = veiculoClienteRepository;
            _registroRepository = registroRepository;
            _despesaRepository = despesaRepository;
            _motoristaRepository = motoristaRepository;
        }

        public async Task<IActionResult> Index(int? pagina)
        {
            var registros = await _registroRepository.ObterTodosRegistros(pagina);


            return View(registros);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]RegistroDespesa registro)
        {
            if (ModelState.IsValid)
            {

                await _registroRepository.Cadastrar(registro);

                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }

            CarregarDados();
            return View();

        }

        private void CarregarDados()
        {
            ViewBag.Cliente = _clienteRepository.ObterTodosClientes().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Veiculo = _VeiculoClienteRepository.ObterTodosVeiculosCliente().Select(a => new SelectListItem(a.Veiculo.Placa, a.VeiculoClienteId.ToString()));
            ViewBag.ListaItens = _despesaRepository.ObterTodasDespesas();
            ViewBag.Motorista = _motoristaRepository.ObterTodosMotoristas().Select(a => new SelectListItem(a.Cliente.Nome, a.ClienteMotoristaId.ToString()));
        }
    }
}