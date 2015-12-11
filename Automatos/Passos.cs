using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatos
{
    public class Passos
    {

        public Passos(string entrada, string saida)
        {
            _entrada = entrada;
            _destino = saida;
        }

        string _entrada = "";

        string _destino = "";

        /// <summary>
        /// Entrada realizada para o automato
        /// </summary>
        public string Entrada
        {
            get
            {
                return _entrada;
            }

            set
            {
                _entrada = value;
            }
        }


        /// <summary>
        /// Destino do automato para a entrada realizada
        /// </summary>
        public string Destino
        {
            get
            {
                return _destino;
            }

            set
            {
                _destino = value;
            }
        }
    }
}
