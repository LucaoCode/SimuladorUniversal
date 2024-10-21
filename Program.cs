using Simulador.Modelos;

/*USANDO A CLASSE PROGRAM PRA FINS DE TESTE SOB O DESENVOLVIMENTO POR ENQUANTO*/

Universo NovoUniverso = new();

// Criar corpos com os parâmetros desejados
// Exemplo de inicialização
Corpo corpo1 = new Corpo("Corpo1", 5.972e24, 6371, 5514, 0, 0, 0); // Terra
Corpo corpo2 = new Corpo("Corpo2", 7.348e22, 1737, 3340, 384400000, 0, 1); // Lua
Corpo corpo3 = new Corpo("Corpo342", 10.0, 1.0, 1.0, 1.0, 20.0, 11.0);
Corpo corpo4 = new Corpo("Corpo224", 10.0, 1.0, 1.0, 1.0, 20.0, 11.0);
Corpo corpo5 = new Corpo("Corpo2342", 10.0, 1.0, 1.0, 1.0, 20.0, 11.0);
Corpo corpo6 = new Corpo("Corpo2234", 10.0, 1.0, 1.0, 1.0, 20.0, 11.0);

// Adicionar os corpos ao universo
NovoUniverso.AdicionarCorpo(corpo1);
NovoUniverso.AdicionarCorpo(corpo2);
NovoUniverso.AdicionarCorpo(corpo3);
NovoUniverso.AdicionarCorpo(corpo4);
NovoUniverso.AdicionarCorpo(corpo5);
NovoUniverso.AdicionarCorpo(corpo6);

// Iniciar a simulação
NovoUniverso.Simular();
Console.ReadKey();