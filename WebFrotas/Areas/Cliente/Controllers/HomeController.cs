using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL.Libraries.Email;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.BLL.Libraries.Login;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using ControleFrotasDLL.BLL;
using ControleFrotasDLL.BLL.Libraries.KeyGenerator;
using ControleFrotasDLL.BLL.Libraries.Lang;

namespace WebFrotas.Areas.Cliente.Controllers
{
    [Area(nameof(Cliente))]
    public class HomeController : Controller
    {
        private IClienteRepository _repositoryCliente;
        private INewsletterRepository _repositoryNewsletter;
        private LoginCliente _loginCliente;
        private GerenciarEmail _gerenciarEmail;
        private IFaturaAluguelRepository _faturaAluguelRepository;
        private IVeiculoClienteRepository _veiculoClienteRepository;
        private IMotoristaRepository _motoristaRepository;
        private IRegistroRepository _registroRepository;

        public HomeController(IClienteRepository repositoryCliente, INewsletterRepository newsletterRepository,
            LoginCliente loginCliente, GerenciarEmail gerenciarEmail, IFaturaAluguelRepository faturaAluguelRepository, 
            IVeiculoClienteRepository veiculoClienteRepository, IMotoristaRepository motoristaRepository, IRegistroRepository registroRepository)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsletter = newsletterRepository;
            _loginCliente = loginCliente;
            _gerenciarEmail = gerenciarEmail;
            _faturaAluguelRepository = faturaAluguelRepository;
            _veiculoClienteRepository = veiculoClienteRepository;
            _motoristaRepository = motoristaRepository;
            _registroRepository = registroRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]ControleFrotasDLL.BLL.Cliente cliente)
        {
            ControleFrotasDLL.BLL.Cliente clienteDB = _repositoryCliente.Login(cliente.Email, cliente.Senha);


            if (clienteDB != null)
            {

                _loginCliente.Login(clienteDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado!";

                return View();
            }
        }

        [HttpGet]
        [ClienteAutorizacao]
        public IActionResult Painel(DateTime? inicio, DateTime? fim)
        {
            ViewBag.Motoristas = _motoristaRepository.QuantidadeTotalMotoristas();
            ViewBag.NumeroAlugueis = _faturaAluguelRepository.QuantidadeTotalAlugueis();
            ViewBag.ValorTotalAlugueis = _faturaAluguelRepository.ValorTotalAlugueis();
            ViewBag.VeiculosCliente = _veiculoClienteRepository.QuantidadeTotalVeiculosCliente();
            ViewBag.QuantidadeBoletoBancario = _faturaAluguelRepository.QuantidadeTotalBoletoBancario();
            ViewBag.QuantidadeCartaoCredito = _faturaAluguelRepository.QuantidadeTotalCartaoCredito();

            ViewBag.Despesas = _registroRepository.ObterTotalPeriodo(inicio, fim);

            return View();
        }

        [ColaboradorAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            _loginCliente.Logout();
            return RedirectToAction("~/Cliente/Home/Login/");
        }

        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarSenha(ControleFrotasDLL.BLL.Cliente cliente)
        {
            if (ModelState.IsValid && cliente != null)
            {
                List<ControleFrotasDLL.BLL.Cliente> clientes = _repositoryCliente.ObterClientePorEmail(cliente.Email);

                clientes[0].Senha = KeyGenerator.GetUniqueKey(8);
                _repositoryCliente.AtualizarSenha(clientes[0]);

                _gerenciarEmail.EnviarSenhaParaClientePorEmail(clientes[0]);

                TempData["MSG_S"] = Mensagem.MSG_S003;

            }
            else
            {
                TempData["MSG_E"] = Mensagem.MSG_E016;
                return View();
            }
            return RedirectToAction(nameof(RecuperarSenha));

        }
    }
}
