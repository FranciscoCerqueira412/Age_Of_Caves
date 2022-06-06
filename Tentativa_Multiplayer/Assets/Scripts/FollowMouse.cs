using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FollowMouse : MonoBehaviourPunCallbacks
{
    PhotonView view;

    public GameObject[] Hand;
    private void Start()
    {
        view = GetComponent<PhotonView>();

        if (view.IsMine)
        {
            randomLookMouse();
            return;
            
        }
        else
        {
            Hand[0].SetActive(false);
            Hand[1].SetActive(false);
            Hand[2].SetActive(false);
            Hand[3].SetActive(false);



        }
    }
    void randomLookMouse()
    {
        int randomInt = Random.Range(0, Hand.Length);
        Hand[randomInt].SetActive(true);
        Debug.Log(randomInt);
    }

    private void Update()
    {
        if (view.IsMine)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition - transform.position);
            difference.Normalize();
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        }

    }
}
