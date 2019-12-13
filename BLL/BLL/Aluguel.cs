using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.BLL.Libraries.Validacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL
{
   public class Aluguel
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        public string TransactionId { get; set; } //PagarMe - Transaction -> ID.

        public string DadosTransaction { get; set; } //Transaction - JSON

        public string FormaPagamento { get; set; } //Boleto-Cartão Credito

        [Display(Name = "Data de Inicio")]
        public string DataInicio { get; set; }

        [Display(Name = "Data Prevista")]
        [HorarioAluguel]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string DataPrevista { get; set; }

        [Display(Name = "Valor Previsto")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double ValorPrevisto { get; set; }

        //[Display(Name = "Seguro")]
        //[Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        //public string Seguro { get; set; }

        [Display(Name = "Seguro")]
        public int? AluguelSeguroId { get; set; }

        [ForeignKey(nameof(AluguelSeguroId))]
        public virtual Seguro Seguro { get; set; }

        [Display(Name = "Veiculo")]
        public int? AluguelVeiculoId { get; set; }

        [ForeignKey(nameof(AluguelVeiculoId))]
        public virtual VeiculoEmpresa VeiculoEmpresa { get; set; }

        [Display(Name = "Cliente")]
        public int? AluguelClienteId { get; set; }

        [ForeignKey(nameof(AluguelClienteId))]
        public virtual ClienteJuridico ClienteJuridico { get; set; }

        [Display(Name = "Motorista")]
        public int? AluguelMotoristaId { get; set; }

        [ForeignKey(nameof(AluguelMotoristaId))]
        public virtual Motorista Motorista { get; set; }

        [NotMapped]
        public string ListaProdutos { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int Status { get; set; }
    }
}
