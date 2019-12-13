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
    public class RegistroUso
    {
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public DateTime Inicio { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public DateTime Prevista { get; set; }

       // [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public DateTime? Retorno { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Rota { get; set; }

        [Display(Name = "Motorista")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int? RegistroUsoMotoristaId { get; set; }

        [ForeignKey(nameof(RegistroUsoMotoristaId))]
        public virtual Motorista Motorista { get; set; }

        [Display(Name = "Veículo")]
        public int? RegistroVeiculoClienteId { get; set; }

        [ForeignKey(nameof(RegistroVeiculoClienteId))]
        public virtual VeiculoCliente VeiculoCliente { get; set; }
    }
}
