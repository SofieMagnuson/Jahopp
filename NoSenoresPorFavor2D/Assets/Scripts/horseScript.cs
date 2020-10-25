using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horseScript : MonoBehaviour
{
    public Rigidbody2D RB;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        speed = -3f;
    }

    // Update is called once per frame
    void Update()
    {
        //RB.velocity = new Vector2(speed, 0);
    }
}
