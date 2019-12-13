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
   public class VeiculoCliente
    {

        [Display(Name ="Código")]
        public int? VeiculoClienteId { get; set; }

        [ForeignKey(nameof(VeiculoClienteId))]
        public virtual Veiculo Veiculo { get; set; }

        [Display(Name ="Descrição")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Descricao { get; set; }

        [Display(Name ="Cliente")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int? ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public virtual Cliente Cliente { get; set; }


        ICollection<RegistroDespesa> Registros { get; set; } = new List<RegistroDespesa>();

        public VeiculoCliente() { }

        public VeiculoCliente(Veiculo veiculo, string descricao, Cliente cliente)
        {
            Veiculo = veiculo;
            Descricao = descricao;
            Cliente = cliente;
        }
    }
}
