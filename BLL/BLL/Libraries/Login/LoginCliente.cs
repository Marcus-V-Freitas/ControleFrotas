using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.BLL.Libraries.Login
{
    //Instalar o pacote Newtonsoft.Json (Serializar/Deserializar)
    public class LoginCliente
    {
        private string key = "Login.Cliente";
        private Sessao.Sessao _sessao;

        public LoginCliente(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Cliente cliente)
        {
            //Serializar

            string clienteJsonString = JsonConvert.SerializeObject(cliente);
            //Armazenar na Sessão
            _sessao.Cadastrar(key, clienteJsonString);

        }

        public Cliente GetCliente()
        {
            //Deserializar
            if (_sessao.Existe(key))
            {
                string clienteJsonString = _sessao.Consultar(key);
                return JsonConvert.DeserializeObject<Cliente>(clienteJsonString);
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


        public int? Tipo()
        {
            if (GetCliente() != null)
            {
                return GetCliente().Id;
            }
            else
            {
                return null;
            }

        }
    }
}
