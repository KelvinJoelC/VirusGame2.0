using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour {

    private string clase = "PlayerProperty :";


    #region Propiedades
    //Privadas



    //Públicas
    public Card[] mazoPricipal;
    public Card[] mazoDescartes;
    public bool esMiTurno;  //Puede ser privada
    #endregion

    void Awake() //  inicializar cualquier variable o estado del juego antes de que comience el juego
    {

    }
    private void Start()
    {

    }

    private void Update()
    {

    }
}
