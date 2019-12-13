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
        public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter
        {

        private string _TipoClienteAutorizado;

        public ClienteAutorizacaoAttribute(string TipoClienteAutorizado = ClienteTipoConstant.Fisico)
        {
            _TipoClienteAutorizado = TipoClienteAutorizado;
        }

        private LoginCliente _loginCliente;

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                _loginCliente = (LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));

                Cliente cliente = _loginCliente.GetCliente();

            if (cliente == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if (cliente.Tipo == ClienteTipoConstant.Fisico && _TipoClienteAutorizado == ClienteTipoConstant.Juridico)
                {
                    context.Result = new StatusCodeResult(403);
                }
            }


        }
        }
    }
