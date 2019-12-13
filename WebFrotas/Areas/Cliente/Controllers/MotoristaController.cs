using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebFrotas.Areas.Cliente.Controllers
{
    [Area(nameof(Cliente))]
    [ClienteAutorizacao(ClienteTipoConstant.Juridico)]
    public class MotoristaController : Controller
    {
        private IClienteJuridicoRepository _clienteJuridicoRepository;
        private IMotoristaRepository _motoristaRepository;
        private IClienteRepository _clienteRepository;

        public MotoristaController(IClienteJuridicoRepository clienteJuridicoRepository, IMotoristaRepository motoristaRepository, IClienteRepository clienteRepository)
        {
            _clienteJuridicoRepository = clienteJuridicoRepository;
            _motoristaRepository = motoristaRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            var motoristas = await _motoristaRepository.ObterTodosMotoristas(pagina, pesquisa);


            return View(motoristas);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]Motorista motorista, ControleFrotasDLL.BLL.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
               await _motoristaRepository.Cadastrar(motorista,cliente);

                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            CarregarDados();
            return View();

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var motorista = _motoristaRepository.ObterMotorista(id);
            CarregarDados();

            return View(motorista);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromForm]Motorista motorista, ControleFrotasDLL.BLL.Cliente cliente)
        {

            if (ModelState.IsValid)
            {

              await  _motoristaRepository.Atualizar(motorista,cliente);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }

            CarregarDados();

            return View();          
        }

        [HttpGet]
        [ValidateHttpReferer]
        public async Task<IActionResult> Excluir(int id)
        {

           await _motoristaRepository.Excluir(id);
            //_motoristaRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }

        private void CarregarDados()
        {
            ViewBag.Cliente = _clienteJuridicoRepository.ObterTodosClientesJuridicos().Select(a => new SelectListItem(a.Cliente.Nome, Convert.ToInt32(a.ClienteJuridicoId).ToString()));
        }
    }
}