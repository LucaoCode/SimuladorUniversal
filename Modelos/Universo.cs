using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Modelos
{
    internal class Universo : Corpo
    {
        private const double G = 6.674 * 1e-11;
        public  List<Corpo> Corpos { get; set;}

        public Universo()
        {
            Corpos = new List<Corpo>();
        }
        // Metodo para adicionar corpo 
        public void AdicionarCorpo(Corpo corpo)
        {
            Corpos.Add(corpo);
        }

        // Calculo da posição dos Corpos em um determinado momento

        // Tratamento das colisões caso ocorram. O metodo de colisão deverá sobrecarregar o operador "+"
        // No tratamento de colisões, deverá ser levado em consideração a quantidade de movimento de cada corpo,
        // para calcular as velocidades nos eixos X e Y após a colisão.

        // Deverá ser utilizada computação paralela para o cálculo das posições e velocidades dos corpos.

    }
}
