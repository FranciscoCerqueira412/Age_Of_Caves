using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BreakableObjects : MonoBehaviourPunCallbacks
{
    public int maxHealth = 10;
    public int currentHealth;

    public GameObject dropPrefab;
    PhotonView view;
    public Animator anim;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        PhotonNetwork.RemoveRPCs(view);
        currentHealth = maxHealth;
        
    }


    public void breakObject(int damage)
    {

        Vector3 randomPos = new Vector3 (Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        Vector3 addSpaceBetweenDrop1 = new Vector3(-0.2f, 0.1f, 0f);
        Vector3 addSpaceBetweenDrop2 = new Vector3(0.1f, 0.2f, 0f);
        Vector3 addSpaceBetweenDrop3 = new Vector3(0.05f, -0.2f, 0f);

        currentHealth -= damage;
            view.RPC("shakeRPC", RpcTarget.AllBuffered);
            if (currentHealth <= 0)
            {
            if (PhotonNetwork.IsMasterClient)
            {
                Die();
                PhotonNetwork.Instantiate(dropPrefab.name, this.gameObject.transform.position + addSpaceBetweenDrop1 + randomPos, Quaternion.identity);
                PhotonNetwork.Instantiate(dropPrefab.name, this.gameObject.transform.position + addSpaceBetweenDrop2 + randomPos, Quaternion.identity);
                PhotonNetwork.Instantiate(dropPrefab.name, this.gameObject.transform.position + addSpaceBetweenDrop3 + randomPos, Quaternion.identity);
            }

            }

    }

    void Die()
    {
        Debug.Log("Block Destoyed");
        PhotonNetwork.Destroy(gameObject);

    }

    [PunRPC]
    public void shakeRPC()
    {
       anim.SetTrigger("MoveBreakable");
    }



}
