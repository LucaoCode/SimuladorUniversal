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

/*NovoUniverso.visualizarCorpos();
*/
Console.WriteLine($"{NovoUniverso.qtdCorpos()}");
Console.ReadKey();

/*NovoUniverso.AdicionarCorpo(new Corpo("Corpo 1", 1.5e24, 5500, 1.0e7, 0.0, 0.0, 1.0e3, 0, 0));
NovoUniverso.AdicionarCorpo(new Corpo("Corpo 2", 1.6e24, 5600, 1.2e7, 0.5e7, -1.5e2, 1.1e3, 0, 0));
NovoUniverso.AdicionarCorpo(new Corpo("Corpo 3", 1.45e24, 5400, 1.0e7, 1.5e7, -1.0e2, -1.0e3, 0, 0));
NovoUniverso.AdicionarCorpo(new Corpo("Corpo 4", 1.48e24, 5500, -1.2e7, -1.2e7, 1.5e2, -1.2e3, 0, 0));
NovoUniverso.AdicionarCorpo(new Corpo("Corpo 6", 1.47e24, 5450, 1.1e7, -0.8e7, -2.0e2, 0.8e3, 0, 0));
NovoUniverso.AdicionarCorpo(new Corpo("Corpo 7", 1.49e24, 5550, 1.5e7, 1.1e7, -1.0e2, -1.5e3, 0, 0));
NovoUniverso.AdicionarCorpo(new Corpo("Corpo 9", 1.51e24, 5600, -1.1e7, 1.2e7, 1.2e2, 1.1e3, 0, 0));
NovoUniverso.AdicionarCorpo(new Corpo("Corpo 10", 1.55e24, 5700, 1.3e7, -1.0e7, -1.8e2, 1.3e3, 0, 0));
*/

/*NovoUniverso.visualizarCorpos();*/

// Iniciar a simulação
//NovoUniverso.Simular();
