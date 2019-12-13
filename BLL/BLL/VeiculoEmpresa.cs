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
    public class VeiculoEmpresa
    {
        [Display(Name ="Código")]
        public int? VeiculoEmpresaId { get; set; }

        [ForeignKey(nameof(VeiculoEmpresaId))]
        public virtual Veiculo Veiculo { get; set; }


        //[Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        //public int Status { get; set; }

        [Display(Name ="Preço do Dia")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double Preco_Dia { get; set; }

        public VeiculoEmpresa() { }

        public VeiculoEmpresa(Veiculo veiculo, double preco_Dia)
        {
            Veiculo = veiculo;
            Preco_Dia = preco_Dia;
        }


    }
}
