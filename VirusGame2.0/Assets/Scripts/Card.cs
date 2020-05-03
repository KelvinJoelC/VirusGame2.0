using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card 
{
    public string tipo;  //Organo - Virus - Medicina - Efecto
    public string color; //Rojo - Verde - Azul - Amarillo - Multicolor - Negro
    public string efecto;
    

    public Card(string tipo, string color){
        this.tipo = tipo;
        this.color = color;
        this.efecto = null;
    }
    public Card(string tipo, string color, string efecto)
    {
        this.tipo = tipo;
        this.color = color;
        this.efecto = efecto;
    }

}

