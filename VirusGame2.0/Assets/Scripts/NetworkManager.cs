using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks {

    [Header("Estado de conexión")]
    public Text txt_estadoDeConexion;

    [Header("Panel principal ")]
    public GameObject pnl_Introduccion; //Logun_UI_Panel
    public InputField if_nombreJugador;
    public Text txt_errorNombre;

    [Header("Panel para crear, unirse a una sala")]
    public GameObject pnl_miraCreaUneSala; //GameOptions_UI_Panel 

    [Header("Panel Crear una sala")]
    public GameObject pnl_crearSala; //Create Room UI
    public InputField if_nombreSala;
    public Dropdown dropdown_numeroJugadores;

    [Header("Panel dentro de la sala")]
    public GameObject pnl_dentroSala; //InideRoomPnael
    public Text txt_infoSala;
    public GameObject prefab_listaJugadores;
    public GameObject ContenedorJugadores;
    public GameObject btn_empezarPartida;

    [Header("Panel estan todas las Salas")]
    public GameObject pnl_listaSala; //Create RoomList UI

    [Header("Entrar a Sala Random")]
    public GameObject pnl_dentroSalaRandom; //Create JoinRandomRoom UI
    public GameObject salaListaPrefab;
    public GameObject salaListaPadreGO;

    private Dictionary<string, RoomInfo> cacheListaSala;
    private Dictionary<string, GameObject> GameObjectsListaSala;
    private Dictionary<int, GameObject> GameObjectsListaJugadores;


    public bool PantaAux; //Testing


    #region Metodos de unity
    // Use this for initialization
    void Start () {
        ActivaPanels(pnl_Introduccion.name);
        cacheListaSala = new Dictionary<string, RoomInfo>();  //Son como los mapas
        GameObjectsListaSala = new Dictionary<string, GameObject>();

        PhotonNetwork.AutomaticallySyncScene = true; //Para que todos los jugadores comienzen con las mismas caracteristicas

    }
	
	// Update is called once per frame
	void Update () {
        txt_estadoDeConexion.text = "Estado de conexión: " + PhotonNetwork.NetworkClientState;  //Actualizará constatemente el estado de conexión 
	}

    #endregion

    #region Acciones de la INTERFAZ
    /* Boton entrar - Conecta con el servidor*/
    public void btn_Entrar_click() 
    {
        string metodo = "btn_Entrar_click";
        Debug.Log(metodo + "INICIO");
        string nombreJugador = if_nombreJugador.text; //Input - Nombre del Jugador 
        
            if (!string.IsNullOrEmpty(nombreJugador))
            {
                PhotonNetwork.LocalPlayer.NickName = nombreJugador;
                txt_errorNombre.text = "";
                PhotonNetwork.ConnectUsingSettings();
                ActivaPanels(pnl_miraCreaUneSala.name);
            } else
            {
                txt_errorNombre.text = "El nombre no se puede encontrar vacío";
                Debug.Log("El nombre se encuentra vacio");
            }
        Debug.Log(metodo + "FIN");
    }

    public void btn_crearSalaCustom_click()
    {
        string metodo = "btn_crearSalaCustom_click";
        Debug.Log(metodo + "INICIO");

        string NombreSala = if_nombreSala.text;
        string NumeroJugadores = dropdown_numeroJugadores.options[dropdown_numeroJugadores.value].text;
        
        if (string.IsNullOrEmpty(NombreSala))
        {
            NombreSala = "Sala " + Random.Range(1000, 10000);
        }

        RoomOptions OpcioneDeSala = new RoomOptions();
        OpcioneDeSala.MaxPlayers = (byte)int.Parse(NumeroJugadores);
       
        Debug.Log(OpcioneDeSala.MaxPlayers);

        PhotonNetwork.CreateRoom(NombreSala, OpcioneDeSala);//Requerido importar P.RealTime para confirgurar las opciones     -  Aqui se puede añadir expectador
        Debug.Log(metodo + "FIN");
    }

    public void btn_cancelarSalaCustom_click()
    {
        string metodo = "btn_cancelarSalaCustom_click";
        Debug.Log(metodo + "INICIO");
        ActivaPanels(pnl_miraCreaUneSala.name);
        Debug.Log(metodo + "FIN");
    }

    public void btn_accederLobby_click()
    {
        string metodo = "btn_accederLobby_click";
        Debug.Log(metodo + "INICIO");

        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        ActivaPanels(pnl_listaSala.name);
        Debug.Log(metodo + "FIN");
    } //Lobby

    public void btn_salirSala_click()
    {
        string metodo = "btn_salirSala_click";
        Debug.Log(metodo + "INICIO");
        PhotonNetwork.LeaveRoom();
        Debug.Log(metodo + "FIN");
    }

    public void btn_salirLobby_click()
    {
        string metodo = "btn_salirLobby_click";
        Debug.Log(metodo + "INICIO");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        ActivaPanels(pnl_miraCreaUneSala.name);
        Debug.Log(metodo + "FIN");

    }

    public void btn_unirseSalaRandom_click()
    {
        string metodo = "btn_unirseSalaRandom_click";
        Debug.Log(metodo + "INICIO");
        ActivaPanels(pnl_dentroSalaRandom.name);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log(metodo + "FIN");
    }

    public void btn_comenzarPartida_click()
    {
        string metodo = "btn_comenzarPartida_click";
        Debug.Log(metodo + "INICIO");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("InGame");
        }
        Debug.Log(metodo + "FIN");
    }

    #endregion

    #region Photon CallBacks
    
    public override void OnConnected()/*1 - Se conecta a internet*/
    {
        string metodo = "OnConnected";
        Debug.Log(metodo + "INICIO");
        Debug.Log("Se ha conectado a internet.");
        Debug.Log(metodo + "FIN");
    }

    public override void OnConnectedToMaster()//2- Se conecta al servidor
    {
        string metodo = "OnConnectedToMaster";
        Debug.Log(metodo + "INICIO");
        Debug.Log(PhotonNetwork.LocalPlayer.NickName+" se ha conectado a los servidores de Photon.");
        Debug.Log(metodo + "FIN");

    }

    public override void OnCreatedRoom()
    {
        string metodo = "OnCreatedRoom";
        Debug.Log(metodo + "INICIO");
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " está creado");
        Debug.Log(metodo + "FIN");
    }

    public override void OnJoinedRoom()
    {
        string metodo = "OnJoinedRoom";
        Debug.Log(metodo + "INICIO");

        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " a entrado en la sala"+ PhotonNetwork.CurrentRoom.Name);
        ActivaPanels(pnl_dentroSala.name);

        if(PhotonNetwork.LocalPlayer.IsMasterClient)  /*Solo el creador de la sala podrá darle empezar*/  //Falta añadirle solo aparezca cuando esta 4 jugadores;
        {
            btn_empezarPartida.SetActive(true);
        }else
        {
            btn_empezarPartida.SetActive(false);
        }

        txt_infoSala.text = "Nombre de la sala: " + PhotonNetwork.CurrentRoom.Name+"   "+
                            "Jugadores/Max Jugadores: "+ PhotonNetwork.CurrentRoom.PlayerCount+"/"+PhotonNetwork.CurrentRoom.MaxPlayers;

        if(GameObjectsListaJugadores==null)
        {
            GameObjectsListaJugadores = new Dictionary<int, GameObject>();
        }

        foreach(Player jugador in PhotonNetwork.PlayerList)
        {
            GameObject listaJugadoresGO = Instantiate(prefab_listaJugadores);
            listaJugadoresGO.transform.SetParent(ContenedorJugadores.transform);
            listaJugadoresGO.transform.localScale = Vector3.one;

            listaJugadoresGO.transform.Find("NombreJugadorText").GetComponent<Text>().text = jugador.NickName;

            if (jugador.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                listaJugadoresGO.transform.Find("Indicador").gameObject.SetActive(true);
            }
            else { listaJugadoresGO.transform.Find("Indicador").gameObject.SetActive(false); } 

            GameObjectsListaJugadores.Add(jugador.ActorNumber, listaJugadoresGO);

        }
        Debug.Log(metodo + "FIN");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        string metodo = "OnPlayerEnteredRoom";
        Debug.Log(metodo + "INICIO");
        txt_infoSala.text = "Nombre de la sala: " + PhotonNetwork.CurrentRoom.Name + "   " +
                            "Jugadores/Max Jugadores: " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;

        GameObject listaJugadoresGO = Instantiate(prefab_listaJugadores);
        listaJugadoresGO.transform.SetParent(ContenedorJugadores.transform);
        listaJugadoresGO.transform.localScale = Vector3.one;

        listaJugadoresGO.transform.Find("NombreJugadorText").GetComponent<Text>().text = newPlayer.NickName;

        if (newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            listaJugadoresGO.transform.Find("Indicador").gameObject.SetActive(true);
        }
        else { listaJugadoresGO.transform.Find("Indicador").gameObject.SetActive(false); }

        GameObjectsListaJugadores.Add(newPlayer.ActorNumber, listaJugadoresGO);
        Debug.Log(metodo + "FIN");
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        string metodo = "OnPlayerLeftRoom";
        Debug.Log(metodo + "INICIO");
        txt_infoSala.text = "Nombre de la sala: " + PhotonNetwork.CurrentRoom.Name + "   " +
                            "Jugadores/Max Jugadores: " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;

        Destroy(GameObjectsListaJugadores[otherPlayer.ActorNumber].gameObject);
        GameObjectsListaJugadores.Remove(otherPlayer.ActorNumber);

        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            btn_empezarPartida.SetActive(true );
        }
        Debug.Log(metodo + "FIN");
    }

    public override void OnLeftRoom()
    {
        string metodo = "OnLeftRoom";
        Debug.Log(metodo + "INICIO");
        ActivaPanels(pnl_miraCreaUneSala.name);

        foreach(GameObject ListaJugador in GameObjectsListaJugadores.Values)
        {
            Destroy(ListaJugador);
        }
        GameObjectsListaJugadores.Clear();
        GameObjectsListaJugadores = null;
        Debug.Log(metodo + "FIN");
    }

    public override void OnRoomListUpdate(List<RoomInfo> salaLista)
    {
        string metodo = "OnRoomListUpdate";
        Debug.Log(metodo + "INICIO");
        limpiarListaSala(); 

        foreach (RoomInfo sala in  salaLista)
        {
            if(!sala.IsOpen || !sala.IsVisible|| sala.RemovedFromList)
            {
                if(cacheListaSala.ContainsKey(sala.Name))
                {
                    cacheListaSala.Remove(sala.Name);
                }
            }
            else
            {   /*Añadir el cache de  la lista*/
                if (cacheListaSala.ContainsKey(sala.Name))
                {
                    cacheListaSala[sala.Name] = sala;
                }
                else
                {
                    cacheListaSala.Add(sala.Name, sala);
                }
            }
            
        }

        foreach(RoomInfo sala in cacheListaSala.Values)
        {
            GameObject salaListaGameObject = Instantiate(salaListaPrefab);
            salaListaGameObject.transform.SetParent(salaListaPadreGO.transform);
            salaListaGameObject.transform.localScale = Vector3.one;

            salaListaGameObject.transform.Find("NombreSalaText").GetComponent<Text>().text = sala.Name;
            salaListaGameObject.transform.Find("CantidadSalaText").GetComponent<Text>().text = sala.PlayerCount+"/"+sala.MaxPlayers;
            salaListaGameObject.transform.Find("LoginSalaBtn").GetComponent<Button>().onClick.AddListener(() => entrarSalaBtnPulsado(sala.Name));
           

            GameObjectsListaSala.Add(sala.Name,salaListaGameObject);
        }


        Debug.Log(metodo + "FIN");

    }

    public override void OnLeftLobby()
    {
        string metodo = "OnLeftLobby";
        Debug.Log(metodo + "INICIO");
        limpiarListaSala();
        cacheListaSala.Clear();

        Debug.Log(metodo + "FIN");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string metodo = "OnJoinRandomFailed";
        Debug.Log(metodo + "INICIO");
        Debug.Log(message);
        //int nRandom = Random.Range(2, 5);
        int nRandom = 4;
        string nombreSala = "Sala " + Random.Range(1000, 10000);
        RoomOptions opciones = new RoomOptions();
        
        opciones.MaxPlayers = (byte)nRandom; /*Comprobar correctamente*/
        PhotonNetwork.CreateRoom(nombreSala, opciones);
        Debug.Log(metodo + "FIN");
    }

    #endregion

    #region Métodos privados

    void entrarSalaBtnPulsado(string roomName)
    {
        string metodo = "entrarSalaBtnPulsado";
        Debug.Log(metodo + "INICIO");
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log(metodo + "FIN");
    }
    void entrarRandomSalaBtn()
    {
        string metodo = "entrarRandomSalaBtn";
        Debug.Log(metodo + "INICIO");
        ActivaPanels(pnl_dentroSalaRandom.name);

        PhotonNetwork.JoinRandomRoom();
        Debug.Log(metodo + "FIN");
    }

    void limpiarListaSala()
    {
        string metodo = "limpiarListaSala";
        Debug.Log(metodo + "INICIO");
        foreach (var salaListGameObject in GameObjectsListaSala.Values)
        {
            Destroy(salaListGameObject);
        }

        GameObjectsListaSala.Clear();
        Debug.Log(metodo + "FIN");
    }
    
    #endregion

    #region Métodos públicos

    public void ActivaPanels(string panelParaActivar)
    {
        pnl_Introduccion.SetActive(panelParaActivar.Equals(pnl_Introduccion.name));
        pnl_miraCreaUneSala.SetActive(panelParaActivar.Equals(pnl_miraCreaUneSala.name));
        pnl_crearSala.SetActive(panelParaActivar.Equals(pnl_crearSala.name));
        pnl_dentroSala.SetActive(panelParaActivar.Equals(pnl_dentroSala.name));
        pnl_listaSala.SetActive(panelParaActivar.Equals(pnl_listaSala.name));
        pnl_dentroSalaRandom.SetActive(panelParaActivar.Equals(pnl_dentroSalaRandom.name));
   
    }

    #endregion
}
