using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks {

    //Scripts
    private PlayerProperty propiedad;
    private PhotonView pv;


    void Start()
    {
        propiedad = this.GetComponent<PlayerProperty>();
        pv = this.GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            Destroy(this);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (propiedad.esMiTurno)
        {

        }

    }
}
