using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Modelos
{
    //Classe responsável por gerenciar todos os corpos;
    internal class Universo
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
            if (corpo == null)
            {
                throw new ArgumentNullException(nameof(corpo), "Não pode ser nulo.");
            }

            if (Corpos.Contains(corpo))
            {
                throw new InvalidOperationException("O corpo já existe nesse universo");
            }

            Corpos.Add(corpo);
            Console.WriteLine("Corpo adicionado testando metodo!! ");
        }

        //Metodo pra criar corpos aleatorios com a quantidade a partir da entrada do usuario:
        public void CriarCorposAleatorios(int quantidade)
        {
            Random random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                string nome = "Corpo " + i;
                double massa = random.NextDouble() * (1e24 - 1e20) + 1e20; // Massa entre 1e20 e 1e24 kg
                double raio = random.NextDouble() * (1000 - 100) + 100; // Raio entre 100 e 1000 km
                double densidade = random.NextDouble() * (5 - 1) + 1; // Densidade entre 1 e 5 g/cm³
                double posX = random.NextDouble();
                double posY = random.NextDouble();
                double velX = random.NextDouble() * 10; // Velocidade X aleatória
                double velY = random.NextDouble() * 10; // Velocidade Y aleatória

                Corpo corpo = new Corpo(nome, massa, raio, densidade, posX, posY, velX, velY);
                AdicionarCorpo(corpo);
            }
        }

        public void visualizarCorpos()
        {
            foreach (Corpo c in Corpos)
            {
                Console.WriteLine("Nome: " + c.getNome());
                Console.WriteLine("Massa: " + c.getMassa() + " kg");
                Console.WriteLine("Raio: " + c.getRaio() + " m");
                Console.WriteLine("Densidade: " + c.getDensidade() + " kg/m³");
                Console.WriteLine("Posição X: " + c.getPosX() + " m");
                Console.WriteLine("Posição Y: " + c.getPosY() + " m");
                Console.WriteLine("Velocidade X: " + c.getVelX() + " m/s");
                Console.WriteLine("Velocidade Y: " + c.getVelY() + " m/s");
            }
        }


        // Calculo da posição dos Corpos em um determinado momento

        // Tratamento das colisões caso ocorram. O metodo de colisão deverá sobrecarregar o operador "+"
        // No tratamento de colisões, deverá ser levado em consideração a quantidade de movimento de cada corpo,
        // para calcular as velocidades nos eixos X e Y após a colisão.

        // Deverá ser utilizada computação paralela para o cálculo das posições e velocidades dos corpos.

    }
}
