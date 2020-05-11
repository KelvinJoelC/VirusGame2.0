using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;


namespace Photon.Pun.UtilityScripts
{
    public class DeckManager : MonoBehaviourPunCallbacks
    {
        string clase = "DeckManager :";

        [Header("Mazos")]
        private List<Card> mazoPricipal;
        private List<Card> mazoDescartes;

        [Header("Ultimas cartas")]
        public String cartaTopside;
        public String cartaDescartada;


        private PhotonView pv;  //PhotonView de deck


        void Start()
        {
            string metodo = "Start";
            Debug.Log(metodo + " : INICIO");
            pv = GetComponent<PhotonView>();
            
            //Creo cartas y barajo
            pv.RPC("RPC_inicializar", RpcTarget.AllBuffered); 
            
        }
        void Update()
        {
            cartaTopside = mazoPricipal[0].tipo + " " + mazoPricipal[0].color;
            if(mazoDescartes==null)
            {
                cartaDescartada = null;
            }
            else
            {
                cartaDescartada = mazoDescartes[mazoDescartes.Count - 1].tipo + " " + mazoDescartes[mazoDescartes.Count - 1].color;
            }
            
        }

        #region Métodos privados

        private List<Card> barajarCartas()
        {
            //int[] numeros = new int[25];

            List<Card> auxiliarList = new List<Card>();
            int i = 0;
            while (mazoPricipal.Count != 0)
            {

                if (mazoPricipal.Count != 1)
                {
                    int nRandom = UnityEngine.Random.Range(0, mazoPricipal.Count - 1);
                    auxiliarList.Add(mazoPricipal[nRandom]);
                    mazoPricipal.Remove(mazoPricipal[nRandom]);
                    i++;
                }
                else
                {
                    auxiliarList.Add(mazoPricipal[0]);
                    mazoPricipal.Remove(mazoPricipal[0]);
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

        #region Métodos RPC 

        [PunRPC]
        void RPC_inicializar()
        {
            Debug.Log("RPC_inicializar :INICIO");

            mazoPricipal = Utiles.generaCartasCompleto();
            Debug.Log("Contador cartas -Creadas " + mazoPricipal.Count);
            mazoPricipal = barajarCartas();
            Debug.Log("Contador cartas barajado " + mazoPricipal.Count);
            Debug.Log("RPC_inicializar: FIN");
        }

        [PunRPC]
        void RPC_getCarta()
        {
            Debug.Log("RPC_getCarta :INICIO");

            mazoPricipal.Remove(mazoPricipal[0]);

            Debug.Log("RPC_getCarta: FIN");
        }


        #endregion



        #region Métodos publicos

        public Card getCarta() {

            Card cartaObtenida = mazoPricipal[0];
            pv.RPC("RPC_getCarta", RpcTarget.AllBuffered);
            return cartaObtenida;

        }
        #endregion

    }
}