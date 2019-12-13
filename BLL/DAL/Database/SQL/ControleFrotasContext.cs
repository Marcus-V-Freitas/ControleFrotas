using ControleFrotasDLL.BLL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.DAL.Database.SQL
{
    public class ControleFrotasContext:DbContext
    {
        /*
         * EF Core e ORM
         *ORM -> Biblioteca para mapear Objetos para banco de dados relacional
         */

        public ControleFrotasContext(DbContextOptions<ControleFrotasContext> options) : base(options)
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<NewsletterEmail> NewsletterEmails { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Colaborador>Colaboradores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteFisico> ClienteFisicos { get; set; }
        public DbSet<ClienteJuridico> ClienteJuridicos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<RegistroDespesa> RegistrosDespesas { get; set; }
        public DbSet<VeiculoCliente> VeiculosCliente { get; set; }
        public DbSet<VeiculoEmpresa> VeiculosEmpresa { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<ItemsRegistro> ItemsRegistros { get; set; }
        public DbSet<FaturaAluguel> FaturaAlugueis { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }
        public DbSet<Seguro> Seguros { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<UnidadeMedida> UnidadeMedidas { get; set; }
        public DbSet<CategoriaVeiculo> CategoriaVeiculos { get; set; }
        public DbSet<RegistroUso> RegistrosUsos { get; set; }
    }
}
