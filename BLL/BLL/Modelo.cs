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
    public class Modelo
    {
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType =typeof(Mensagem), ErrorMessageResourceName ="MSG_E001")]
        public string Nome { get; set; }

        [Display(Name ="Marca")]
        [Required(ErrorMessageResourceType =typeof(Mensagem), ErrorMessageResourceName ="MSG_E001")]
        public int MarcaId { get; set; }

        [ForeignKey(nameof(MarcaId))]
        public virtual Marca Marca { get; set; }

        public Modelo() { }

        public Modelo(string nome, Marca marca)
        {
            Nome = nome;
            Marca = marca;
        }
    }
}
