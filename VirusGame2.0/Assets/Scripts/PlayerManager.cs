using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  //Nesario para añadir las letras 
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    private Dictionary<int, Player> playerList;


    private PhotonView pv;
    private List<Card> mano;
    private GameObject barajaGO;
    private GameObject miManoUI;
    private GameObject prefab_jugador;
    private GameObject prefab_organo;
    public string cartasMano;
    
    void Start()
    {

        string metodo = "StatPlayer";
        Debug.Log(metodo + "INICIO");
        pv = GetComponent<PhotonView>();
        mano = new List<Card>();
        barajaGO = GameObject.FindGameObjectWithTag("Baraja");


         prefab_jugador = Resources.Load("prefab_jugador_inGame") as GameObject;
         prefab_organo = Resources.Load("prefab_jugador_organo") as GameObject;

        if (pv.IsMine)
        {
            
            GameObject contenedorJugadores = GameObject.FindGameObjectWithTag("Contenedor");
            miManoUI = GameObject.FindGameObjectWithTag("miManoCont");
            playerList = PhotonNetwork.CurrentRoom.Players;


            foreach (Player jugador in playerList.Values)
            {
                GameObject listaJugadoresGO = Instantiate(prefab_jugador);
                listaJugadoresGO.transform.SetParent(contenedorJugadores.transform);
                listaJugadoresGO.transform.localScale = Vector3.one;
                listaJugadoresGO.transform.Find("txt_nombreJugador").GetComponent<Text>().text = jugador.NickName;
                listaJugadoresGO.tag="Jug:"+jugador.ActorNumber;

            }
        }

        //GameObject.FindGameObjectWithTag("BotonPrueba").GetComponent<Button>().onClick.AddListener(() => robaCarta_click());  // PRUEBA
        Debug.Log(metodo + "FIN");

    }
     void Update()
    {

        
    }

    #region Métodos RPC
    [PunRPC]
    void RPC_robaCarta()
    {
        Card cartaRecibida = barajaGO.GetComponent<DeckManager>().getCarta();
        mano.Add(cartaRecibida);

        GameObject organoInstancia = Instantiate(prefab_organo);
        if (pv.IsMine) {
            organoInstancia.transform.SetParent(miManoUI.transform);
        }
        
        organoInstancia.transform.localScale = Vector3.one;
        organoInstancia.SetActive(true);
        organoInstancia.GetComponent<Button>().enabled=true;
        organoInstancia.transform.Find("Text").GetComponent<Text>().text = cartaRecibida.tipo +" "+ cartaRecibida.color +" "+cartaRecibida.efecto;
        organoInstancia.transform.GetComponent<Button>().onClick.AddListener(() => seleccionarCarta_click(cartaRecibida));

    }

    #endregion

    #region Métodos privados
    private void robaCarta_click()
    {
        if (pv.IsMine && miManoUI.transform.childCount<=2)
        {
            pv.RPC("RPC_robaCarta", RpcTarget.AllBuffered);
            

        }
    }

    private void seleccionarCarta_click(Card cartaSeleccionada)
    {
        
            if (cartaSeleccionada.tipo== "Organo")
            {
                Debug.Log(cartaSeleccionada.tipo + "FIN");
                if (pv.IsMine)
                {
                    
                }else
                {

                GameObject jugadorRealizador = GameObject.FindGameObjectWithTag("Jug:" + PhotonNetwork.LocalPlayer.ActorNumber);
                
                GameObject organoInstancia = Instantiate(prefab_organo);
                organoInstancia.transform.SetParent(jugadorRealizador.transform.Find("pnl_organos").transform);
                organoInstancia.transform.localScale = Vector3.one;
                organoInstancia.transform.Find("Text").GetComponent<Text>().text = cartaSeleccionada.tipo + " " + cartaSeleccionada.color + " " + cartaSeleccionada.efecto;
                }
            }
            if (cartaSeleccionada.tipo== "Virus")
            {
                Debug.Log(cartaSeleccionada.tipo + "FIN");
            }
            if (cartaSeleccionada.tipo == "Medicina")
            {
                Debug.Log(cartaSeleccionada.tipo + "FIN");
            }
            if (cartaSeleccionada.tipo == "Efecto")
            {
                Debug.Log(cartaSeleccionada.tipo + "FIN");
            }
            
    }



    #endregion

}
