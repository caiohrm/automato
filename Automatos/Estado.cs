using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatos
{
    public class Estado
    {
        public Estado(string nome)
        {
            _nomeEstado = nome;
        }


        private bool _ehEntrada = false;
        private bool _ehSaida = false;

        string _nomeEstado = "";

        /// <summary>
        /// Nome do estado
        /// </summary>
        public string NomeEstado
        {
            get
            {
                return _nomeEstado;
            }
        }


        /// <summary>
        /// Lista de passos para cada tipo de entrada do automato
        /// </summary>
        public List<Passos> LstPassos
        {
            get
            {
                return _lstPassos ?? (_lstPassos = new List<Passos>());
            }

            set
            {
                _lstPassos = value;
            }
        }

        public bool EhEntrada
        {
            get { return _ehEntrada; }
            set { _ehEntrada = value; }
        }

        public bool EhSaida
        {
            get { return _ehSaida; }
            set { _ehSaida = value; }
        }

        List<Passos> _lstPassos = null;

    }
}
