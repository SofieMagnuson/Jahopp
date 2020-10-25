using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRB;
    public SpriteRenderer mySR;
    public float playerSpeed, playerJump, jumpTimer, animTimer, accelarationTimer;
    public bool isJumping = false;
    public bool isGrabbingLedge = false;
    public bool isPlayingAnim = false;
    public bool isRolling;
    public AnimationClip pullingUp;
    public Animator anim;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        pullingUp = Resources.Load<AnimationClip>("Player_pulling_up");
        anim = GetComponent<Animator>();

        
        playerSpeed = 4f;
        accelarationTimer = 0;
        playerJump = 40f;
        isRolling = false;

        gameObject.GetComponent<Animator>().enabled = false;
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
            mySR.flipX = true;
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            mySR.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            myRB.AddForce(new Vector2(0, playerJump), ForceMode2D.Impulse);
            isJumping = true;
            jumpTimer = 0.9f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && isRolling == false)
        {
            isRolling = true;
            playerSpeed = 6f;
           
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isRolling == true)
        {
            isRolling = false;
            playerSpeed = 4f;
            accelarationTimer = 0;
        }

        if (isRolling == true)
        {
            accelarationTimer += Time.deltaTime;
            if (accelarationTimer > 2)
            {
                playerSpeed = 7f;
            }
            if (accelarationTimer > 4)
            {
                playerSpeed = 8f;
            }
            if (accelarationTimer > 6)
            {
                playerSpeed = 9f;
            }
            if (accelarationTimer > 8)
            {
                playerSpeed = 10f;
            }
        }

        if (isJumping == true)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (jumpTimer <= 0)
        {
            isJumping = false;
        }
        if (isGrabbingLedge == true)
        {
            //anim.SetBool("isHoldingLedge", true); 
            gameObject.GetComponent<Animator>().enabled = true;
            animTimer = 0.7f;
            isPlayingAnim = true;
        }
        if (isPlayingAnim == true)
        {
            animTimer -= Time.deltaTime;
        }
        if (animTimer <= 0)
        {
            //anim.SetBool("isHoldingLedge", false);

            gameObject.GetComponent<Animator>().enabled = false;
            isPlayingAnim = false;
        }


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
                if(playerSpeed > 5f)
                {
                    playerSpeed = playerSpeed - 2f;
                    accelarationTimer = accelarationTimer - 4f;
                }
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
