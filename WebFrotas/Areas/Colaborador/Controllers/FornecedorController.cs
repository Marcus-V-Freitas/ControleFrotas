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

namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class FornecedorController : Controller
    {

        private IFornecedorRepository _fornecedorRepository;
        private ISeguroRepository _seguroRepository;

        public FornecedorController(IFornecedorRepository fornecedorRepository, ISeguroRepository seguroRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _seguroRepository = seguroRepository;
        }


        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            var fornecedores = await _fornecedorRepository.ObterTodosFornecedores(pagina, pesquisa);

            return View(fornecedores);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
               await _fornecedorRepository.Cadastrar(fornecedor);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
           
            return View();

        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var fornecedor =  _fornecedorRepository.ObterFornecedor(id);
            
            return View(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromForm]Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
              await  _fornecedorRepository.Atualizar(fornecedor);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
           

            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public async Task<IActionResult> Excluir(int id)
        {
           await _fornecedorRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ValidateHttpReferer]
        public async Task<IActionResult> Seguro(int? pagina, string rota, string pesquisa)
        {
          
            var seguros = await _seguroRepository.ObterTodosSegurosPorFornecedor(pagina, rota, pesquisa);

            ViewData["Rota"] = rota;
            return View(seguros);

        }
    }
}