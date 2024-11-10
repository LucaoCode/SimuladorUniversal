# SimuladorUniversal

## Tasks

- [X] Este simulador deverá distribuir aleatoriamente uma determinada quantidade de corpos na tela e calcular a interação gravitacional entre estes corpos.


- [ ] Deverá ser possível gravar a configuração atual do Universo em um arquivo texto, para que posteriormente possa ser recarregado para continuar o processamento.


- [ ] Deverá ser possível gravar a configuração inicial do Universo em um arquivo texto, para que posteriormente possa ser recarregado para continuar o processamento.

- Implementação de uma classe chamada Corpo, contendo os atributos:
- [X] Nome;
- [X] Massa;
- [X] Raio;
- [X] Densidade;
- [X] PosX;
- [X] PosY;
- [X] VelX;
- [X] VelY.

- [X] Implementação de uma classe Universo, que utilize a classe Corpo para realizar os cálculos necessários para a simulação;

Deverão ser tratados as seguintes situações:
- [ ] Cálculo da posição dos corpos em um determinado momento;
- [X] Tratamento das colisões, caso ocorram. O método de colisão deverá sobrecarregar o operador “+”;
- [ ] No tratamento de colisões, deverá ser levado em consideração a quantidade de movimento de cada corpo, para calcular as velocidades nos eixos X e Y após a colisão.

- [ ] Deverá ser utilizada computação paralela para o cálculo das posições e velocidades dos corpos.


- [ ] Interface pra mostrar os corpos com forms 
