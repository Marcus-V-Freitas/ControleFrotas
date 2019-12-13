using ControleFrotasDLL.BLL.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL
{
   public class UnidadeMedida
    {
        [Display(Name = "Código")]
        public int Id { get; set; }
        

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Nome { get; set; }

        public UnidadeMedida() { }

        public UnidadeMedida(string nome)
        {
            Nome = nome;
        }
    }
}
