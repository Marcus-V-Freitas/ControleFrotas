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
    public class Seguro
    {
        [Display(Name ="Código")]
        public int Id { get; set; }

        [Display(Name = "Fornecedor")]
        public int? FornecedorId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public double Custo { get; set; }

        [ForeignKey(nameof(FornecedorId))]
        public virtual Fornecedor Fornecedor { get; set; }

        public Seguro() { }

        public Seguro(string nome, double custo, Fornecedor fornecedor)
        {
            Nome = nome;
            Custo = custo;
            Fornecedor = fornecedor;
        }
    }
}
