﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRB;
    public SpriteRenderer mySR;
    public CameraMovement myCamera;
    public float playerSpeed, rollingSpeed, playerJump, jumpTimer, rollingTimer;
    public bool isGrabbingLedge = false;
    public bool isPlayingAnim = false;
    public int jumpsLeft;
    public bool isRolling, isGrounded, isCollidingLeft, isCollidingRight, isWallJumpingLeft, isWallJumpingRight, isAtCheckpoint, isShowingGlass, showedGlass;
    public Vector3 startPos, checkpoint;
 
 

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();

        jumpTimer = 0f;
        playerSpeed = 8f;
        rollingSpeed = 10f;
        rollingTimer = 0.5f;
        playerJump = 40f;
        isRolling = false;
        isGrounded = false;
        isShowingGlass = false;
        showedGlass = false;
        jumpsLeft = 2;
        startPos = new Vector3(-8.39f, -1.865f, -0.1f);
        checkpoint = new Vector3(99.2f, 25.09658f, -0.1f);
       
        
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

        if (isShowingGlass)
        {
            myCamera.isShowingGlass = true;
            myCamera.ShowGlass();
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
                jumpsLeft = 1;
                myRB.velocity.Set(-12, playerJump +2);
                myRB.AddForce(new Vector2(-12, playerJump + 4), ForceMode2D.Impulse);

            }
            else if (isWallJumpingLeft)
            {
                jumpsLeft = 1;
                myRB.velocity.Set(12, playerJump + 2);
                myRB.AddForce(new Vector2(12, playerJump + 4), ForceMode2D.Impulse);

                

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
            //transform.position = checkpoint;

            //loose life
        }
        if (col.gameObject.tag == "deadly2")
        {
            transform.position = checkpoint;
            //loose life
        }
        if (col.gameObject.name == "step1")
        {
            myCamera.CameraZoom11();
        }
        if (col.gameObject.name == "leaf (2)")
        {
            if (!showedGlass)
            {
                isShowingGlass = true;
                showedGlass = true;
            }
        }
        if (col.gameObject.tag == "Checkpoint")

        {
           LoadingScene();
        }

        if (col.gameObject.tag == "WinningCheckpoint")

        {
            WinScene();
        }
    }

    public void LoadingScene()
    {
        {
            
            SceneManager.LoadScene("Level 2");
            Time.timeScale = 1f;
        }
    }

    public void WinScene()
    {
        {

            SceneManager.LoadScene("Win");
            Time.timeScale = 1f;
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
