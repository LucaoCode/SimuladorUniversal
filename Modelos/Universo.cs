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

                Corpo corpo = new Corpo(nome, massa, densidade, posX, posY, velX, velY);
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

        //Usando programação paralela para calcular a força, distancia e velocidade entre os corpos:
        public void AtualizarEstado(double deltaTempo)
        {
            // Calculo das forças gravitacionais entre os corpos
            Parallel.For(0, Corpos.Count, i =>
            {
                for (int j = i + 1; j < Corpos.Count; j++)
                {
                    Corpo corpo1 = Corpos[i];
                    Corpo corpo2 = Corpos[j];
                    
                    double distancia = CalcularDistancia(corpo1, corpo2);

                    // Evitar divisão por zero (se for 0 quer dizer que colidiu, ainda n fiz essa parte
                    if (distancia == 0) continue;

                    // Calcular a força gravitacional entre os dois corpos F = G * (m1 * m2) / r^2 e depois calcular as componentes em X e Y 
                    double forca = (corpo1.getMassa() * corpo2.getMassa()) / (distancia * distancia) * G;
                    double forcaX = forca * (corpo2.getPosX() - corpo1.getPosX()) / distancia;
                    double forcaY = forca * (corpo2.getPosY() - corpo1.getPosY()) / distancia;

                    // Atualizar a velocidade dos corpos usando Lock para prevenir que tentem alterar o msm lugar ao mesmo tempo
                    lock (corpo1)
                    {
                        corpo1.setVelX(corpo1.getVelX() + (forcaX / corpo1.getMassa()) * deltaTempo);
                        corpo1.setVelY(corpo1.getVelY() + (forcaY / corpo1.getMassa()) * deltaTempo);
                    }
                    lock (corpo2)
                    {
                        corpo2.setVelX(corpo2.getVelX() - (forcaX / corpo2.getMassa()) * deltaTempo);
                        corpo2.setVelY(corpo2.getVelY() - (forcaY / corpo2.getMassa()) * deltaTempo);
                    }
                }
            });

            // Atualizar a posição dos corpos usando a velocidade
            Parallel.ForEach(Corpos, corpo =>
            {
                corpo.setPosX(corpo.getPosX() + corpo.getVelX() * deltaTempo);
                corpo.setPosY(corpo.getPosY() + corpo.getVelY() * deltaTempo);
            });
        }

        public void Simular()
        {
            double deltaTime = 1.0; // Intervalo de tempo fixo em segundos

            while (true) 
            {
                AtualizarEstado(deltaTime); // Atualiza forças e posições
                ExibirVelocidadeEPosicoes(); // Exibir as posições na tela
                Thread.Sleep(1000); // Aguardar 1 segundo
            }
        }

        private void ExibirVelocidadeEPosicoes()
        {
            Console.Clear(); // Limpa a tela

            Console.WriteLine("Velocidades dos corpos:");
            foreach (var corpo in Corpos)
            {
                // Exibe o nome do corpo e suas velocidades em X e Y
                Console.WriteLine($"{corpo.getNome()}: VelX = {corpo.getVelX():F2}, VelY = {corpo.getVelY():F2}");
                Console.WriteLine($"PosX = {corpo.getPosX():F2}, PosY = {corpo.getPosY():F2}");
            }
            Console.WriteLine(); // Para mover para a próxima linha
        }

        //Retornar quantidade de corpos no universo
        public int qtdCorpos() => Corpos.Count;

        //Calcular a distância entre dois objetos do tipo Corpo usando a fórmula da distância euclidiana. 
        public double CalcularDistancia(Corpo corpo1, Corpo corpo2)
        {
            double deltaX = corpo1.getPosX() - corpo2.getPosX(); // Diferença na posição X
            double deltaY = corpo1.getPosY() - corpo2.getPosY(); // Diferença na posição y

            return Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
        }



        // Calculo da posição dos Corpos em um determinado momento

        // Tratamento das colisões caso ocorram. O metodo de colisão deverá sobrecarregar o operador "+"
        // No tratamento de colisões, deverá ser levado em consideração a quantidade de movimento de cada corpo,
        // para calcular as velocidades nos eixos X e Y após a colisão.

        // Deverá ser utilizada computação paralela para o cálculo das posições e velocidades dos corpos.

    }
}
