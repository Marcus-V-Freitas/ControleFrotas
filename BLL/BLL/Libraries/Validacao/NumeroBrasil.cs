using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Validacao
{
    namespace ControleFrotasDLL.BLL.Libraries.Validacao
    {
        /*
         * Classe que valida se valores com casas decimais estão sendo introduzidos no sistema de acordo 
         * com o padrão brasileiro "Vírgula" ou invés de "Ponto"
         */
        public class NumeroBrasil : ValidationAttribute
        {
            public Boolean DecimalRequerido { get; set; }
            public Boolean PontoMilharPermitido { get; set; }
            public NumeroBrasil()
            {
                this.ErrorMessage = "Valor inválido.";
                this.DecimalRequerido = false;
                this.PontoMilharPermitido = false;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {

                // Verifica se o Valor é nulo
                if (value == null)
                {
                    value = "";
                }

                string newValue = value.ToString();

                // Verifica se Ponto milhar é permitido, se sim retira os pontos para validação
                if (PontoMilharPermitido)
                {
                    newValue = newValue.Replace(".", string.Empty);
                }

                // Caso o valor informado seja nulo não é requerido retorna sem validar
                if (value.ToString() == "" && DecimalRequerido == false)
                {
                    return ValidationResult.Success; ;
                }

                // Verifica se decimal é requerida para fazer a validacao como double ou com integer
                // Se a conversão não for possivel retor com erro padrão
                if (DecimalRequerido == true)
                {
                    try
                    {
                        double x = Convert.ToDouble(newValue);
                    }
                    catch
                    {
                        return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                    }
                }
                else
                {
                    try
                    {
                        Int32 x = Convert.ToInt32(newValue);
                    }
                    catch
                    {
                        return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                    }
                }

                // Retorna com sucesso caso a converão tenha sido feita
                return ValidationResult.Success;
            }
        }
    }
}
