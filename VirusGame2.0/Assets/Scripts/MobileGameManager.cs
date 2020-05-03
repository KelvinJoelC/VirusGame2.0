using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;  //Nesario para añadir las letras 
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class MobileGameManager : MonoBehaviour {
    
    [Header("Gestor de turnos")]
    //public PunTurnManager turnManager;
    private Dictionary<int, Player> playerList;
    private Dictionary<int, PhotonView> PVList;

    [Header("Gestor del mazo")]
    private List<Card> listaCartas;


    [Header("Instancia visual del jugador")]
    public GameObject prefab_jugador;

    void Start () {
        Debug.Log("Start");
        playerList = PhotonNetwork.CurrentRoom.Players;
        
        //turnManager = new PunTurnManager();




        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (prefab_jugador != null )
            {
                PhotonNetwork.CurrentRoom.SetTurn(0, false);
                GameObject instanciaPlayerUI = PhotonNetwork.Instantiate(prefab_jugador.name, Vector3.one, Quaternion.Euler(0, 0, 0));
                GameObject instanciaDeckManager = PhotonNetwork.Instantiate(prefab_jugador.name, Vector3.one, Quaternion.Euler(0, 0, 0));
                
                //Se reparten las cartas
                PhotonView pvPlayer = instanciaPlayerUI.GetComponent<PhotonView>();
                //pvPlayer.RPC();


                //Se decide quien comienza
                //Turno del jugador
                //ActiondelJugador(Ataccar, defender, Coloca organo, Utilizar carta, desechar cartas.)
                //Tiene 4 organos  sanos?
                //Coge carta
                //Siguiente turno
                //Tiene 4 organos  sanos?


                Debug.Log("Turno: "+ PhotonNetwork.CurrentRoom.GetTurn());
                //instanciaPlayerUI.GetComponent<PlayerManager>().prefab_jugador_inGame = prefab_jugador_inGame;
                //instanciaPlayerUI.GetComponent<PlayerManager>().contenedorJugadores = contenedorJugadores;

            } else
            {
                Debug.Log("Colocar el prefab para jugador");
            }
        }
		
	}

    
}
