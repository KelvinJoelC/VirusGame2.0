using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  //Nesario para añadir las letras 
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    private Dictionary<int, Player> playerList;


    private PhotonView pv;

    void Start()
    {

        string metodo = "StatPlayer";
        Debug.Log(metodo + "INICIO");
        pv = GetComponent<PhotonView>();


        if (photonView.IsMine)
        {
            GameObject contenedorJugadores = GameObject.FindGameObjectWithTag("Contenedor");
            GameObject prefab_jugador = Resources.Load("prefab_jugador_inGame") as GameObject;
            playerList = PhotonNetwork.CurrentRoom.Players;


            foreach (Player jugador in playerList.Values)
            {
                GameObject listaJugadoresGO = Instantiate(prefab_jugador);
                listaJugadoresGO.transform.SetParent(contenedorJugadores.transform);
                listaJugadoresGO.transform.localScale = Vector3.one;
                listaJugadoresGO.transform.Find("txt_nombreJugador").GetComponent<Text>().text = jugador.NickName;
            }
        }
        Debug.Log(metodo + "FIN");

    }



    #region Métodos privados

    void SetPlayerUI(GameObject instanciaPlayerUI)
    {
        Transform pnl_infJugador = instanciaPlayerUI.transform.Find("pnl_infJugador").GetComponent<Transform>();
        pnl_infJugador.transform.Find("txt_nombreJugadorActual").GetComponent<Text>().text = " " + photonView.Owner.NickName;
        Transform pnl_muestraRonda = instanciaPlayerUI.transform.Find("pnl_muestraRonda").GetComponent<Transform>();
        pnl_muestraRonda.transform.gameObject.SetActive(false);

        Transform pnl_descartar = instanciaPlayerUI.transform.Find("pnl_descartar").GetComponent<Transform>();
        //pnl_descartar.transform.Find("btn_descartarCartas").GetComponent<Button>().onClick.AddListener(() => pv.RPC("robaCarta_click", RpcTarget.AllViaServer));

    }

    #endregion

    // Update is called once per frame
    /*void FixedUpdate () {
        if (photonView.IsMine)
        {
            pv.RPC("robaCarta_click",RpcTarget.AllViaServer);
        }
		
	}*/

    [PunRPC]
    public void robaCarta_click()
    { /*
        GO_baraja = GameObject.FindWithTag("Baraja");
        deckManager = (DeckManager)GO_baraja.GetComponent(typeof(DeckManager));
        manoJugador.Add(deckManager.robaCarta());
        Debug.Log("COMIENZAAAAAAAA");
        foreach (Card carta in manoJugador)
        {
            Debug.Log("Juegador tiene carta: " + carta.tipo + " : " + carta.color);
        }
        */

        Transform jugadorUI = transform.Find("JugadorUI").GetComponent<Transform>();
        Transform canvas_UI = jugadorUI.transform.Find("Canvas").GetComponent<Transform>();
        canvas_UI.transform.Find("txt_nombreJugador").GetComponent<TextMeshProUGUI>().text = "3";
    }
}
