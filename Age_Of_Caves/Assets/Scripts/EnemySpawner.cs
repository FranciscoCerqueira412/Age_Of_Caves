using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float startTimeBtwSpanws;
    public float timeBtwSpanws;
    public float timeWave;
    public GameObject[] enemyPreFabs;
    int randomMonster;

    [HideInInspector]public Score score;
    //public Enemy_Dino din;


    private void Start()
    {
        timeBtwSpanws = startTimeBtwSpanws;
        timeWave = 4;
        score = FindObjectOfType<Score>();
        //din = FindObjectOfType<Enemy_Dino>();


    }
    private void Update()
    {

        if (score.score>=0 && score.score<10)
        {
            timeWave = 4;
            
        }
        else if (score.score >= 10 && score.score < 20)
        {
            timeWave = 3f;
            
        }
        else if (score.score >= 20 && score.score < 40)
        {
            timeWave = 2.5f;
            
        }
        else if (score.score >= 40 && score.score < 60)
        {
            timeWave = 2f;
            
        }
        else if (score.score >= 60 )
        {
            timeWave = 1.5f;
            
        }


        if (PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount!=2)
        {
            return;
        }

        if (timeBtwSpanws <= 0)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            randomMonster = Random.Range(0, enemyPreFabs.Length);
            PhotonNetwork.Instantiate(enemyPreFabs[randomMonster].name, spawnPosition, Quaternion.identity);
            timeBtwSpanws = timeWave;

        }

        else
        {

         timeBtwSpanws -= Time.deltaTime;

        }
            

    }
}
