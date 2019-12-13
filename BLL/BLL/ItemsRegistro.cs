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
    public class ItemsRegistro
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Registro")]
        public int? RegistroId { get; set; }

        [ForeignKey(nameof(RegistroId))]
        public virtual RegistroDespesa RegistroDespesa { get; set; }

        [Display(Name = "Item")]
        public int? DespesaId { get; set; }

        [ForeignKey(nameof(DespesaId))]
        public virtual Despesa Despesa { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double QuantidadeItem { get; set; }

        [Display(Name = "Preço da Unidade")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double PrecoUnitario { get; set; }


    }
}
