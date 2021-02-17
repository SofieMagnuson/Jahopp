using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    public PlayerScript playerScript;
    private Camera cam;
    public Vector3 offsetPos, offsetLookingUp, offsetRolling;
    public float cameraSpeed;

    void Start()
    {
        playerScript = GameObject.Find("player").GetComponent<PlayerScript>();
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();

        offsetPos = new Vector3(7.6f, 5.5f, -5.8f);
        offsetLookingUp = new Vector3(9.2f, 7.5f, -3.8f);
        offsetRolling = new Vector3(6.8f, 3.5f, -3.8f);

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
            Vector3 endPos = new Vector3(player.transform.position.x - 7, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

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
            Vector3 endPos = new Vector3(player.transform.position.x - 6.7f, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == true && Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 19.0f, player.transform.position.y + (offsetLookingUp.y + 4f), player.transform.position.z + offsetLookingUp.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == false && Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + (offsetLookingUp.x + 8f), player.transform.position.y + (offsetLookingUp.y + 4f), player.transform.position.z + offsetLookingUp.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 1 && playerScript.isRolling && playerScript.rollingTimer <= 0)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + (offsetRolling.x + 7f), player.transform.position.y + (offsetRolling.y + 2f), player.transform.position.z + offsetRolling.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == -1 && playerScript.isRolling && playerScript.rollingTimer <= 0)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 11, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == true && Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 19.0f, player.transform.position.y + (offsetLookingUp.y - 20f), player.transform.position.z + offsetLookingUp.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == false && Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + (offsetLookingUp.x + 9f), player.transform.position.y + (offsetLookingUp.y - 20f), player.transform.position.z + offsetLookingUp.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
    }

    public void CameraZoom11()
    {
        cam.orthographicSize = 11.0f;
    }

    public void CameraLower()
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + offsetPos.x, player.transform.position.y + offsetPos.y - 20f, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 7, player.transform.position.y + offsetPos.y - 20f, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == false)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + offsetPos.x, player.transform.position.y + offsetPos.y - 10f, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == true)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 6.7f, player.transform.position.y + offsetPos.y - 10f, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
    }
}
