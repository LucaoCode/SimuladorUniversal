using Simulador.Modelos;

/*USANDO A CLASSE PROGRAM PRA FINS DE TESTE SOB O DESENVOLVIMENTO POR ENQUANTO*/

Universo NovoUniverso = new();

// Exemplo de inicialização
Corpo Mercurio = new Corpo("Mercúrio", 3.3011e23, 5.427, 4.79e10, 1.2e10, 45.36e3, -10.0e3);
Corpo Venus = new Corpo("Vênus", 4.8675e24, 5.243, 1.15e11, 2.0e10, 32.02e3, -8.0e3);
Corpo Terra = new Corpo("Terra", 5.972e24, 5.514, 1.496e11, 3.1e10, 30.78e3, -5.0e3);
Corpo Marte = new Corpo("Marte", 6.4171e23, 3.933, 2.35e11, 4.5e10, 22.07e3, -6.0e3);
Corpo Jupiter = new Corpo("Júpiter", 1.8982e27, 1.326, 7.9e11, 6.8e10, 12.07e3, -2.0e3);
Corpo Saturno = new Corpo("Saturno", 5.6834e26, 0.687, 1.4e12, 8.3e10, 8.69e3, -1.0e3);
Corpo Urano = new Corpo("Urano", 8.6810e25, 1.27, 2.8e12, 1.0e11, 7.81e3, -0.5e3);
Corpo Netuno = new Corpo("Netuno", 1.02413e26, 1.638, 4.5e12, 1.2e11, 5.43e3, -0.3e3);
Corpo corpo9 = new Corpo("CorpoFicticio", 10.0, 1.0, 2.5, 3.0, 100.0, 150.0);


// Adicionar os corpos ao universo
NovoUniverso.AdicionarCorpo(Mercurio);
NovoUniverso.AdicionarCorpo(Venus);
NovoUniverso.AdicionarCorpo(Terra);
NovoUniverso.AdicionarCorpo(Marte);
NovoUniverso.AdicionarCorpo(Jupiter);
NovoUniverso.AdicionarCorpo(Saturno);
NovoUniverso.AdicionarCorpo(Urano);
NovoUniverso.AdicionarCorpo(Netuno);
NovoUniverso.AdicionarCorpo(corpo9);

/*NovoUniverso.visualizarCorpos();*/

// Iniciar a simulação
NovoUniverso.Simular();
