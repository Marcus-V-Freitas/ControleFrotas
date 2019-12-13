using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index(int? pagina, string pesquisa)
        {
           var clientes = _clienteRepository.ObterTodosClientes(pagina, pesquisa);
            return View(clientes);
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            var cliente = _clienteRepository.ObterCliente(id);
            return View(cliente);
        }

       

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult AtivarDesativar(int id)
        {
            ControleFrotasDLL.BLL.Cliente cliente = _clienteRepository.ObterCliente(id);

            cliente.Situacao = (cliente.Situacao == SituacaoConstant.Ativo) ? cliente.Situacao = SituacaoConstant.Desativado : cliente.Situacao = SituacaoConstant.Ativo;

            _clienteRepository.AtivarDesativar(cliente);

            TempData["MSG_S"] = Mensagem.MSG_S001;
            return RedirectToAction(nameof(Index));

        }
    }
    }