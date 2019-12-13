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
    public class RegistroUsoController : Controller
    {

        private IRegistroUsoRepository _registroUsoRepository;
        private IMotoristaRepository _motoristaRepository;

        public RegistroUsoController(IRegistroUsoRepository registroUsoRepository, IMotoristaRepository motoristaRepository)
        {
            _registroUsoRepository = registroUsoRepository;
            _motoristaRepository = motoristaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? pagina, string pesquisa)
        {
            var registro = await _registroUsoRepository.ObterTodosRegistroUsos(pagina, pesquisa);
            return View(registro);
        }

        [HttpGet]
        public IActionResult Devolver(int id)
        {
            var registro = _registroUsoRepository.ObterRegistroUso(id);

            CarregarDados();
            return View(registro);
        }

        [HttpPost]
        public async Task<IActionResult> Devolver([FromForm]RegistroUso registroUso)
        {

            if (ModelState.IsValid)
            {
                await _registroUsoRepository.Atualizar(registroUso);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            CarregarDados();


            return View();
        }

         private void CarregarDados()
        {
            ViewBag.Motoristas = _motoristaRepository.ObterTodosMotoristas().Select(a => new SelectListItem(a.Cliente.Nome, a.ClienteMotoristaId.ToString()));
        }
    }
}