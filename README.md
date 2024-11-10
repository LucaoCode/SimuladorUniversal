# SimuladorUniversal

## Tasks

- [X] Este simulador dever� distribuir aleatoriamente uma determinada quantidade de corpos na tela e calcular a intera��o gravitacional entre estes corpos.


- [ ] Dever� ser poss�vel gravar a configura��o atual do Universo em um arquivo texto, para que posteriormente possa ser recarregado para continuar o processamento.


- [ ] Dever� ser poss�vel gravar a configura��o inicial do Universo em um arquivo texto, para que posteriormente possa ser recarregado para continuar o processamento.

- Implementa��o de uma classe chamada Corpo, contendo os atributos:
- [X] Nome;
- [X] Massa;
- [X] Raio;
- [X] Densidade;
- [X] PosX;
- [X] PosY;
- [X] VelX;
- [X] VelY.

- [X] Implementa��o de uma classe Universo, que utilize a classe Corpo para realizar os c�lculos necess�rios para a simula��o;

Dever�o ser tratados as seguintes situa��es:
- [ ] C�lculo da posi��o dos corpos em um determinado momento;
- [X] Tratamento das colis�es, caso ocorram. O m�todo de colis�o dever� sobrecarregar o operador �+�;
- [ ] No tratamento de colis�es, dever� ser levado em considera��o a quantidade de movimento de cada corpo, para calcular as velocidades nos eixos X e Y ap�s a colis�o.

- [ ] Dever� ser utilizada computa��o paralela para o c�lculo das posi��es e velocidades dos corpos.


- [ ] Interface pra mostrar os corpos com forms 
