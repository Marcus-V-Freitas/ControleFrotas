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
   public class Veiculo
    {
        public int Id{ get; set; }


        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Placa]
        public string Placa { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Renavam]
        public string Renavam { get; set; }

        [Display(Name ="Endereço da Foto")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Link_Foto { get; set; }

        [Display(Name ="Modelo")]
        public int? ModeloId { get; set; }

        [ForeignKey(nameof(ModeloId))]
        public virtual Modelo Modelo { get; set; }

        [Display(Name = "Categoria")]
        public int? CategoriaVeiculoId { get; set; }

        [ForeignKey(nameof(CategoriaVeiculoId))]
        public virtual CategoriaVeiculo CategoriaVeiculo { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        public Veiculo() { }

        public Veiculo(string placa, string renavam, string link_foto, Modelo modelo, CategoriaVeiculo categoriaVeiculo)
        {
            Placa = placa;
            Renavam = renavam;
            Link_Foto = link_foto;
            Modelo = modelo;
            CategoriaVeiculo = categoriaVeiculo;
        }

      

    }
}
