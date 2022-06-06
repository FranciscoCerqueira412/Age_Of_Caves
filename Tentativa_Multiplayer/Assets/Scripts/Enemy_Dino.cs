using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy_Dino : MonoBehaviour
{
    PlayerController[] players;
    PlayerController nearestPlayer;
    public float speedDino;
    public float spd;
    public float enemyHP;
    Shake shake;

    PhotonView view;
    Score score;

    public GameObject deathFXEnemy;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        players = FindObjectsOfType<PlayerController>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        view = GetComponent<PhotonView>();
        score = FindObjectOfType<Score>();
        spd = speedDino;
        shake= FindObjectOfType<Shake>();
    }

    private void Update()
    {
        float distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);

 

        if (distanceOne < distanceTwo)
            nearestPlayer = players[0];
        else
            nearestPlayer = players[1];

        if (nearestPlayer!=null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, spd * Time.deltaTime);

        }
        this.spriteRenderer.flipX = nearestPlayer.transform.position.x > this.transform.position.x;

        if (score.score >= 0 && score.score < 10)
        {
            spd = speedDino;

        }
        else if (score.score >= 10 && score.score < 20)
        {
            spd = speedDino + 0.2f;

        }
        else if (score.score >= 20 && score.score < 40)
        {
            spd = speedDino + 0.3f;

        }
        else if (score.score >= 40 && score.score < 60)
        {
            spd = speedDino + 0.5f;

        }
        else if (score.score >= 60)
        {
            spd = speedDino + 0.7f;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.tag == "Raio")
            {
                enemyHP--;
                view.RPC("SpawnParticle", RpcTarget.All);
                view.RPC("shakeScreen", RpcTarget.All);
                if (enemyHP<1)
                {
                    score.gainScore();

                    PhotonNetwork.Destroy(this.gameObject);
                }


            }
        }

    }

    [PunRPC]
    void shakeScreen()
    {
        Shake.instance.startShake(.08f, .3f);
    }

    [PunRPC]
    void SpawnParticle()
    {
        Instantiate(deathFXEnemy, transform.position, Quaternion.identity);
    }
}
