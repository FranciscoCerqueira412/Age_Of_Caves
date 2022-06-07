using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class SurvivalPlayerController : MonoBehaviourPunCallbacks
{


    public float moveSpeed;
    public float size;

    public SpriteRenderer sr;
    public Rigidbody2D rb;


    public Text nameDisplay;


    PhotonView view;
    Animator anim;
    Health healthScript;

    public GameObject playerCamera;
    public GameObject followMouse;

    public void Awake()
    {
        if (photonView.IsMine)
        {
            playerCamera.SetActive(true);
        }
    }


    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("BackgroundMusicS");
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        healthScript= FindObjectOfType<Health>();

        if (view.IsMine)
        {
            nameDisplay.text = PhotonNetwork.NickName;
        }
        else
        {
            nameDisplay.text = view.Owner.NickName;
            nameDisplay.color = Color.magenta;
        }
    }


    private void Update()
    {
        if (view.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized ;
            Vector2 moveAmount = moveInput.normalized * moveSpeed * Time.deltaTime;
            rb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
            Physics2D.IgnoreLayerCollision(6, 6, true);


            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                view.RPC("FlipTrue", RpcTarget.AllBuffered);

            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                view.RPC("FlipFalse", RpcTarget.AllBuffered);

            }


            if (moveInput == Vector2.zero)
            {
                anim.SetBool("isRunning", false);
            }
            else
                anim.SetBool("isRunning", true);

        }
       
    }

    [PunRPC]
    private void FlipTrue()
    {
        sr.flipX = true;
    }
    [PunRPC]
    private void FlipFalse()
    {
        sr.flipX = false;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine)
        {
            if (collision.tag == "Enemy")
            {
                healthScript.takeDamage();
            }
        }
    }

   

}
