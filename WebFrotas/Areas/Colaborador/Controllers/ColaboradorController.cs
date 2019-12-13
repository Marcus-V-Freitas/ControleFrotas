using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Email;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.BLL.Libraries.KeyGenerator;
using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ColaboradorController : Controller
    {

        private IColaboradorRepository _colaboradorRepository;
        private GerenciarEmail _gerenciarEmail;

        public ColaboradorController(IColaboradorRepository colaboradorRepository, GerenciarEmail gerenciarEmail)
        {
            _colaboradorRepository = colaboradorRepository;
            _gerenciarEmail = gerenciarEmail;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? pagina)
        {
            var colaboradores = await _colaboradorRepository.ObterTodosColaboradores(pagina);
            return View(colaboradores);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromForm]ControleFrotasDLL.BLL.Colaborador colaborador)
        {
            ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                colaborador.Tipo = ColaboradorTipoConstant.Comum;
                colaborador.Senha = KeyGenerator.GetUniqueKey(8);
               await _colaboradorRepository.Cadastrar(colaborador);

                _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public async Task<IActionResult> GerarSenha(int id)
        {
            ControleFrotasDLL.BLL.Colaborador colaborador = _colaboradorRepository.ObterColaborador(id);
            colaborador.Senha = KeyGenerator.GetUniqueKey(8);
           await _colaboradorRepository.AtualizarSenha(colaborador);

            _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(colaborador);

            TempData["MSG_S"] = Mensagem.MSG_S003;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            ControleFrotasDLL.BLL.Colaborador colaborador = _colaboradorRepository.ObterColaborador(id);
            return View(colaborador);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromForm]ControleFrotasDLL.BLL.Colaborador colaborador, int id)
        {
            ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                colaborador.Tipo = ColaboradorTipoConstant.Comum;
               await _colaboradorRepository.Atualizar(colaborador);
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public async Task<IActionResult> Excluir(int id)
        {
           await _colaboradorRepository.Excluir(id);
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }
    }
    }