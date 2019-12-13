using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Login
{
    public class LoginColaborador
    {
        private string key = "Login.Colaborador";
        private Sessao.Sessao _sessao;

        public LoginColaborador(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Colaborador colaborador)
        {
            //Serializar

            string colaboradorJsonString = JsonConvert.SerializeObject(colaborador);
            //Armazenar na Sessão
            _sessao.Cadastrar(key, colaboradorJsonString);

        }

        public Colaborador GetColaborador()
        {
            //Deserializar
            if (_sessao.Existe(key))
            {
                string colaboradorJsonString = _sessao.Consultar(key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJsonString);
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
