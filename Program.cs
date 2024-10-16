using Simulador.Modelos;

/*USANDO A CLASSE PROGRAM PRA FINS DE TESTE SOB O DESENVOLVIMENTO POR ENQUANTO*/

Universo NovoUniverso = new();

Console.WriteLine("Quantidade de corpos: ");
NovoUniverso.CriarCorposAleatorios(int.Parse(Console.ReadLine()));
NovoUniverso.visualizarCorpos();


Console.ReadKey();