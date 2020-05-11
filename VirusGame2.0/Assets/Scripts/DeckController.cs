using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;

public class DeckController : MonoBehaviourPunCallbacks
{

    //Scripts
    private DeckProperty propiedad;
    private PhotonView pv;
    

	void Start () {
        propiedad = this.GetComponent<DeckProperty>();
        pv = this.GetComponent<PhotonView>();
        /*if (!pv.IsMine)
        {
            Destroy(this);
        }*/
        
        GameObject.FindGameObjectWithTag("BotonPrueba").GetComponent<Button>().onClick.AddListener(() => pv.RPC("RPC_getCarta", RpcTarget.AllBuffered));
        


    }   
	
	// Update is called once per frame
	void Update () {
		
	}

    #region Métodos publicos
    /*public void getCarta()
    {

        
        pv.RPC("RPC_getCarta", RpcTarget.AllBuffered);

    }*/
    #endregion
}
