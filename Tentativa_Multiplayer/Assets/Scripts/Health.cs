using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public int health = 10;
    public Text healthDisplayed;
    public Enemy_Dino enemySWP;

    PhotonView view;

    public GameObject gameOver;


    private void Start()
    {
        view = GetComponent<PhotonView>();
        enemySWP = GetComponent<Enemy_Dino>();
    }

    public void takeDamage()
    {
        view.RPC("takeDamageRPC", RpcTarget.All);
    }

    [PunRPC]
    public void takeDamageRPC()
    {
        health--;
        if (health<=0)
        {
            
            gameOver.SetActive(true);
            //Time.timeScale = 0;

        }
        healthDisplayed.text = health.ToString();
    }
}
