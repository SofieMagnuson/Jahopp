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
    public bool isRolling, isGrounded, isCollidingLeft, isCollidingRight, isWallJumpingLeft, isWallJumpingRight, isAtCheckpoint, isShowingGlass, showedGlass, candoublejump;
    public Vector3 startPos, checkpoint;
 
 

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();

        jumpTimer = 0f;
        playerSpeed = 9f;
        rollingSpeed = 11f;
        rollingTimer = 0.5f;
        playerJump = 40f;
        isRolling = false;
        isGrounded = false;
        isShowingGlass = false;
        showedGlass = false;
        startPos = new Vector3(-8.39f, -1.865f, -0.1f);
        checkpoint = new Vector3(86.2f, 25.09658f, -0.1f);
       
        
    }

    void Update()
    {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (!isShowingGlass)
        {
            transform.position += movement * Time.deltaTime * playerSpeed;
        }
        

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            mySR.flipX = false;
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            mySR.flipX = true;
        }
       
        if (Input.GetKey(KeyCode.RightShift) && (Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Horizontal") == 1))
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

        //hoppa
        if (Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            if (isGrounded)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, 0);
                myRB.AddForce(new Vector2(0, playerJump), ForceMode2D.Impulse);
                candoublejump = true;
            }
            else
            {
                if (candoublejump)
                {
                    myRB.velocity = new Vector2(myRB.velocity.x, 0);
                    myRB.AddForce(new Vector2(0, playerJump), ForceMode2D.Impulse);
                    candoublejump = false;
                }
            }

        }
    }
    void FixedUpdate()
    {

        if (Input.GetKeyUp(KeyCode.RightShift) && jumpTimer <= 0 && isGrounded)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, 0);
            myRB.AddForce(new Vector2(0, playerJump), ForceMode2D.Impulse);
            isGrounded = false;
            candoublejump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "deadly")
        {
            transform.position = startPos;
        }
        if (col.gameObject.tag == "deadly2")
        {
            transform.position = checkpoint;
        }
        if (col.gameObject.tag == "enemy")
        {
            if (isRolling)
            {
                Destroy(col.gameObject);
            }
            else
            {
                transform.position = checkpoint;
            }
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
        if (col.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
}
