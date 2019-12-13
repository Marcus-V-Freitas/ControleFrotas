using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Middleware
{
    /*
     * Classe para validação de AntiForgeryToken -
     * Evitar ataques maliciosos CSRF por método Post (Valido para toda a aplicação)
     */
    public class ValidateAntiForgeryTokenMiddleware
        {
            private RequestDelegate _next;
            private IAntiforgery _antiforgery;

            public ValidateAntiForgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
            {
                _next = next;
                _antiforgery = antiforgery;
            }

        
            public async Task Invoke(HttpContext context)
            {
            var Cabecalho = context.Request.Headers["x-requested-with"];
            bool AJAX = (Cabecalho == "XMLHttpRequest") ? true : false;

            //Verifica se a requisição veio do próprio site ou é um arquivo do tipo AJAX (Evita envios não autorizados)
            if (HttpMethods.IsPost(context.Request.Method) && !(context.Request.Form.Files.Count == 1 && AJAX))
            {
                await _antiforgery.ValidateRequestAsync(context);
            }

            await _next(context);
        }


    }
    }