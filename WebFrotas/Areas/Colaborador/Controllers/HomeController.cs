using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleFrotasDLL.BLL.Libraries.Filtro;
using ControleFrotasDLL.BLL.Libraries.Login;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;



namespace WebFrotas.Areas.Colaborador.Controllers
{
    [Area(nameof(Colaborador))]  //Especificar que o controller direciona 
    public class HomeController : Controller
    {

        private LoginColaborador _loginColaborador;
        private IColaboradorRepository _repositoryColaborador;
        private INewsletterRepository _newsletterRepository;
        private IFaturaAluguelRepository _faturaAluguelRepository;
        private IClienteRepository _clienteRepository;
        private IVeiculoEmpresaRepository _veiculoEmpresaRepository;
        private IFornecedorRepository _fornecedorRepository;

        public HomeController(IColaboradorRepository repositoryColaborador, LoginColaborador loginColaborador, INewsletterRepository newsletterRepository,
            IFaturaAluguelRepository faturaAluguelRepository, IClienteRepository clienteRepository, IVeiculoEmpresaRepository veiculoEmpresaRepository, 
            IFornecedorRepository fornecedorRepository)
        {
            _repositoryColaborador = repositoryColaborador;
            _loginColaborador = loginColaborador;
            _newsletterRepository = newsletterRepository;
            _faturaAluguelRepository = faturaAluguelRepository;
            _clienteRepository = clienteRepository;
            _veiculoEmpresaRepository = veiculoEmpresaRepository;
            _fornecedorRepository = fornecedorRepository;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm]ControleFrotasDLL.BLL.Colaborador colaborador) //Evitar Confusões com o nome da area
        {
            ControleFrotasDLL.BLL.Colaborador colaboradorDB = await _repositoryColaborador.Login(colaborador.Email, colaborador.Senha);


            if (colaboradorDB != null)
            {

                _loginColaborador.Login(colaboradorDB);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado, verifique o e-mail e senha digitado!";

                return View();
            }
        }

        [ColaboradorAutorizacao]
        public IActionResult Painel()
        {
            ViewBag.Clientes = _clienteRepository.QuantidadeTotalClientes();
            ViewBag.Newsletter = _newsletterRepository.QuantidadeTotalNewsletters();

            ViewBag.Fornecedores = _fornecedorRepository.QuantidadeTotalFornecedores();
            ViewBag.NumeroAlugueis = _faturaAluguelRepository.QuantidadeTotalAlugueis();
            ViewBag.ValorTotalAlugueis = _faturaAluguelRepository.ValorTotalAlugueis();
            ViewBag.VeiculosEmpresa = _veiculoEmpresaRepository.QuantidadeTotalVeiculosEmpresa();
            ViewBag.QuantidadeBoletoBancario = _faturaAluguelRepository.QuantidadeTotalBoletoBancario();
            ViewBag.QuantidadeCartaoCredito = _faturaAluguelRepository.QuantidadeTotalCartaoCredito();
            return View();
        }

        [ColaboradorAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            _loginColaborador.Logout();
            return RedirectToAction("Login", "Home", "");
        }

        public IActionResult GerarCSVNewsletter()
        {
            var news = _newsletterRepository.ObterTodasNewsletter();

            StringBuilder sb = new StringBuilder();

            foreach (var email in news)
            {
                sb.AppendLine(email.Email);
            }

            byte[] buffer = Encoding.ASCII.GetBytes(sb.ToString());
            return File(buffer, "text/csv", $"newsletter.csv");
        }
    }
}
