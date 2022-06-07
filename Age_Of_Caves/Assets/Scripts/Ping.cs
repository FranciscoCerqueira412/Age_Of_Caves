using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Ping : MonoBehaviour
{
    public Text pingText;
  

    // Update is called once per frame
    void Update()
    {
        pingText.text = "Ping: " + PhotonNetwork.GetPing();
    }
}
