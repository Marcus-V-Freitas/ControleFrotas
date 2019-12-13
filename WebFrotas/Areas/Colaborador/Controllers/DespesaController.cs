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

namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao]
    public class DespesaController : Controller
    {

        private IClienteRepository _clienteRepository;
        private IDespesaRepository _despesaRepository;
        private IUnidMedRepository _unidMedRepository;


        public DespesaController(IClienteRepository clienteFisicoRepository, IDespesaRepository despesaRepository, IUnidMedRepository unidMedRepository)
        {
            _clienteRepository = clienteFisicoRepository;
            _despesaRepository = despesaRepository;
            _unidMedRepository = unidMedRepository;
        }

        public async Task<IActionResult> Index(int? pagina)
        {
            var despesas = await _despesaRepository.ObterTodasDespesas(pagina);


            return View(despesas);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CarregarDados();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                
              await  _despesaRepository.Cadastrar(despesa);

                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            CarregarDados();
            return View();

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var despesa = _despesaRepository.ObterDespesa(id);


            CarregarDados();
            return View(despesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar([FromForm]Despesa despesa)
        {

            if (ModelState.IsValid)
            {
               await _despesaRepository.Atualizar(despesa);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            CarregarDados();


            return View();         
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(int id)
        {
           await _despesaRepository.Excluir(id);

            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }

        private void CarregarDados()
        {
            ViewBag.Cliente = _clienteRepository.ObterTodosClientes().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.UnidadeMedida = _unidMedRepository.ObterTodasUnidMeds().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
        }
    }
}