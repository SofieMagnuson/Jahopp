using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRB;
    public SpriteRenderer mySR;
    public float playerSpeed, rollingSpeed, playerJump, jumpTimer, rollingTimer;
    public bool isGrabbingLedge = false;
    public bool isPlayingAnim = false;
    public int jumpsLeft;
    public bool isRolling, isGrounded, isCollidingLeft, isCollidingRight, isWallJumpingLeft, isWallJumpingRight;
    public Vector3 startPos;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();

        jumpTimer = 1f;
        playerSpeed = 6f;
        rollingSpeed = 10f;
        rollingTimer = 0.5f;
        playerJump = 40f;
        isRolling = false;
        isGrounded = false;
        jumpsLeft = 2;
        startPos = new Vector3(-8.39f, -1.865f, -0.1f);

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
            playerSpeed = rollingSpeed;
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
                playerSpeed = 6f;
            }
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space) && jumpsLeft != 0)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, 0);
            myRB.AddForce(new Vector2(0, playerJump), ForceMode2D.Impulse);
            isGrounded = false;
            jumpsLeft -= 1;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && jumpsLeft != 0 && jumpTimer <= 0)
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
                //jumpsLeft = 1;
                myRB.velocity.Set(-12, playerJump +2);
                //myRB.AddForce(new Vector2(-12, playerJump + 4), ForceMode2D.Impulse);

            }
            else if (isWallJumpingLeft)
            {
                //jumpsLeft = 1;
                myRB.velocity.Set(12, playerJump + 2);
                //myRB.AddForce(new Vector2(12, playerJump + 4), ForceMode2D.Impulse);

            }

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "MiddleTree" && !isGrounded)
        {
            isWallJumpingRight = true;
        }
        if (col.gameObject.name == "FirstTree" && !isGrounded)
        {
            isWallJumpingLeft = true;

        }
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
            jumpsLeft = 2;
        }
        if (col.gameObject.tag == "wall")
        {
            jumpsLeft = 1;
        }
        if (col.gameObject.tag == "specialwall")
        {
            jumpsLeft = 2;
        }
        if (col.gameObject.tag == "deadly")
        {
            transform.position = startPos;
            //loose life
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "MiddleTree")
        {
            isWallJumpingRight = false;
        }
        if (col.gameObject.name == "FirstTree")
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
