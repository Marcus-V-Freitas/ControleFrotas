using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.BLL.Libraries.Constants;

namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class MarcaController : Controller
    {

            private IMarcaRepository _marcaRepository;

            public MarcaController(IMarcaRepository marcaRepository)
            {
                _marcaRepository = marcaRepository;
            }


            public async Task<IActionResult> Index(int? pagina, string pesquisa)
            {
                var marcas = await _marcaRepository.ObterTodasMarcas(pagina,pesquisa);

                return View(marcas);
            }

            [HttpGet]
            public IActionResult Cadastrar()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Cadastrar([FromForm]Marca marca)
            {
                if (ModelState.IsValid)
                {
                   await _marcaRepository.Cadastrar(marca);
                    TempData["MSG_S"] = Mensagem.MSG_S001;
                    return RedirectToAction(nameof(Index));
                }
             
                return View();

            }

            [HttpGet]
            public IActionResult Atualizar(int id)
            {
                var marca = _marcaRepository.ObterMarca(id);
               
                return View(marca);
            }

            [HttpPost]
            public async Task<IActionResult> Atualizar([FromForm]Marca marca)
            {
                if (ModelState.IsValid)
                {
                   await _marcaRepository.Atualizar(marca);
                    TempData["MSG_S"] = Mensagem.MSG_S001;
                    return RedirectToAction(nameof(Index));
                }
               
                return View();
            }

        [HttpGet]
        [ValidateHttpReferer]
        public async Task<IActionResult> Excluir(int id)
            {
              await  _marcaRepository.Excluir(id);
                TempData["MSG_S"] = Mensagem.MSG_S002;
                return RedirectToAction(nameof(Index));
            }
        }
}