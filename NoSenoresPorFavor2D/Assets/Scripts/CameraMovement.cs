using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    public Vector3 offsetPos;
    public float cameraSpeed;

    void Start()
    {

        offsetPos = new Vector3(2.6f, 1.5f, -3.8f);
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
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == true)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x + offsetPos.x, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && player.GetComponent<SpriteRenderer>().flipX == false)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(player.transform.position.x - 1.2f, player.transform.position.y + offsetPos.y, player.transform.position.z + offsetPos.z);

            transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
        }

        //Vector3 startPos = transform.position;
        //Vector3 endPos = player.transform.position;

        //endPos.x += offsetPos.x;
        //endPos.y += offsetPos.y;
        //endPos.z += offsetPos.z;

        //transform.position = Vector3.Lerp(startPos, endPos, cameraSpeed * Time.deltaTime);
    }

}
