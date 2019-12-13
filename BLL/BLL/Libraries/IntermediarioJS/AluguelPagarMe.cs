using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ControleFrotasDLL.BLL.Libraries.IntermediarioJS
{
    public class AluguelPagarMe
    {
        /*
        * Classe que serve de intermediária para preenchimento dos campos da classe Aluguel referentes a transação
        */
        public string TransactionId { get; set; } //PagarMe - Transaction -> ID.

        public string DadosTransaction { get; set; } //Transaction - JSON

        public string FormaPagamento { get; set; } //Boleto-Cartão Credito
    }
}