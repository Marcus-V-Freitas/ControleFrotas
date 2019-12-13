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
    public class Motorista
    {
        [Key]
        [Display(Name = "Código")]
        public int? ClienteMotoristaId { get; set; }

        [ForeignKey(nameof(ClienteMotoristaId))]
        public virtual Cliente Cliente { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Sexo { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string RG { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Renach { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string CNH { get; set; }

        [Display(Name ="Empresa")]
        public int? EmpresaId { get; set; }

        [ForeignKey(nameof(EmpresaId))]
        public virtual ClienteJuridico clienteJuridico { get; set; }
    }
}
