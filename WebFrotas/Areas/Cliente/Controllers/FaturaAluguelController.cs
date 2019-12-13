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

namespace WebFrotas.Areas.Cliente.Controllers
{
    [Area(nameof(Cliente))]
    [ClienteAutorizacao(ClienteTipoConstant.Juridico)]
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
    }
}