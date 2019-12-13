using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.Email;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.BLL.Libraries.Login;
using ControleFrotasDLL.BLL.Libraries.ViewModel;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebFrotas.Models;

namespace WebFrotas.Controllers
{
    public class HomeController : Controller
    {
        private IClienteFisicoRepository _clienteFisicoRepository;
        private INewsletterRepository _repositoryNewsletter;
        private LoginCliente _loginCliente;
        private GerenciarEmail _gerenciarEmail;
        private ILogger<HomeController> _logger;
        private IVeiculoEmpresaRepository _veiculoEmpresaRepository;
        private IClienteJuridicoRepository _clienteJuridicoRepository;

        public HomeController(ILogger<HomeController> logger, IClienteFisicoRepository clienteFisicoRepository, INewsletterRepository newsletterRepository, LoginCliente loginCliente,
             IVeiculoEmpresaRepository veiculoEmpresaRepository, GerenciarEmail gerenciarEmail, IClienteJuridicoRepository clienteJuridicoRepository)
        {
            _logger = logger;
            _clienteFisicoRepository = clienteFisicoRepository;
            _repositoryNewsletter = newsletterRepository;
            _loginCliente = loginCliente;
            _gerenciarEmail = gerenciarEmail;
            _veiculoEmpresaRepository = veiculoEmpresaRepository;
            _clienteJuridicoRepository = clienteJuridicoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm] NewsletterEmail newsletterEmail)
        {
            if (ModelState.IsValid)
            {
                _repositoryNewsletter.Cadastrar(newsletterEmail);


                TempData["MSG_S"] = "E-mail cadastro com sucesso! Agora você vai receber promoções especiais no seu E-mail!!! Fique atento as novidades!!!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contato([FromForm]Contato contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _gerenciarEmail.EnviarContatoEmail(contato);

                    ViewData["MSG_S"] = "Mensagem de contato enviada com sucesso!";
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro, tente novamente mais tarde!";

                _logger.LogError(e.Message, "HomeController > Contato > Exception");
            }

            return View("Contato");

        }

        [HttpGet]
        public IActionResult CadastroClienteFisico()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastroClienteFisico([FromForm]ClienteFisico clienteFisico, Cliente cliente)
        {
            if (ModelState.IsValid)
            {
               await _clienteFisicoRepository.Cadastrar(clienteFisico, cliente);

                TempData["MSG_S"] = Mensagem.MSG_S004;

                return RedirectToAction(nameof(CadastroClienteFisico));
            }
            return View();
        }

        [HttpGet]
        public IActionResult CadastroClienteJuridico()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastroClienteJuridico([FromForm]ClienteJuridico clienteJuridico, Cliente cliente)
        {
            if (ModelState.IsValid)
            {
               await _clienteJuridicoRepository.Cadastrar(clienteJuridico, cliente);
                RedirectToAction(nameof(Index));
                TempData["MSG_S"] = Mensagem.MSG_S004;
                return RedirectToAction(nameof(CadastroClienteJuridico));
            }
           
                return View();
            
        }
    }
}
