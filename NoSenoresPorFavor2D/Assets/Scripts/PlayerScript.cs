using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// konstant rulling funkar inte, leta upp ett sätt där man inte behöver använda sig utav rigid body 

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRB;
    public SpriteRenderer mySR;
    public float playerSpeed, playerJump, jumpTimer, rollingTimer, landingTimer;
    public bool isJumping = false;
    public bool isGrabbingLedge = false;
    public bool isPlayingAnim = false;
    public bool isRolling, isGrounded, isCollidingLeft, isCollidingRight, isWallJumping;
    public float collidingLeftOrRight;
    public LayerMask groundMask;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();

        jumpTimer = 1.2f;
        landingTimer = 1.1f;
        playerSpeed = 4f;
        rollingTimer = 0.5f;
        playerJump = 40f;
        isRolling = false;
        isGrounded = false;

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
        if (landingTimer <= 0)
        {
            isJumping = false;
            landingTimer = 1.1f;
        }

        if (Input.GetKeyUp(KeyCode.Space) && jumpTimer <= 0)
        {
            isJumping = true;
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

        if (isJumping == true && isWallJumping == false)
        {
            landingTimer -= Time.deltaTime;
        }


        //isGrounded = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 1.1f), new Vector2(1.2f, 0.2f), 0f, groundMask);
        //isCollidingLeft = Physics2D.OverlapBox(new Vector2(transform.position.x - 0.9f, transform.position.y + 0.2f), new Vector2(0.2f, 1.2f), 0f, groundMask);
        //isCollidingRight = Physics2D.OverlapBox(new Vector2(transform.position.x + 0.9f, transform.position.y + 0.2f), new Vector2(0.2f, 1.2f), 0f, groundMask);

        //if (isCollidingLeft)
        //{
        //    collidingLeftOrRight = 1;
        //}
        //if (isCollidingRight)
        //{
        //    collidingLeftOrRight = -1;
        //}

        //if (isWallJumping)
        //{
        //    myRB.velocity = new Vector2(playerSpeed * collidingLeftOrRight, playerJump);
        //}

        //if (Input.GetKeyDown(KeyCode.Space) && (isCollidingLeft || isCollidingRight))
        //{
        //    isWallJumping = true;
        //}



    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "wall" && isJumping == true)
        {
            myRB.AddForce(new Vector2(-13, playerJump + 2), ForceMode2D.Impulse);
            isWallJumping = true;
        }
        if (col.gameObject.name == "wall2" && isJumping == true)
        {
            myRB.AddForce(new Vector2(13, playerJump + 2), ForceMode2D.Impulse);

        }
        if (col.gameObject.name == "wall3" && isJumping == true)
        {
            isJumping = false;
            isWallJumping = false;

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
        if (col.gameObject.name == "enemy")
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
        //if (col.gameObject.name == "wall")
        //{
        //    isCollidingRight = true;
        //}
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

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y - 1.1f), new Vector2(1.2f, 0.2f));


    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawCube(new Vector2(transform.position.x - 0.9f, transform.position.y + 0.2f), new Vector2(0.2f, 1.2f));


    //    Gizmos.color = Color.white;
    //    Gizmos.DrawCube(new Vector2(transform.position.x + 0.9f, transform.position.y + 0.2f), new Vector2(0.2f, 1.2f));
    //}


}
