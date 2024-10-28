using Simulador.Modelos;

/*USANDO A CLASSE PROGRAM PRA FINS DE TESTE SOB O DESENVOLVIMENTO POR ENQUANTO*/

Universo NovoUniverso = new();

// Adicionar os corpos ao universo
int qtdCorpos;
Console.Write("Número de corpos a serem criados: ");

try
{
    string input = Console.ReadLine()!;
    if (int.TryParse(input, out qtdCorpos))
    {
        NovoUniverso.CriarCorposAleatorios(qtdCorpos);
    }
    else
    {
        Console.WriteLine("Digite um número válido");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

// Exibir a quantidade inicial de corpos
Console.WriteLine($"{NovoUniverso.qtdCorpos()}");

double tempoSimulacao = 1.0 ; // Intervalo de tempo em segundos
int numeroIteracoes = 10; // Número de iterações na simulação

for (int i = 0; i < numeroIteracoes; i++)
{
    NovoUniverso.Simular(tempoSimulacao);

    // Limpar a tela para exibir as novas informações
    Console.Clear();

    // Visualizar os corpos atualizados
    NovoUniverso.visualizarCorpos();

    // Aguardar um curto intervalo para melhor visualização
    System.Threading.Thread.Sleep(1000); // Atraso em milissegundos
}

Console.ReadKey();
