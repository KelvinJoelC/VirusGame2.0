using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class DeckProperty : MonoBehaviourPun//, IPunObservable
{

    private string clase = "DeckManager :";
    

    #region Propiedades
    //Privadas
    private List<Card> mazoPrincipal;
    private List<Card> mazoDescartes;

    [SerializeField]
    public string[] mazoP;
    public string[] mazoD;
    public string ultimaCartaStr;
    public Card proximaCarta;
    

    private PhotonView pv;

    #endregion

    
    
    void Awake() //  inicializar cualquier variable o estado del juego antes de que comience el juego
    {
        //mazoPrincipal = Utiles.generaCartasCompleto();
    }
    private void Start () {
        string metodo = "Start";
        pv = GetComponent<PhotonView>();
        mazoPrincipal = new List<Card>();
        mazoDescartes = new List<Card>();




        if (pv.IsMine)
        {
            mazoPrincipal = Utiles.generaCartasCompleto();
            mazoPrincipal = barajarCartas();
            prepararProximaCarta();


        }
        
        



    }
	
	private void Update () {
        int i = 0;
        
        
        if(mazoPrincipal.Count != 0)
        {
            mazoP = new string[mazoPrincipal.Count];
            foreach (Card carta in mazoPrincipal)
            {
                mazoP[i] = carta.tipo + " : " + carta.color + " : " + carta.efecto;
                i++;
            }
        }
        if (mazoDescartes.Count != 0)
        {
            mazoD = new string[mazoDescartes.Count];
            foreach (Card carta in mazoDescartes)
            {
                mazoD[i] = carta.tipo + " : " + carta.color + " : " + carta.efecto;
                i++;
            }
        }

    }

    #region Actualizar
    public void prepararProximaCarta()
    {
        string[] nextCard;
        if (mazoPrincipal.Count != 0)
        {
            if (mazoPrincipal[0].efecto != null)
            {
                nextCard = new string[3] { mazoPrincipal[0].tipo, mazoPrincipal[0].color, mazoPrincipal[0].efecto };
                ultimaCartaStr = mazoPrincipal[0].tipo + " : " + mazoPrincipal[0].color + " : " + mazoPrincipal[0].efecto;
            }
            else
            {
                nextCard = new string[2] { mazoPrincipal[0].tipo, mazoPrincipal[0].color };
                ultimaCartaStr = mazoPrincipal[0].tipo + " : " + mazoPrincipal[0].color + " : " + mazoPrincipal[0].efecto;
            }
            //proximaCarta = mazoPrincipal[0];
            pv.RPC("RPC_actualizarBaraja", RpcTarget.AllBuffered, nextCard);

        }
        else {
            //BARAJAR
        }
    }
    [PunRPC]
    public void RPC_actualizarBaraja(string[] nextCard)
    {
        Debug.Log("RPC_actualizarBaraja :INICIO");
        Card cartaAux;

        //if (pv.IsMine){
            if (nextCard.Length==3)
            {
                cartaAux = new Card(nextCard[0], nextCard[1], nextCard[2]);
                ultimaCartaStr = cartaAux.tipo + " : " + cartaAux.color + " : " + cartaAux.efecto;
            }
            else
            {
                cartaAux = new Card(nextCard[0], nextCard[1]);
                ultimaCartaStr = cartaAux.tipo + " : " + cartaAux.color;
            }
            proximaCarta = cartaAux;
            
        //}

        Debug.Log("RPC_actualizarBaraja: FIN");
    }
    #endregion


    #region Métodos privados

    private List<Card> barajarCartas()
    {
        //int[] numeros = new int[25];

        List<Card> auxiliarList = new List<Card>();
        int i = 0;
        while (mazoPrincipal.Count != 0)
        {

            if (mazoPrincipal.Count != 1)
            {
                int nRandom = UnityEngine.Random.Range(0, mazoPrincipal.Count - 1);
                auxiliarList.Add(mazoPrincipal[nRandom]);
                mazoPrincipal.Remove(mazoPrincipal[nRandom]);
                i++;
            }
            else
            {
                auxiliarList.Add(mazoPrincipal[0]);
                mazoPrincipal.Remove(mazoPrincipal[0]);
            }
        }

        foreach (Card carta in auxiliarList)
        {
            Debug.Log("BARAJA CARTAS : " + JsonUtility.ToJson(carta, true));
        }
        Debug.Log("BARAJA CARTAS Nº: " + auxiliarList.Count);
        return auxiliarList;
    }
    #endregion


    #region Métodos getCarta
    public void getCarta() {

    }

    [PunRPC]
    public void RPC_getCarta()
    {
        Debug.Log("RPC_getCarta :INICIO");
        GameObject.FindGameObjectWithTag("TextoPrueba").GetComponent<Text>().text = "Mazo " + proximaCarta.tipo + " : " + proximaCarta.color;
        if (pv.IsMine)
        {
            mazoPrincipal.Remove(mazoPrincipal[0]);
        }
        
        prepararProximaCarta();



        Debug.Log("RPC_getCarta: FIN");
    }

    [PunRPC]
    public void RPC_barajarCarta()
    {
        Debug.Log("RPC_barajarCarta :INICIO");
        List<Card> aux = barajarCartas();
        mazoPrincipal = aux;
        Debug.Log("RPC_barajarCarta: FIN");
    }

    


    #endregion


    /*void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(SynCustomData.Serialize(mazoPrincipal));
            //stream.SendNext(mazoDescartes);
        }
        else
        {
            
            mazoPrincipal = SynCustomData.Deserialize(SynCustomData.Serialize(stream.ReceiveNext()));
            //mazoDescartes = (List<Card>)stream.ReceiveNext();
        }
    }*/
}
