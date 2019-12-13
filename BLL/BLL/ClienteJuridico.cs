using ControleFrotasDLL.BLL.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace ControleFrotasDLL.BLL
{
    public class ClienteJuridico
    {
        [Display(Name = "Código")]
        public int? ClienteJuridicoId { get; set; }

        [ForeignKey(nameof(ClienteJuridicoId))]
        public virtual Cliente Cliente { get; set; }

        [Display(Name ="Razão Social")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string RazaoSocial { get; set; }
    }
}