using ControleFrotasDLL.BLL.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL
{
    public class CategoriaVeiculo
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Nome { get; set; }

        [Display(Name ="Descrição")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Descricao { get; set; }

        public CategoriaVeiculo() { }

        public CategoriaVeiculo(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
    }
}
