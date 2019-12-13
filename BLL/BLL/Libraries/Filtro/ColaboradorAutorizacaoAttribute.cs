using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Filtro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private string _TipoColaboradorAutorizado;

        public ColaboradorAutorizacaoAttribute(string TipoColaboradorAutorizado = ColaboradorTipoConstant.Comum)
        {
            _TipoColaboradorAutorizado = TipoColaboradorAutorizado;
        }

        private LoginColaborador _loginColaborador;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));

            BLL.Colaborador colaborador = _loginColaborador.GetColaborador();

            if (colaborador == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if (colaborador.Tipo == ColaboradorTipoConstant.Comum && _TipoColaboradorAutorizado == ColaboradorTipoConstant.Gerente)
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
            

        }
    }
}
