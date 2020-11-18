using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    public Rigidbody2D RB;
    public float speed, switchTimer;
    public bool goingUp, goingDown;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        speed = 3f;
        switchTimer = 3f;
        goingUp = true;
        goingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (goingUp == true)
        {
            switchTimer -= Time.deltaTime;
            RB.velocity = new Vector2(0, speed);
        }
        if (switchTimer <= 0)
        {
            goingUp = false;
            goingDown = true;
        }
        if (goingDown == true)
        {
            switchTimer += Time.deltaTime;
            RB.velocity = new Vector2(0, -speed);
        }
        if (switchTimer >= 3f)
        {
            goingDown = false;
            goingUp = true;
        }
    }
}
