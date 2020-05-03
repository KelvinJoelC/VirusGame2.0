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

        [Header("Mazos")]
        public GameObject mazoPricipal;
        public GameObject mazoDescartes;

        [Header("Manos")]
        public GameObject prefab_cartaJuego;


        private List<Card> listaCartas;
        private PhotonView pv;
        private List<PhotonView> listPV;

        // Use this for initialization
        // RECIBE LLAMADA DEL GESTOR DE TURNOS
        private void Awake()
        {

        }

        void Start()
        {
            pv = GetComponent<PhotonView>();
            listPV = new List<PhotonView>();
            
            //pv.RPC("inicializarRPC", RpcTarget.AllViaServer);
            //Creo cartas y barajo
            listaCartas = Utiles.generaCartasCompleto();
            Debug.Log("Contador cartas -Creadas " + listaCartas.Count);
            listaCartas = barajarCartas();
            Debug.Log("Contador cartas barajado " + listaCartas.Count);
            GameObject[] contenedorJugadores = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject jugador in contenedorJugadores)
            {
                listPV.Add(jugador.GetComponent<PhotonView>());
            }
            
            
        }

        #region Métodos privados

        private List<Card> barajarCartas()
        {
            //int[] numeros = new int[25];

            List<Card> auxiliarList = new List<Card>();
            int i = 0;
            while (listaCartas.Count != 0)
            {

                if (listaCartas.Count != 1)
                {
                    int nRandom = UnityEngine.Random.Range(0, listaCartas.Count - 1);
                    auxiliarList.Add(listaCartas[nRandom]);
                    listaCartas.Remove(listaCartas[nRandom]);
                    i++;
                }
                else
                {
                    auxiliarList.Add(listaCartas[0]);
                    listaCartas.Remove(listaCartas[0]);
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
        void repartirCartasInicio()
        {
            listaCartas = Utiles.generaCartasCompleto();//Ordenadas
            Debug.Log("Contador cartas " + listaCartas.Count);
            listaCartas = barajarCartas();
        }

        #endregion

        /* Update is called once per frame
        void Update()
        {
            pv.RPC("actualizarDatos", RpcTarget.AllViaServer);
        }

        [PunRPC]
        void inicializarRPC()
        {
            listaCartas = Utiles.generaCartasCompleto();//Ordenadas
            Debug.Log("Contador cartas " + listaCartas.Count);
            listaCartas = barajarCartas();
        }


        [PunRPC]
        List<Card> barajarCartas()
        {
            //int[] numeros = new int[25];

            List<Card> auxiliarList = new List<Card>();
            int i = 0;
            while (listaCartas.Count != 0)
            {

                if (listaCartas.Count != 1)
                {
                    int nRandom = 1;//Random.Range(0, listaCartas.Count - 1);
                    auxiliarList.Add(listaCartas[nRandom]);
                    listaCartas.Remove(listaCartas[nRandom]);
                    i++;
                }
                else
                {
                    auxiliarList.Add(listaCartas[0]);
                    listaCartas.Remove(listaCartas[0]);
                }


            }

            foreach (Card carta in auxiliarList)
            {
                Debug.Log("BARAJA CARTAS : " + JsonUtility.ToJson(carta, true));
            }
            Debug.Log("BARAJA CARTAS Nº: " + auxiliarList.Count);
            return auxiliarList;
        }

        public void robaCarta(PhotonView pvSolicitante)
        {
            //Se elimina la carta que hemos escrito por si se roba de nuevo, nos salga la siguiente
            Debug.Log("robaCarta : inicio : " + JsonUtility.ToJson(listaCartas, true));
            Card cartaObtenida = listaCartas[0];
            listaCartas.Remove(listaCartas[0]);
            Debug.Log("robaCarta : FIN : Quedan:" + listaCartas.Count + JsonUtility.ToJson(cartaObtenida, true));
        }
        */
    }
}