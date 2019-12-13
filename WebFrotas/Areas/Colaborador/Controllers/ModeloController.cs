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

namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ModeloController : Controller
    {

        private IModeloRepository _modeloRepository;
        private IMarcaRepository _marcaRepository;

        public ModeloController(IModeloRepository modeloRepository, IMarcaRepository marcaRepository)
        {
            _modeloRepository = modeloRepository;
            _marcaRepository = marcaRepository;
        }

        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            var modelos = await _modeloRepository.ObterTodosModelos(pagina,pesquisa);

            return View(modelos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CarregarDados();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]Modelo modelo)
        {
            if (ModelState.IsValid)
            {
               await _modeloRepository.Cadastrar(modelo);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            CarregarDados();
            return View();

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var modelo =  _modeloRepository.ObterModelo(id);
            CarregarDados();
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromForm]Modelo modelo)
        {
            if (ModelState.IsValid)
            {
               await _modeloRepository.Atualizar(modelo);
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
           await _modeloRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }

        private void CarregarDados()
        {
            ViewBag.Marcas = _marcaRepository.ObterTodasMarcas().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
        }
    }
}