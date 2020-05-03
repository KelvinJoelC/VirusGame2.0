using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utiles : MonoBehaviour {

	public static List<Card> generaCartas(string tipo, string color, int cantidad)
    {
        List<Card> listaCartas = new List<Card>();
        for(int i=0;i<cantidad; i++)
        {
            Card carta = new Card(tipo, color);
            listaCartas.Add(carta);
        }
        return listaCartas;
    }

    public static List<Card> generaCartas(string tipo, string color)
    {
        List<Card> listaCartas = new List<Card>();
        Card carta = new Card(tipo, color);
        listaCartas.Add(carta);
        return listaCartas;
    }

    public static List<Card> generaCartasCompleto()
    {
        List<Card> listaCartas = new List<Card>();
        #region Organos
        foreach (Card carta in generaCartas("Organo", "Rojo", 5))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Organo", "Verde", 5))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Organo", "Azul", 5))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Organo", "Amarillo", 5))
        {
            listaCartas.Add(carta);
        }
        listaCartas.Add(new Card("Organo", "Multicolor"));
        #endregion

        #region Medicinas
        foreach (Card carta in generaCartas("Medicina", "Rojo", 4))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Medicina", "Verde", 4))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Medicina", "Azul", 4))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Medicina", "Amarillo", 4))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Medicina", "Multicolor", 4))
        {
            listaCartas.Add(carta);
        }
        #endregion

        #region Virus
        foreach (Card carta in generaCartas("Virus", "Rojo", 4))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Virus", "Verde", 4))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Virus", "Azul", 4))
        {
            listaCartas.Add(carta);
        }
        foreach (Card carta in generaCartas("Virus", "Amarillo", 4))
        {
            listaCartas.Add(carta);
        }
        listaCartas.Add(new Card("Virus", "Multicolor"));
        #endregion

        #region Efecto
        //POR EL MOMENTO NO
        #endregion

        return listaCartas;
    }
}
