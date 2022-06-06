using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{


    public float moveSpeed;
    float resetSpeed;
    public float size;
    public float dashSpeed;
    public float dashTime;
    private bool facingRight;
    public float minX, maxX, minY, maxY;
    public SpriteRenderer sr;

    public Text nameDisplay;
    public Canvas cav;
    public Camera cam;



    PhotonView view;
    public Rigidbody2D rb;
    Animator anim;
    Health healthScript;
    LineRenderer rend;

    Vector2 movement;

    

    public void Start()
    {
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
        resetSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        healthScript= FindObjectOfType<Health>();
        rend= FindObjectOfType<LineRenderer>();

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
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

            Wrap();

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                view.RPC("FlipTrue", RpcTarget.AllBuffered);

            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                view.RPC("FlipFalse", RpcTarget.AllBuffered);

            }


            if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero)
            {
                FindObjectOfType<AudioManager>().Play("Dash");
                StartCoroutine(Dash());
            }

            if (moveInput == Vector2.zero)
            {
                anim.SetBool("isRunning", false);
            }
            else
                anim.SetBool("isRunning", true);


            rend.SetPosition(0, transform.position);

        }
        else
            rend.SetPosition(1, transform.position);


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

    IEnumerator Dash()
    {
        moveSpeed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = resetSpeed;
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

    public void Wrap()
    {
        if (transform.position.x<minX)
        {
            transform.position = new Vector2(maxX, transform.position.y);

        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(-maxX, transform.position.y);

        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, maxY);

        }
        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, minY);

        }
    }

}
