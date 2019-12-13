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
    public class EmailUnicoColaboradorAttribute : ValidationAttribute
    {
        /*
      *  Classe que valida se o email que o Colaborador quer cadastrar/Atualizar já existe na base de dados
      */
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(Mensagem.MSG_E001);
            }
            string Email = (value as string).Trim(); //Remover espaços em branco

            IColaboradorRepository _colaboradorRepository = (IColaboradorRepository)validationContext.GetService(typeof(IColaboradorRepository));

            List<Colaborador> colaboradores =  _colaboradorRepository.ObterColaboradorPorEmail(Email);

            Colaborador colaborador = (Colaborador)validationContext.ObjectInstance;

            if (colaboradores.Count > 1)
            {
                return new ValidationResult(Mensagem.MSG_E015);
            }

            if (colaboradores.Count == 1 && colaborador.Id != colaboradores[0].Id)
            {
                return new ValidationResult(Mensagem.MSG_E015);
            }

            return ValidationResult.Success;
        }
    }
}
