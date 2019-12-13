using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao]
    public class FaturaAluguelController : Controller
    {

        private IFaturaAluguelRepository _faturaRepository;
        private IAluguelRepository _aluguelRepository;

        public FaturaAluguelController(IAluguelRepository aluguelRepository, IFaturaAluguelRepository faturaAluguelRepository)
        {
            _faturaRepository = faturaAluguelRepository;
            _aluguelRepository = aluguelRepository;
        }

        public async Task<IActionResult> Index(int? pagina)
        {
            var faturas = await _faturaRepository.ObterTodasFaturasAlugueis(pagina);


            return View(faturas);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            CarregarDados();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]FaturaAluguel faturaAluguel)
        {
            if (ModelState.IsValid)
            {

               await _faturaRepository.Cadastrar(faturaAluguel);

                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }

            CarregarDados();
            return View();

        }

        private void CarregarDados()
        {
            ViewBag.Aluguel = _aluguelRepository.ObterTodosAlugueis();
        }
    }
}