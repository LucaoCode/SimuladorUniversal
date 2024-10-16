using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Modelos;

internal class Corpo
{
    private string Nome {get; set;}
    private double Massa {get; set;}
    private double Raio {get; set;}
    private double Densidade {get; set;}
    private double PosX {get; set;}
    private double PosY {get; set;}
    private double VelX {get; set;}
    private double VelY {get; set;}

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
}
