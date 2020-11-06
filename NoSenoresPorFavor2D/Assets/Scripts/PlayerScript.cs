using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// konstant rulling funkar inte, leta upp ett sätt där man inte behöver använda sig utav rigid body 

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRB;
    public SpriteRenderer mySR;
    public float playerSpeed, playerJump, jumpTimer, animTimer, rollingTimer;
    public bool isJumping = false;
    public bool isGrabbingLedge = false;
    public bool isPlayingAnim = false;
    public bool isRolling;
    //public AnimationClip pullingUp;
    //public Animator anim;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        //pullingUp = Resources.Load<AnimationClip>("Player_pulling_up");
        //anim = GetComponent<Animator>();

        jumpTimer = 1.2f;
        playerSpeed = 4f;
        rollingTimer = 0.5f;
        playerJump = 40f;
        isRolling = false;

        //gameObject.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        transform.position += movement * Time.deltaTime * playerSpeed;

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            mySR.flipX = false;
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            mySR.flipX = true;
        }
        if (Input.GetKey(KeyCode.Space) && (Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Horizontal") == 1))
        {
            isRolling = true;
        }
        else
        {
            isRolling = false;
            playerSpeed = 4f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && jumpTimer <= 0)
        {
            if (mySR.flipX == false)
            {
                myRB.AddForce(new Vector2(8, playerJump), ForceMode2D.Impulse);
            }
            else
            {
                myRB.AddForce(new Vector2(-8, playerJump), ForceMode2D.Impulse);
            }
        }
        if (rollingTimer <= 0)
        {
            playerSpeed = 8f;
        }

        if (isRolling == true)
        {
            jumpTimer -= Time.deltaTime;
            rollingTimer -= Time.deltaTime;
        }
        else
        {
            jumpTimer = 1.5f;
            rollingTimer = 0.5f;
        }

        if (isJumping == true)
        {
        }
        //if (isGrabbingLedge == true)
        //{
        //    //anim.SetBool("isHoldingLedge", true); 
        //    gameObject.GetComponent<Animator>().enabled = true;
        //    animTimer = 0.7f;
        //    isPlayingAnim = true;
        //}
        //if (isPlayingAnim == true)
        //{
        //    animTimer -= Time.deltaTime;
        //}
        //if (animTimer <= 0)
        //{
        //    //anim.SetBool("isHoldingLedge", false);

        //    gameObject.GetComponent<Animator>().enabled = false;
        //    isPlayingAnim = false;
        //}


    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "ground2")
        {
            myRB.gravityScale = 0;
            myRB.velocity = Vector3.zero;
            isJumping = true;
            jumpTimer = 100f;
            isGrabbingLedge = true;
        }
        if (col.gameObject.name == "horse")
        {
            if (isRolling == true)
            {
                Destroy(col.gameObject);
                //if(playerSpeed > 5f)
                //{
                //    playerSpeed = playerSpeed - 2f;
                //    accelarationTimer = accelarationTimer - 4f;
                //}
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "ground2")
        {
            myRB.gravityScale = 3;
            isJumping = false;
            jumpTimer = 0f;
            isGrabbingLedge = false;
        }
    }


}
