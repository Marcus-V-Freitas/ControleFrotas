using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.BLL.Libraries.Validacao.ControleFrotasDLL.BLL.Libraries.Validacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL
{
   public class Despesa
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Descricao { get; set; }

        [Display(Name = "Preço da Unidade")]
        [NumeroBrasil(ErrorMessage = "Número inválido", DecimalRequerido = true, PontoMilharPermitido = false)]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double PrecoUnitario { get; set; }

        [Display(Name = "Unidade de Medida")]
        public int? DespesaMedidaId { get; set; }

        [ForeignKey(nameof(DespesaMedidaId))]
        public virtual UnidadeMedida UnidadeMedida { get; set; }

        [Display(Name = "Endereço Imagem")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Link { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int? DespesaClienteId { get; set; }

        [ForeignKey(nameof(DespesaClienteId))]
        public virtual Cliente Cliente { get; set; }

    }
}
