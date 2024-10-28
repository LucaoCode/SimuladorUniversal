using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Modelos;

internal class Corpo
{

    /*
    MASSA quilogramas (kg)
    DENSIDADE metro cúbico(kg/m³)
    DISTANCIA EM KM
    RAIO metros quadrados(m²)*/

    private string Nome { get; set; }
    private double Massa { get; set; }
    private double Raio => CalcularRaio();
    private double Densidade { get; set; }
    private double PosX { get; set; }
    private double PosY { get; set; }
    private double VelX { get; set; }
    private double VelY { get; set; }
    private double ForcaX { get; set; }
    private double ForcaY { get; set; }

    public string getNome() => this.Nome;
    public double getMassa() => this.Massa;
    public double CalcularRaio()
    {
        /*Fórmula para calcular o raio quando se tem a Massa e a Densidade conhecida.
        O raio de um corpo esférico é igual à raiz cúbica do quociente entre três vezes a massa 𝑀 do corpo
        e o produto de quatro vezes a constante matemática pi(π) e a densidade média 𝜌 do corpo.*/
        return Math.Pow((3 * Massa) / (4 * Math.PI * Densidade), 1.0 / 3.0); //RAIO metros quadrados(m²)
    }
    public double getRaio() => this.Raio;
    public double getDensidade() => this.Densidade;
    public double getPosX() => this.PosX;
    public double getPosY() => this.PosY;
    public double getVelX() => this.VelX;
    public double getVelY() => this.VelY;
    public double getForcaX() => this.ForcaX;
    public double getForcaY() => this.ForcaY;

    public void setPosX(double posX)
    {
        this.PosX = posX;
    }

    public void setPosY(double posY)
    {
        this.PosY = posY;
    }

    public void setVelX(double velX)
    {
        this.VelX = velX;
    }

    public void setVelY(double velY)
    {
        this.VelY = velY;
    }

    public void setForcaY(double forcaY)
    {
        this.ForcaY = forcaY;
    }

    public void setForcaX(double forcaX)
    {
        this.ForcaX = forcaX;
    }

    public Corpo(string nome, double massa, double densidade, double posX, double posY, double velX, double velY, double forX, double forY)
    {
        this.Nome = nome;
        this.Massa = massa;        
        this.Densidade = densidade;
        this.PosX = posX;
        this.PosY = posY;
        this.VelX = velX;
        this.VelY = velY;
        this.ForcaX = forX;
        this.ForcaY = forY;

    }

    public Corpo()
    {
        this.Nome = " ";
        this.Massa = 0;
        this.Densidade = 1;
        this.PosX = 0;
        this.PosY = 0;
        this.VelX = 0;
        this.VelY = 0;
        this.ForcaX = 0;
        this.ForcaY = 0;

    }

    /*Sobrescrever o metodo equals e GetHashCode pra verificar o corpo ja existe ao ser adicionado, não permitindo dois corpos com o mesmo nome
    o uso do equals e GetHashCode facilita a busca.*/
    public override bool Equals(object? obj)
    {
        if (obj is Corpo corpo)
        {
            return Nome == corpo.Nome;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Nome.GetHashCode();
    }

    // Tratamento das colisões, caso ocorram. O método de colisão deverá sobrecarregar o operador “+”;
    //No tratamento de colisões, deverá ser levado em consideração a quantidade de movimento de cada corpo, para calcular as velocidades nos eixos X e Y após a colisão.
    public static (Corpo, Corpo) operator +(Corpo corpo1, Corpo corpo2)
    {
        if (corpo1 == null || corpo2 == null) throw new ArgumentNullException("Corpos não podem ser nulos.");

        
        double distancia = corpo1.CalcularDistancia(corpo2);
        double somaRaios = corpo1.getRaio() + corpo2.getRaio();

        
        if (distancia <= somaRaios)
        {
            
            double momentoCorpo1 = corpo1.getMassa() * Math.Sqrt(Math.Pow(corpo1.getVelX(), 2) + Math.Pow(corpo1.getVelY(), 2));
            double momentoCorpo2 = corpo2.getMassa() * Math.Sqrt(Math.Pow(corpo2.getVelX(), 2) + Math.Pow(corpo2.getVelY(), 2));

            
            double novaVelX1 = (momentoCorpo1 * corpo1.getVelX() + momentoCorpo2 * corpo2.getVelX()) / (corpo1.getMassa() + corpo2.getMassa());
            double novaVelY1 = (momentoCorpo1 * corpo1.getVelY() + momentoCorpo2 * corpo2.getVelY()) / (corpo1.getMassa() + corpo2.getMassa());

            double novaVelX2 = novaVelX1; 
            double novaVelY2 = novaVelY1; 

            
            corpo1.setVelX(novaVelX1);
            corpo1.setVelY(novaVelY1);
            corpo2.setVelX(novaVelX2);
            corpo2.setVelY(novaVelY2);
        }

        return (corpo1, corpo2);
    }

    public double CalcularDistancia(Corpo corpo)
    {
        double deltaX = this.getPosX() - corpo.getPosX();
        double deltaY = this.getPosY() - corpo.getPosY();
        return Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
    }


    public void AtualizarVelocidade(double deltaVx, double deltaVy)
    {
        VelX += deltaVx;
        VelY += deltaVy;
    }

    public void AtualizarPosicao(double tempo)
    {
        PosX += VelX * tempo;
        PosY += VelY * tempo;
    }

}
