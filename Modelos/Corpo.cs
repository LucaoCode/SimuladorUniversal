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
    private double Raio {get; set;}
    private double Densidade {get; set;}
    private double PosX {get; set;}
    private double PosY {get; set;}
    private double VelX {get; set;}
    private double VelY {get; set;}

    public string getNome() => this.Nome;
    public double getMassa() => this.Massa;
    public double getRaio()
    {
        return Math.Pow((3 * Massa) / (4 * Math.PI * Densidade), 1.0 / 3.0);
    }
    public double getDensidade() => this.Densidade;
    public double getPosX() => this.PosX;
    public double getPosY() => this.PosY;
    public double getVelX() => this.VelX;
    public double getVelY() => this.VelY;

    public Corpo(string nome,double massa,double raio,double densidade, double posX,double posY, double velX, double velY)
    {
        this.Nome = nome;
        this.Massa = massa;
        this.Raio = raio;
        this.Densidade = densidade;
        this.PosX = posX;
        this.PosY = posY;
        this.VelX = velX;
        this.VelY = velY;
       
    }

    public Corpo()
    {
        this.Nome = " ";
        this.Massa = 0;
        this.Raio = 0;
        this.Densidade = 1;
        this.PosX = 0;
        this.PosY = 0;
        this.VelX = 0;
        this.VelY = 0;

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
