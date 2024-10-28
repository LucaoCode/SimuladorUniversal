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
        public List<Corpo> Corpos { get; set; }

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
        }

        //Metodo pra criar corpos aleatorios com a quantidade a partir da entrada do usuario:
        public void CriarCorposAleatorios(int quantidade)
        {
            Random random = new Random();
            double massaMinima = 3.30e23;  
            double massaMaxima = 1.90e27;

            double densidadeMinima = 0.687;
            double densidadeMaxima = 5.52;

            double distanciaMinima = 1 * 149.6e6; // 1  Unidade Astronômica em km
            double distanciaMaxima = 10 * 149.6e6; // 10  Unidade Astronômica em km

            double velocidadeMaxima = 50.0; //50 unidades por segundo

            double randomValue = random.NextDouble();

            //Gerar Massa aleatória dentre os limites defindos 
            double GerarMassaAleatoriaLogaritmica(double massaMinima, double massaMaxima)
            {

                double logMin = Math.Log10(massaMinima);
                double logMax = Math.Log10(massaMaxima);


                double randomLogValue = logMin + random.NextDouble() * (logMax - logMin);


                return Math.Pow(10, randomLogValue);
            }

            //Gerar Densidade aleatoria dentro dos limites definidos
            double GerarDensidadeAleatoria(double densidadeMinima, double densidadeMaxima)
            {
                // Gera um número aleatório entre os limites das densidades
                return densidadeMinima + random.NextDouble() * (densidadeMaxima - densidadeMinima);
            }


            //Gerar Posições aleatorias dentro dos limites definidos
            (double x, double y) GerarPosicaoAleatoria(double distanciaMinima, double distanciaMaxima)
            {
                Random random = new Random();
                
                double distancia = random.NextDouble() * (distanciaMaxima - distanciaMinima) + distanciaMinima;
                
                double angulo = random.NextDouble() * 2 * Math.PI; // Ângulo entre 0 e 2*pi

                // Calcular as coordenadas x e y
                double x = distancia * Math.Cos(angulo);
                double y = distancia * Math.Sin(angulo);

                return (x, y);
            }

            for (int i = 0; i < quantidade; i++)
            {
                string nome = "Corpo " + i;
                double massa = GerarMassaAleatoriaLogaritmica(massaMinima, massaMaxima); // Massa entre 1,90 × 10²⁷ kg  e 3,30 × 10²³ kg                
                double densidade = GerarDensidadeAleatoria(densidadeMinima, densidadeMaxima); // Densidade entre 0.687 e 5.52 g/cm³
                (double x, double y) = GerarPosicaoAleatoria(distanciaMinima, distanciaMaxima);
                double posX = x/1000;
                double posY = y/1000;
                double velX = random.NextDouble() * velocidadeMaxima;
                double velY = random.NextDouble() * velocidadeMaxima; 

                Corpo corpo = new Corpo(nome, massa, densidade, posX, posY, velX, velY, 0, 0);
                
                AdicionarCorpo(corpo);
            }
        }

        public void visualizarCorpos()
        {
            // Método para formatar a massa
            string FormatarMassa(double massa)
            {
                string massaFormatada = massa.ToString("0.0000E+0", System.Globalization.CultureInfo.InvariantCulture);
                massaFormatada = massaFormatada.Replace("E", " × 10^");

                massaFormatada = massaFormatada.Replace("× 10^+", "× 10^");

                return massaFormatada;
            }


            foreach (Corpo c in Corpos)
            {
                Console.WriteLine($"Nome: {c.getNome()}");
                Console.WriteLine($"Massa: {FormatarMassa(c.getMassa())} kg");
                Console.WriteLine($"Raio: {c.getRaio():F3} m");
                Console.WriteLine($"Densidade: {c.getDensidade():F3} kg/m³");
                Console.WriteLine($"Posição X: {c.getPosX():F3} km"); 
                Console.WriteLine($"Posição Y: {c.getPosY():F3} km"); 
                Console.WriteLine($"Velocidade X: {c.getVelX():F3} m/s");
                Console.WriteLine($"Velocidade Y: {c.getVelY():F3} m/s");

            }
        }


        //Calcular a força entre dois corpos num método separado
        void calcularForcaG(Corpo corpo1, Corpo corpo2, double deltaTempo)
        {
            double hipotenusa = CalcularDistancia(corpo1, corpo2);

            if (hipotenusa == 0) return; // LOGICA DA COLISAO AQUI

            // Calcular a força gravitacional entre os dois corpos
            double forca = (corpo1.getMassa() * corpo2.getMassa()) / (Math.Pow(hipotenusa, 2)) * G;

            // Calcular as componentes da força em X e Y
            double forcaX = forca * (corpo2.getPosX() - corpo1.getPosX()) / hipotenusa;
            double forcaY = forca * (corpo2.getPosY() - corpo1.getPosY()) / hipotenusa;

            // Atualizar a força resultante em cada corpo
            corpo1.setForcaX(corpo1.getForcaX() + forcaX);
            corpo1.setForcaY(corpo1.getForcaY() + forcaY);

            corpo2.setForcaX(corpo2.getForcaX() - forcaX);
            corpo2.setForcaY(corpo2.getForcaY() - forcaY);
        }

        //Usando programação paralela para calcular a distancia e velocidade entre os corpos:
        public void AtualizarEstado(double deltaTempo)
        {
            // Resetar as forças antes do cálculo
            foreach (var corpo in Corpos)
            {
                corpo.setForcaX(0);
                corpo.setForcaY(0);
            }

            // Calculo das forças gravitacionais entre os corpos 
            Parallel.For(0, Corpos.Count, i =>
            {
                for (int j = i + 1; j < Corpos.Count; j++)
                {
                    Corpo corpo1 = Corpos[i];
                    Corpo corpo2 = Corpos[j];

                    calcularForcaG(corpo1, corpo2, deltaTempo);

                    // Verifica e trata colisões
                    var (novoCorpo1, novoCorpo2) = corpo1 + corpo2; // Isso chamará o operador sobrecarregado
                }
            });

            // Atualizar as velocidades com base nas forças
            Parallel.For(0, Corpos.Count, i =>
            {
                var corpo = Corpos[i];
                double aceleracaoX = corpo.getForcaX() / corpo.getMassa();
                double aceleracaoY = corpo.getForcaY() / corpo.getMassa();

                corpo.setVelX(corpo.getVelX() + aceleracaoX * deltaTempo);
                corpo.setVelY(corpo.getVelY() + aceleracaoY * deltaTempo);

                // Atualizar a posição com base na velocidade
                corpo.setPosX(corpo.getPosX() + corpo.getVelX() * deltaTempo);
                corpo.setPosY(corpo.getPosY() + corpo.getVelY() * deltaTempo);
            });
        }


        public void Simular()
        {
            double deltaTempo = 1.0; // Intervalo de tempo fixo em segundos
            double tempoTotal = 0.0; // Tempo total da simulação
            double duracaoSimulacao = 50.0; // Duração total da simulação em segundos (por exemplo, 10 segundos)

            while (tempoTotal < duracaoSimulacao)
            {
                AtualizarEstado(deltaTempo); // Atualiza as forças e posições dos corpos
                ExibirVelocidadeEPosicoes(); // Exibe as posições e velocidades na tela

                Thread.Sleep(1000); // Aguarda 1 segundo
                tempoTotal += deltaTempo; // Atualiza o tempo total
            }
        }

        private void ExibirVelocidadeEPosicoes()
        {
            Console.Clear(); // Limpa a tela

            Console.WriteLine("Velocidade e Posições dos corpos:");
            foreach (var corpo in Corpos)
            {
                // Exibe o nome do corpo e suas velocidades em X e Y
                Console.WriteLine($"{corpo.getNome()}: VelX = {corpo.getVelX():F2}, VelY = {corpo.getVelY():F2}, ForcaX = {corpo.getForcaX():F2}, ForcaY = {corpo.getForcaY():F2}");
                Console.WriteLine($"PosX = {corpo.getPosX():F2}, PosY = {corpo.getPosY():F2}");
            }
            Console.WriteLine();
        }


        public int qtdCorpos() => Corpos.Count;

        //Calcular a distância entre dois objetos do tipo Corpo usando a fórmula da distância euclidiana. 
        public double CalcularDistancia(Corpo corpo1, Corpo corpo2)
        {
            double deltaX = corpo1.getPosX() - corpo2.getPosX(); // Diferença na posição X
            double deltaY = corpo1.getPosY() - corpo2.getPosY(); // Diferença na posição y

            return Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
        }

        //Tratamento de colisão



        // Calculo da posição dos Corpos em um determinado momento

        // Tratamento das colisões caso ocorram. O metodo de colisão deverá sobrecarregar o operador "+"
        // No tratamento de colisões, deverá ser levado em consideração a quantidade de movimento de cada corpo,
        // para calcular as velocidades nos eixos X e Y após a colisão.

        // Deverá ser utilizada computação paralela para o cálculo das posições e velocidades dos corpos.

    }
}
