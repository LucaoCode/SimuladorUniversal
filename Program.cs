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

NovoUniverso.visualizarCorpos();

Console.WriteLine($"{NovoUniverso.qtdCorpos()}");



NovoUniverso.Simular(5);
Console.ReadKey();
