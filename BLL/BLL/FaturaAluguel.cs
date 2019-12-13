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
    public class FaturaAluguel
    {
        [Key]
        [Display(Name = "Código")]
        public int? AluguelId { get; set; }

        [ForeignKey(nameof(AluguelId))]
        public virtual Aluguel Aluguel { get; set; }

        [Display(Name = "Data de Inicio")]
        public string DataInicio { get; set; }

        [Display(Name = "Data de Retorno")]
        public string DataRetorno { get; set; }

        [Display(Name = "Valor Total")]
        [Required(ErrorMessageResourceType =typeof(Mensagem), ErrorMessageResourceName ="MSG_E001")]
        public double ValorTotal { get; set; }

        [NotMapped]
        public string ListaProdutos { get; set; }
    }
}
