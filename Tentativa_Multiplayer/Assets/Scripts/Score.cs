using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Score : MonoBehaviour
{
    public int score = 0;
    public Text scoreDisplayed;

    PhotonView view;


    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void gainScore()
    {
        view.RPC("gainScoreRPC", RpcTarget.All);
    }

    [PunRPC]
    public void gainScoreRPC()
    {
        score++;
        scoreDisplayed.text = score.ToString();
    }
}
