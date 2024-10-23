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
    RAIO metros quadrados(m²)*/

    private string Nome {get; set;}
    private double Massa {get; set;}
    private double Raio => CalcularRaio();
    private double Densidade {get; set;}
    private double PosX {get; set;}
    private double PosY {get; set;}
    private double VelX {get; set;}
    private double VelY {get; set;}
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

    public Corpo(string nome,double massa,double densidade, double posX,double posY, double velX, double velY, double forX, double forY)
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

}
