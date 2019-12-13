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
    public class VeiculoEmpresaController : Controller
    {
        private ICategoriaVeiculoRepository _categoriaVeiculoRepository;
        private IVeiculoEmpresaRepository _VeiculoEmpresaRepository;
        private IModeloRepository _modeloRepository;
        

        public VeiculoEmpresaController(IVeiculoEmpresaRepository veiculoEmpresaRepository, ICategoriaVeiculoRepository categoriaVeiculoRepository,
            IModeloRepository modeloRepository)
        {
            _categoriaVeiculoRepository = categoriaVeiculoRepository;
            _VeiculoEmpresaRepository = veiculoEmpresaRepository;
            _modeloRepository = modeloRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {

            var veiculosEmpresa = await _VeiculoEmpresaRepository.ObterTodosVeiculosEmpresa(pagina, pesquisa);


            return View(veiculosEmpresa);
        }

        [HttpGet]
        public async Task<IActionResult> IndexRodizio(int? pagina, string pesquisa)
        {
            var veiculosEmpresa = await _VeiculoEmpresaRepository.ObterTodosVeiculosEmpresaRodizio(pagina, pesquisa);
            return View(veiculosEmpresa);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]Veiculo veiculo, VeiculoEmpresa veiculoEmpresa)
        {
            if (ModelState.IsValid)
            {
              await  _VeiculoEmpresaRepository.Cadastrar(veiculoEmpresa, veiculo);

                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            CarregarDados();
            return View();

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var veiculoEmpresa = _VeiculoEmpresaRepository.ObterVeiculoEmpresa(id);


            CarregarDados();

            return View(veiculoEmpresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar([FromForm]Veiculo veiculo, VeiculoEmpresa veiculoEmpresa)
        {

            if (ModelState.IsValid)
            {

               await _VeiculoEmpresaRepository.Atualizar(veiculoEmpresa, veiculo);
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

           await _VeiculoEmpresaRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }

        private void CarregarDados()
        {
            ViewBag.Modelos = _modeloRepository.ObterTodosModelos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Categorias = _categoriaVeiculoRepository.ObterTodasCategoriasVeiculo().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
        }
    }
}