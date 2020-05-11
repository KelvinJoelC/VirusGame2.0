using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string tipo;  //Organo - Virus - Medicina - Efecto
    public string color; //Rojo - Verde - Azul - Amarillo - Multicolor - Negro
    public string efecto;
    public byte Id { get; set; }


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
    public Card() {
        
    }
    

    public static List<Card> Deserialize(List<string[]> deserializar)
    {
        List<Card> aux = new List<Card>();
        foreach (string[] data in deserializar) {
            Card result = new Card(data[0], data[1],data[2]);
            aux.Add(result);
        }
        
        return aux;
    }

    public static List<string[]> Serialize(List<Card> listaCartas)
    {
        List<string[]> aux = new List<string[]>();
        foreach(Card carta in listaCartas)
        {
            string[] list = { carta.tipo, carta.color, carta.efecto };
            aux.Add(list);
        }
        return aux;

    }
}

