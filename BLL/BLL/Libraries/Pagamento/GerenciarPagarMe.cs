using ControleFrotasDLL.BLL.Libraries.Texto;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using PagarMe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ControleFrotasDLL.BLL.Libraries.Pagamento
{
    public class GerenciarPagarMe
    {
        private IConfiguration _configuration;
        private IClienteRepository _clienteRepository;
        private IMotoristaRepository _motoristaRepository;

        public GerenciarPagarMe(IConfiguration configuration, IClienteRepository clienteRepository, IMotoristaRepository motoristaRepository)
        {
            _configuration = configuration;
            _clienteRepository = clienteRepository;
            _motoristaRepository = motoristaRepository;
        }


        public Transaction GerarBoleto(Aluguel aluguel)
        {
            Cliente cliente = _clienteRepository.ObterCliente(Convert.ToInt32(aluguel.AluguelClienteId));

            PagarMeService.DefaultApiKey = "ak_test_7VAjSBubdQj1qT3Kgc7AWluUKfiv3M";//_configuration.GetValue<string>("Pagamento:PagarMe:ApiKey");
            PagarMeService.DefaultEncryptionKey = "ek_test_KEA6ruzcJ7tTF8dEEJ6ik6rWfXfiGW";//_configuration.GetValue<string>("Pagamento:PagarMe:EncryptionKey");
            int DaysExpire = 2;

            Transaction transaction = new Transaction();

            transaction.Amount = Mascara.ConverterValorPagarMe(aluguel.ValorPrevisto);
            transaction.PaymentMethod = PaymentMethod.Boleto;
            transaction.BoletoExpirationDate = DateTime.Now.AddDays(DaysExpire);

            transaction.Customer = new Customer
            {
                ExternalId = cliente.Id.ToString(),
                Name = cliente.Nome,
                Type = CustomerType.Individual,
                Country = "br",
                Email = cliente.Email,
                Documents = new[] {
                        new Document{
                            Type = DocumentType.Cnpj,
                            Number = Mascara.Remover(cliente.CPFCNPJ)
                        }
                    },
                PhoneNumbers = new string[]
               {
                        "+55" + Mascara.Remover( cliente.Telefone )
               },
                //Birthday = Mascara.Remover(cliente.Nascimento)
            };

            Item[] itens = new Item[1];

            for (var i = 0; i < itens.Length; i++)
            {
                var item = aluguel;

                var itemA = new Item()
                {
                    Id = item.Id.ToString(),
                    Title = "Aluguel",
                    Quantity = 1,
                    Tangible = false,
                    UnitPrice = Mascara.ConverterValorPagarMe(item.ValorPrevisto)
                };


                itens[i] = itemA;
            }

            transaction.Item = itens;

            transaction.Save();

           // transaction.Customer.Gender = (cliente.Sexo == "M") ? Gender.Male : Gender.Female;

            return transaction;
        }

        public Transaction GerarPagCartaoCredito(CartaoCredito cartao,  Aluguel aluguel, Parcelamento parcela)
        {
            Cliente cliente = _clienteRepository.ObterCliente(Convert.ToInt32(aluguel.AluguelMotoristaId));
            Motorista motorista = _motoristaRepository.ObterMotorista(Convert.ToInt32(aluguel.AluguelMotoristaId));


            PagarMeService.DefaultApiKey = "ak_test_7VAjSBubdQj1qT3Kgc7AWluUKfiv3M";//_configuration.GetValue<string>("Pagamento:PagarMe:ApiKey");
            PagarMeService.DefaultEncryptionKey = "ek_test_KEA6ruzcJ7tTF8dEEJ6ik6rWfXfiGW";//_configuration.GetValue<string>("Pagamento:PagarMe:EncryptionKey");


            Card card = new Card();
            card.Number = cartao.NumeroCartao;
            card.HolderName = cartao.NomeNoCartao;
            card.ExpirationDate = cartao.VecimentoMM + cartao.VecimentoYY;
            card.Cvv = cartao.CodigoSeguranca;

            card.Save();

            Transaction transaction = new Transaction();
            transaction.Amount = Mascara.ConverterValorPagarMe(aluguel.ValorPrevisto);
            transaction.PaymentMethod = PaymentMethod.CreditCard;
            /*
             * Transaction.postbackurl
             * - Parâmetro importante para que seu site seja informado sobre todas as mudanças de status ocorridas no Pagar.Me.
             * URL 1: https://pagarme.zendesk.com/hc/pt-br/articles/205973476-Quando-o-POSTback-%C3%A9-enviado-
             * URL 2: https://docs.pagar.me/v1/reference#criar-transacao
             */

            transaction.Card = new Card
            {
                Id = card.Id
            };

            transaction.Customer = new Customer
            {
                ExternalId = cliente.Id.ToString(),
                Name = cliente.Nome,
                Type = CustomerType.Individual,
                Country = "br",
                Email = cliente.Email,
                Documents = new[] {
                        new Document{
                            Type = DocumentType.Cpf,
                            Number = Mascara.Remover(cliente.CPFCNPJ)
                        }
                    },
                PhoneNumbers = new string[]
                    {
                        "+55" + Mascara.Remover( cliente.Telefone )
                    },
                Birthday = Mascara.Remover(cliente.Nascimento)
            };

            transaction.Billing = new Billing
            {
                Name = cliente.Nome,
                Address = new Address()
                {
                    Country = "br",
                    State = cliente.Estado,
                    City = cliente.Cidade,
                    Neighborhood = cliente.Bairro,
                    Street = cliente.Endereco + " " + cliente.Complemento,
                    StreetNumber = cliente.Numero,
                    Zipcode = Mascara.Remover(cliente.CEP)
                }
            };


            Item[] itens = new Item[1];

            for (var i = 0; i < itens.Length; i++)
            {
                var item = aluguel;

                var itemA = new Item()
                {
                    Id = item.Id.ToString(),
                    Title = "Aluguel",
                    Quantity = 1,
                    Tangible = false,
                    UnitPrice = Mascara.ConverterValorPagarMe(item.ValorPrevisto)
                };


                itens[i] = itemA;
            }

            transaction.Item = itens;

            transaction.Installments = parcela.Numero;

            transaction.Save();

            transaction.Customer.Gender = (motorista.Sexo == "M") ? Gender.Male : Gender.Female;
            return transaction;
        }

        public List<Parcelamento> CalcularPagamentoParcelado(decimal valor)
        {
            List<Parcelamento> lista = new List<Parcelamento>();

        
            int maxParcelamento =12;// _configuration.GetValue<int>("Pagamento:PagarMe:MaxParcelas");
            int parcelaPagaVendedor = 3;//_configuration.GetValue<int>("Pagamento:PagarMe:ParcelaPagaVendedor");
            decimal juros = 5;//_configuration.GetValue<decimal>("Pagamento:PagarMe:Juros");

            for (int i = 1; i <= maxParcelamento; i++)
            {
                Parcelamento parcelamento = new Parcelamento();
                parcelamento.Numero = i;

                if (i > parcelaPagaVendedor)
                {
                    //Juros - i = (4-3 - parcelaPagaVendedor) + 5%
                    int quantidadeParcelasComJuros = i - parcelaPagaVendedor;
                    decimal valorDoJuros = valor * juros / 100;

                    parcelamento.Valor = quantidadeParcelasComJuros * valorDoJuros + valor;
                    parcelamento.ValorPorParcela = parcelamento.Valor / parcelamento.Numero;
                    parcelamento.Juros = true;

                }
                else
                {
                    parcelamento.Valor = valor;
                    parcelamento.ValorPorParcela = parcelamento.Valor / parcelamento.Numero;
                    parcelamento.Juros = false;
                }
                lista.Add(parcelamento);
            }

            return lista;
        }
    }
}