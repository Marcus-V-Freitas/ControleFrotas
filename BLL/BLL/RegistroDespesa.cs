using ControleFrotasDLL.BLL.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL
{
    public class RegistroDespesa
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public DateTime? Data { get; set; }

        [Display(Name = "Veiculo")]
        public int? RegistroVeiculoId { get; set; }

        [ForeignKey(nameof(RegistroVeiculoId))]
        public virtual VeiculoCliente VeiculoCliente { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double Total { get; set; }

        [Display(Name = "Motorista")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int? RegistroMotoristaId { get; set; }

        [ForeignKey(nameof(RegistroMotoristaId))]
        public virtual Motorista Motorista { get; set; }


        [NotMapped]
        public string ListaProdutos { get; set; }
    }
}
