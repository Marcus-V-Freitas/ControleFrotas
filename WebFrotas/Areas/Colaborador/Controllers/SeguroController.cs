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
    public class SeguroController : Controller
    {
        private ISeguroRepository _seguroRepository;
        private IFornecedorRepository _fornecedorRepository;

        public SeguroController(ISeguroRepository seguroRepository, IFornecedorRepository fornecedorRepository)
        {
            _seguroRepository = seguroRepository;
            _fornecedorRepository = fornecedorRepository;
        }


        [HttpGet]
        public IActionResult Cadastrar()
        {
            CarregarDados();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]Seguro seguro)
        {
            if (ModelState.IsValid)
            {
               await _seguroRepository.Cadastrar(seguro);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction("Index","Fornecedor");
            }
            CarregarDados();
            return View();

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var seguro = _seguroRepository.ObterSeguro(id);
            CarregarDados();
            return View(seguro);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromForm]Seguro seguro)
        {
            if (ModelState.IsValid)
            {
               await _seguroRepository.Atualizar(seguro);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction("Index", "Fornecedor");
            }
            CarregarDados();

            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public async Task<IActionResult> Excluir(int id)
        {
           await _seguroRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction("Index", "Fornecedor");
        }

        private void CarregarDados()
        {
            ViewBag.Fornecedor = _fornecedorRepository.ObterTodosFornecedores().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
        }
}
}