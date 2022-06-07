using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DisapearPau : MonoBehaviourPunCallbacks
{
    public GameObject pau;
    public Transform weapon;

    public Animator anim;
    PhotonView view;
    public LayerMask breakableObject;
    public int damageToBreak = 1;
    public float attackRate = 2f;
    float nextAttaxkTime = 0f;
    bool isHitting;


    private void Start()
    {
        view = GetComponent<PhotonView>();
        PhotonNetwork.RemoveRPCs(view);
        //view.RPC("hittingFALSE", RpcTarget.AllBuffered);
        anim.SetBool("isHitting", false);
    }

    private void Update ()
    {

        if (view.IsMine)
        {
            if (Time.time >= nextAttaxkTime)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    anim.SetBool("isHitting", true);
                    //view.RPC("hittingTRUE", RpcTarget.AllBuffered);
                    view.RPC("attack", RpcTarget.AllBuffered);
                    nextAttaxkTime = Time.time + 1 / attackRate;
                }
                if (true)
                {
                    if (!Input.GetKey(KeyCode.Mouse0))
                    {
                        anim.SetBool("isHitting", false);
                        //view.RPC("hittingFALSE", RpcTarget.AllBuffered);
                    }
                }
                


            }

        }

    }




    //[PunRPC]
    //public void hittingTRUE()
    //{
    //    anim.SetBool("isHitting", true);
    //}

    //[PunRPC]
    //public void hittingFALSE()
    //{
    //    anim.SetBool("isHitting", false);
    //}


    [PunRPC]
    public void attack()
    {
        //detetar material para partir
        Collider2D[] objectsToBreak =Physics2D.OverlapCircleAll(weapon.position, 0.5f, breakableObject);


        foreach (Collider2D breakObj in objectsToBreak)
        {
            Debug.Log("partiu");
            breakObj.GetComponent<BreakableObjects>().breakObject(damageToBreak);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (weapon == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(weapon.position, 0.5f);
    }
}
