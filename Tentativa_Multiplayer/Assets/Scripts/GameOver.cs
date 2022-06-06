using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameOver : MonoBehaviour
{
    public Text scoreDisplay;
    public GameObject restartButton;
    public GameObject waitingText;
    public EnemySpawner enemySWP;


    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        enemySWP = GetComponent<EnemySpawner>();
        scoreDisplay.text = FindObjectOfType<Score>().score.ToString();
        if (PhotonNetwork.IsMasterClient==false)
        {
            restartButton.SetActive(false);
            waitingText.SetActive(true);
        }
    }

    public void OnClickRestart()
    {
        view.RPC("Restart", RpcTarget.All);
        //Time.timeScale = 1;
    }

    [PunRPC]

    void Restart()
    {
        //enemySWP.enabled = true;
        PhotonNetwork.LoadLevel("Game");



    }




}

