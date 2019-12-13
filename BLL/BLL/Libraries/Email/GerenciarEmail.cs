using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Email
{
   public class GerenciarEmail
    {
        private SmtpClient _smtp;
        private IConfiguration _conf;

        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _conf = configuration;
        }


        /*
         * Envio de mensagem referente ao suporte que o cliente solicita (View Contato)
         */

        public void EnviarContatoEmail(Contato contato)
        {

            string corpoMsg = string.Format("<h2>Contato - LojaVirtual</h2>" +
                "<b>Nome: </b> {0} <br />" +
                "<b>Email: </b> {1} <br />" +
                "<b>Texto: </b> {2} <br />" +
                "<br /> E-mail enviado automaticamente do site LojaVirtual", contato.Nome, contato.Email, contato.Texto);

            /*
             * MailMessage -> Construir a mensagem 
             */

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(_conf.GetValue<string>("Email:Username"));
            mensagem.To.Add(contato.Email);
            mensagem.Subject = "Contato - LojaVirtual - E-mail" + contato.Email;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar a mensagem via SMTP
            _smtp.Send(mensagem);
        }

        /*
        * Envio de mensagem referente a alteração de senha do Colaborador
        */

        public void EnviarSenhaParaColaboradorPorEmail(Colaborador colaborador)
        {
            string corpoMsg = string.Format("<h2>Colaborador - LojaVirtual</h2>" +
                "Sua senha é:" +
                "<h3> {0} </h3>", colaborador.Senha);

            /*
             * MailMessage -> Construir a mensagem 
             */

            MailMessage mensagem = new MailMessage
            {
                From = new MailAddress(_conf.GetValue<string>("Email:Username"))
            };
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador - LojaVirtual - Senha do colaborador - " + colaborador.Nome;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar a mensagem via SMTP
            _smtp.Send(mensagem);
        }

        public void EnviarSenhaParaClientePorEmail(Cliente cliente)
        {
            string corpoMsg = string.Format("<h2>Cliente - LojaVirtual</h2>" +
                "Sua senha é:" +
                "<h3> {0} </h3>", cliente.Senha);

            /*
             * MailMessage -> Construir a mensagem 
             */

            MailMessage mensagem = new MailMessage
            {
                From = new MailAddress(_conf.GetValue<string>("Email:Username"))
            };
            mensagem.To.Add(cliente.Email);
            mensagem.Subject = "Cliente - LojaVirtual - Senha do colaborador - " + cliente.Nome;
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar a mensagem via SMTP
            _smtp.Send(mensagem);
        }


    }
}
