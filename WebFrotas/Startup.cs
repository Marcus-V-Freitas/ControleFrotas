using System;
using System.Net;
using System.Net.Mail;
using ControleFrotasDLL.BLL.Libraries.Email;
using ControleFrotasDLL.BLL.Libraries.Login;
using ControleFrotasDLL.BLL.Libraries.Sessao;
using ControleFrotasDLL.DAL.Database.SQL;
using ControleFrotasDLL.DAL.Database.SQL.Data;
using ControleFrotasDLL.DAL.Repositories;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ControleFrotasDLL.BLL.Libraries.Middleware;
using ControleFrotasDLL.BLL.Libraries.Validacao;
using ControleFrotasDLL.BLL.Libraries.Pagamento;
using ControleFrotasDLL.BLL.Libraries.AutoMapper;
using AutoMapper;

namespace WebFrotas
{
    public class Startup
    {
        //Inicia o Startup com as configurações do  sistema
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Este método é chamado em tempo de execução e é usado para adicionar serviços ao contêiner.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
             * API - Logging (Instalar o pacote Serilog)
             * Pacote nuget usado para gravar logs do sistema (Nesse caso em arquivo .txt)
             */

            services.AddLogging();

            /*
             * AutoMapper - AutoMapper.Extensions.Microsoft.DependencyInjection
             * Usado para introduzir uma injecção de depedência para todo a solução (Converte tipos não diretamente
             * relacionados de forma a haver compatibilidade)
             */

            services.AddAutoMapper(config => config.AddProfile<MappingProfile>(),AppDomain.CurrentDomain.GetAssemblies());



            //Padrão Repository e acesso a sessão (Incluir)
            services.AddHttpContextAccessor();

            //Singletons dos repositórios e suas devidas interfaces (Uma instância para cada requisição)
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IClienteFisicoRepository, ClienteFisicoRepository>();
            services.AddScoped<IClienteJuridicoRepository, ClienteJuridicoRepository>();
            services.AddScoped<IMotoristaRepository, MotoristaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IModeloRepository, ModeloRepository>();
            services.AddScoped<IRegistroRepository, RegistroRepository>();
            services.AddScoped<IDespesaRepository, DespesaRepository>();
            services.AddScoped<IVeiculoClienteRepository, VeiculoClienteRepository>();
            services.AddScoped<IAluguelRepository, AluguelRepository>();
            services.AddScoped<IVeiculoEmpresaRepository, VeiculoEmpresaRepository>();
            services.AddScoped<IFaturaAluguelRepository, FaturaAluguelRepository>();
            services.AddScoped<ISeguroRepository, SeguroRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IUnidMedRepository, UnidMedRepository>();
            services.AddScoped<ICategoriaVeiculoRepository, CategoriaVeiculoRepository>();
            services.AddScoped<IRegistroUsoRepository, RegistroUsoRepository>();


            /* 
            * SMTP - Serviço Envio Emails (Incluir)
            */

            //Singleton SmtpClient - Servidor do Gmail
            services.AddScoped(options => {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:ServerPort"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:Username"), Configuration.GetValue<string>("Email:PasswordLocal")),
                    EnableSsl = true
                };
                return smtp;
            });

            //DI simples - Gerenciar Email
            services.AddScoped<GerenciarEmail>();

            //Funcionar os cookies (Incluir)
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Definir um tempo limite curto para evitar problemas relacionados ao desempenho. 
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                // Tornar o "cookie de sessão" essencial
                options.Cookie.IsEssential = true;
            });


            services.Configure<CookiePolicyOptions>(options =>
            {
                //Essa instrução lambda determina se o consentimento do usuário para os "cookies não essenciais" é necessário para uma solicitação.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //Session - Configuração
            /*
             * Guardar os dados na memoria
             */
            services.AddMemoryCache(); //Usado para que a Sessão funcione (Incluir)
            services.AddSession(options => { }); //Usado para que a Sessão funcione (Incluir)

            //services.AddSession(options =>
            //{
            //    options.Cookie.IsEssential = true;
            //});

            services.AddScoped<GerenciarPagarMe>(); //Adiciona a classe para geração de pagamento
            services.AddScoped<SeedingService>(); //Adiciona Classe para popular base de dados
            services.AddScoped<Sessao>(); //Adicionar Sessão
            services.AddScoped<LoginCliente>(); //Adicionar Login Cliente
            services.AddScoped<LoginColaborador>(); //Adicionar Login Colaborador
            services.AddMvc().AddSessionStateTempDataProvider();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); //Indica que a compatibilidade do padrão é .NET CORE 2.1

            //Instalar pacote Microsoft.EntityFrameworkCore.Tools Version 2.1.1 (SQL SERVER) /String de Conexão com SQL Server

            //Pegar a string de conexão de acordo com necessidade (Local: appsetings.Development.json)
            //string connection = Configuration.GetValue<string>("Connection:SmarterASPMSSQL");

            //Banco de Dados Local (Testes) 
            string connection = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ControleFrota;Integrated Security=True;" +
                "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //Banco de Dados Externo (SmarterASP)
            // string connection = "Data Source=SQL5047.site4now.net;Initial Catalog=DB_A4EDFE_controlefrotas;User Id=DB_A4EDFE_controlefrotas_admin;Password=Vaders2233;";

            //Banco de Dados Externo (Somee)
            // string connection = "workstation id=ControleFrota.mssql.somee.com;packet size=4096;user id=MarcusCore20_SQLLogin_1;pwd=feojfyp3rj;data source=ControleFrota.mssql.somee.com;persist security info=False;initial catalog=ControleFrota";

            //DI do Context da aplicação (EntityFrameworkCore) - Definição da Pasta de Migrations para WebFrotas
            services.AddDbContext<ControleFrotasContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly(nameof(WebFrotas))));
        }

        // Este método é chamado em tempo de execução para configurar o pipeline de solicitação HTTP.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService) //Aplica ao depurar o código
        {
            // Middlewares do Software

            if (env.IsDevelopment()) //Habilita mensagem de error ambiente de desenvolvimento
            {
                app.UseDeveloperExceptionPage();
                seedingService.seed(); //Popula a base de dados com os dados padrões
            }
            else
            {
                // app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}"); //Redireciona para qualquer página de erro que use HTTP (lado Cliente)
                app.UseExceptionHandler("/Error/Error500"); //Redireciona para página de erro do lado do servidor
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles(); //Ativa o uso de arquivos padrão
            app.UseStaticFiles();

            app.UseSession(); //Métodos usado para fazer funcionar a Sessão (Incluir)
            app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>(); //AntiForgeryToken - Evitar ataques maliciosos CSRF por método Post (Valido para toda a aplicação)
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                //Rota adicionada para Area do Colaborador e Cliente (Tem a prioridade de chamada)
                routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}