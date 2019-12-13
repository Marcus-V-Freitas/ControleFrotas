using ControleFrotasDLL.BLL.Libraries.Lang;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Validacao
{
    public class EmailUnicoClienteAttribute : ValidationAttribute
    {
        /*
          *  Classe que valida se o email que o cliente quer cadastrar/Atualizar já existe na base de dados
          */
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(Mensagem.MSG_E001);
            }
            string Email = (value as string).Trim(); //Remover espaços em branco

            IClienteRepository _clienteRepository = (IClienteRepository)validationContext.GetService(typeof(IClienteRepository));

            List<Cliente> clientes = _clienteRepository.ObterClientePorEmail(Email);

            Cliente cliente = (Cliente)validationContext.ObjectInstance;

            if (clientes.Count > 1)
            {
                return new ValidationResult(Mensagem.MSG_E015);
            }

            if (clientes.Count == 1 && cliente.Id != clientes[0].Id)
            {
                return new ValidationResult(Mensagem.MSG_E015);
            }

            return ValidationResult.Success;
        }
    }
}
