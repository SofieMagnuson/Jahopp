using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRB;
    public SpriteRenderer mySR;
    public float playerSpeed, playerJump, jumpTimer, rollingTimer;
    public bool isGrabbingLedge = false;
    public bool isPlayingAnim = false;
    public int jumpsLeft;
    public bool isRolling, isGrounded, isCollidingLeft, isCollidingRight, isWallJumpingLeft, isWallJumpingRight;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();

        jumpTimer = 1f;
        playerSpeed = 4f;
        rollingTimer = 0.5f;
        playerJump = 40f;
        isRolling = false;
        isGrounded = false;
        jumpsLeft = 2;

        //gameObject.GetComponent<Animator>().enabled = false;
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
          
        }

      
        if (rollingTimer <= 0)
        {
            playerSpeed = 8f;
        }

        if (isRolling)
        {
            jumpTimer -= Time.deltaTime;
            rollingTimer -= Time.deltaTime;
        }
        else
        {
            rollingTimer = 0.5f;
            if (isGrounded)
            {
                jumpTimer = 1f;
                playerSpeed = 4f;
            }
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space) && jumpTimer <= 0 && jumpsLeft != 0)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, 0);
            myRB.AddForce(new Vector2(0, playerJump), ForceMode2D.Impulse);
            isGrounded = false;
            jumpsLeft -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isWallJumpingRight)
            {
                myRB.velocity.Set(-12, playerJump +2);
                //myRB.AddForce(new Vector2(-12, playerJump + 4), ForceMode2D.Impulse);

            }
            else if (isWallJumpingLeft)
            {
                myRB.velocity.Set(12, playerJump + 2);
                //myRB.AddForce(new Vector2(12, playerJump + 4), ForceMode2D.Impulse);

            }

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "wall" && !isGrounded)
        {
            isWallJumpingRight = true;
        }
        if (col.gameObject.name == "wall2" && !isGrounded)
        {
            isWallJumpingLeft = true;

        }
        if (col.gameObject.name == "wall3")
        {
        }
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
            jumpsLeft = 2;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "wall")
        {
            isWallJumpingRight = false;
        }
        if (col.gameObject.name == "wall2")
        {
            isWallJumpingLeft = false;
        }
    }


    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.name == "ground2")
    //    {
    //        myRB.gravityScale = 0;
    //        myRB.velocity = Vector3.zero;
    //        isJumping = true;
    //        jumpTimer = 100f;
    //        isGrabbingLedge = true;
    //    }
    //    if (col.gameObject.name == "enemy")
    //    {
    //        if (isRolling == true)
    //        {
    //            Destroy(col.gameObject);
    //            //if(playerSpeed > 5f)
    //            //{
    //            //    playerSpeed = playerSpeed - 2f;
    //            //    accelarationTimer = accelarationTimer - 4f;
    //            //}
    //        }
    //    }
    //    //if (col.gameObject.name == "wall")
    //    //{
    //    //    isCollidingRight = true;
    //    //}
    //}

    //private void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.gameObject.name == "ground2")
    //    {
    //        myRB.gravityScale = 3;
    //        isJumping = false;
    //        jumpTimer = 0f;
    //        isGrabbingLedge = false;
    //    }
    //}

}
