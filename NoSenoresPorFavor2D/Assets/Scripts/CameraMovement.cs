using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    public PlayerScript playerScript;
    public Vector3 offsetPos, offsetLookingUp, offsetRolling;
    public float cameraSpeed;

    void Start()
    {
        playerScript = GameObject.Find("player").GetComponent<PlayerScript>();

        offsetPos = new Vector3(2.6f, 1.5f, -3.8f);
        offsetLookingUp = new Vector3(3.2f, 5.5f, -3.8f);
        offsetRolling = new Vector3(6.8f, 1.5f, -3.8f);


        cameraSpeed = 3f;
    }


    void Update()
    {

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + offsetPos.x, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 1, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == false)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + offsetPos.x, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == true)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 1.2f, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == true && Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 4.0f, player.transform.position.y + offsetLookingUp.y, player.transform.position.z + offsetLookingUp.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == false && Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + offsetLookingUp.x, player.transform.position.y + offsetLookingUp.y, player.transform.position.z + offsetLookingUp.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 1 && playerScript.isRolling && playerScript.rollingTimer <= 0)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + offsetRolling.x, player.transform.position.y + offsetRolling.y, player.transform.position.z + offsetRolling.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == -1 && playerScript.isRolling && playerScript.rollingTimer <= 0)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 6, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }


    }

}
