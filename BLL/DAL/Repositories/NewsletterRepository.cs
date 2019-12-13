using ControleFrotasDLL.BLL;
using ControleFrotasDLL.DAL.Database.SQL;
using ControleFrotasDLL.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.DAL.Repositories
{
    public class NewsletterRepository:INewsletterRepository
    {
        private ControleFrotasContext _banco;

        public NewsletterRepository(ControleFrotasContext banco)
        {
            _banco = banco;
        }

        public async Task Cadastrar(NewsletterEmail newsletterEmail)
        {
            _banco.Add(newsletterEmail);
           await _banco.SaveChangesAsync();
        }

        public IEnumerable<NewsletterEmail> ObterTodasNewsletter()
        {
            return _banco.NewsletterEmails.ToList();
        }

        public int QuantidadeTotalNewsletters()
        {
            return _banco.NewsletterEmails.Count();
        }
    }
}
