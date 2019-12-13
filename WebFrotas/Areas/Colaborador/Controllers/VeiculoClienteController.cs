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
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao]
    public class VeiculoClienteController : Controller
    {
        private ICategoriaVeiculoRepository _categoriaVeiculoRepository;
        private IClienteRepository _clienteRepository;
        private IVeiculoClienteRepository _VeiculoClienteRepository;
        private IModeloRepository _modeloRepository;
        private IMotoristaRepository _motoristaRepository;
        private IRegistroUsoRepository _registroUsoRepository;

        public VeiculoClienteController(IClienteRepository clienteFisicoRepository, IVeiculoClienteRepository veiculoClienteRepository, 
            ICategoriaVeiculoRepository categoriaVeiculoRepository, IModeloRepository modeloRepository, IMotoristaRepository motoristaRepository,
            IRegistroUsoRepository registroUsoRepository)
        {
            _categoriaVeiculoRepository = categoriaVeiculoRepository;
            _clienteRepository = clienteFisicoRepository;
            _VeiculoClienteRepository = veiculoClienteRepository;
            _modeloRepository = modeloRepository;
            _motoristaRepository = motoristaRepository;
            _registroUsoRepository = registroUsoRepository;
        }

        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            var veiculosCliente = await _VeiculoClienteRepository.ObterTodosVeiculosCliente(pagina, pesquisa);


            return View(veiculosCliente);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]Veiculo veiculo, VeiculoCliente veiculoCliente)
        {
            if (ModelState.IsValid)
            {
              await  _VeiculoClienteRepository.Cadastrar(veiculoCliente, veiculo);

                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            CarregarDados();
            return View();

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var veiculoCliente =  _VeiculoClienteRepository.ObterVeiculoCliente(id);


            CarregarDados();

            return View(veiculoCliente);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromForm]Veiculo veiculo, VeiculoCliente veiculoCliente)
        {

            if (ModelState.IsValid)
            {
               await _VeiculoClienteRepository.Atualizar(veiculoCliente, veiculo);
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

           await _VeiculoClienteRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Requisitar(int id)
        {
            ViewBag.Veiculo = _VeiculoClienteRepository.ObterVeiculoCliente(id);
            CarregarDados();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Requisitar([FromForm]RegistroUso registroUso)
        {

            if (ModelState.IsValid)
            {
                await _registroUsoRepository.Cadastrar(registroUso);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            CarregarDados();
            ViewBag.Veiculo = _VeiculoClienteRepository.ObterVeiculoCliente(Convert.ToInt32(registroUso.RegistroVeiculoClienteId));

            return View();
        }

        private void CarregarDados()
        {
            ViewBag.Cliente = _clienteRepository.ObterTodosClientes().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Modelos = _modeloRepository.ObterTodosModelos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Categorias = _categoriaVeiculoRepository.ObterTodasCategoriasVeiculo().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Motoristas = _motoristaRepository.ObterTodosMotoristas().Select(a => new SelectListItem(a.Cliente.Nome, a.ClienteMotoristaId.ToString()));
            
            
        }
    }
}