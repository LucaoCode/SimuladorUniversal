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
                Console.WriteLine($"Velocidade X: {c.getVelX()} m/s");
                Console.WriteLine($"Velocidade Y: {c.getVelY()} m/s");

            }
        }


        public void Simular(double tempo)
        {
            double[] somaFx = new double[Corpos.Count];
            double[] somaFy = new double[Corpos.Count];
            object lockObject = new object(); // Objeto para sincronização

            // Calcular forças em paralelo
            Parallel.For(0, Corpos.Count, index1 =>
            {
                var corpo1 = Corpos[index1];
                for (int index2 = 0; index2 < Corpos.Count; index2++)
                {
                    var corpo2 = Corpos[index2];
                    if (corpo1 != corpo2)
                    {
                        var (Fx, Fy) = CalcularComponentesDaForca(corpo1, corpo2);

                        // Usar lock ao atualizar as somas das forças
                        lock (lockObject)
                        {
                            somaFx[index1] += Fx;
                            somaFy[index1] += Fy;
                        }
                    }
                }
            });

            // Atualizar posições e velocidades em paralelo
            Parallel.For(0, Corpos.Count, index =>
            {
                var corpo = Corpos[index];

                // Gravar a velocidade inicial
                var velocidadeInicialX = corpo.getVelX();
                var velocidadeInicialY = corpo.getVelY();

                double aceleracaoX = somaFx[index] / corpo.getMassa();
                double aceleracaoY = somaFy[index] / corpo.getMassa();

                // Atualizar a velocidade e posição do corpo
                corpo.AtualizarVelocidade(aceleracaoX * tempo, aceleracaoY * tempo);
                corpo.AtualizarPosicao(tempo);

                // Gravar a velocidade final
                var velocidadeFinalX = corpo.getVelX();
                var velocidadeFinalY = corpo.getVelY();

                // Exibir ou registrar a velocidade inicial e final
                Console.WriteLine($"Corpo {corpo.getNome()}: Velocidade Inicial = ({velocidadeInicialX}, {velocidadeInicialY}), Velocidade Final = ({velocidadeFinalX}, {velocidadeFinalY})");
            });
        }



        private (double Fx, double Fy) CalcularComponentesDaForca(Corpo corpo1, Corpo corpo2)
        {

            double dx = corpo2.getPosX() - corpo1.getPosX(); // Diferença de posição no eixo X
            double dy = corpo2.getPosY() - corpo1.getPosY(); // Diferença de posição no eixo Y
            double distancia = Math.Sqrt(dx * dx + dy * dy); // Distância entre os corpos

            // Evitar divisão por zero
            if (distancia == 0)
            {
                return (0, 0);
            }

            double força = G * (corpo1.getMassa() * corpo2.getMassa()) / (distancia * distancia); // Força gravitacional

            // Componentes da força
            double Fx = força * (dx / distancia);
            double Fy = força * (dy / distancia);


            return (Fx, Fy);
        }

        private void ExibirVelocidadeEPosicoes()
        {
            Console.Clear();
            visualizarCorpos();
            Console.WriteLine();
        }


        public int qtdCorpos() => Corpos.Count;

        //Calcular a distância entre dois objetos do tipo Corpo
        public double CalcularDistancia(Corpo corpo1, Corpo corpo2)
        {
            double deltaX = corpo1.getPosX() - corpo2.getPosX(); // Diferença na posição X
            double deltaY = corpo1.getPosY() - corpo2.getPosY(); // Diferença na posição y

            double distanciaSuperficie = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));

           
            double raio1 = corpo1.getRaio();
            double raio2 = corpo2.getRaio();

            double distanciaEfetiva = distanciaSuperficie - (raio1 + raio2);
            
            return Math.Max(distanciaEfetiva, 0); // Garantir que a distância não seja negativa
        }

        // Metodo para escrever os dados iniciais e gravar 
        public void GravacaoInicial(List<Corpo> corpos)
        {
            try
            {
                string now = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string caminhoArquivo = $"../Gravacao/GravacaoInicial/GravacaoInicial_{now}.txt";

                // Verifica se o diretório existe, se não, cria
                string diretorio = Path.GetDirectoryName(caminhoArquivo);
                if (!Directory.Exists(diretorio))
                {
                    Directory.CreateDirectory(diretorio);
                }

                using (StreamWriter writer = new StreamWriter(caminhoArquivo))
                {
                    foreach (var corpo in corpos)
                    {
                        writer.WriteLine($"Nome: {corpo.getNome()}, Massa: {corpo.getMassa():F2}, Densidade: {corpo.getDensidade():F2}, " +
                                         $"Raio: {corpo.getRaio():F2}, PosX: {corpo.getPosX():F2}, PosY: {corpo.getPosY():F2}, " +
                                         $"VelX: {corpo.getVelX():F2}, VelY: {corpo.getVelY():F2}, ForcaX: {corpo.getForcaX():F2}, ForcaY: {corpo.getForcaY():F2}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao gravar arquivo: {ex.Message}");
            }
        }

        //Tratamento de colisão



        // Calculo da posição dos Corpos em um determinado momento

        // Tratamento das colisões caso ocorram. O metodo de colisão deverá sobrecarregar o operador "+"
        // No tratamento de colisões, deverá ser levado em consideração a quantidade de movimento de cada corpo,
        // para calcular as velocidades nos eixos X e Y após a colisão.

        // Deverá ser utilizada computação paralela para o cálculo das posições e velocidades dos corpos.

    }
}
