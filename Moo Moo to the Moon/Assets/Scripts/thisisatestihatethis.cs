
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class thisisatestihatethis : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    public float acceleration = 0.2f;
    public float maxSpeed = 1.0f;
    public Vector3 speed;
    // Start is called before the first frame update
    void Start()
    {
        xSpeed = 0.0f * Time.deltaTime;
        ySpeed = 0.0f * Time.deltaTime;
        speed = new Vector3(xSpeed, ySpeed, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        testMove();
        gameObject.GetComponent<thisisatestihatethis>().transform.position += speed
    }
    void testMove() {

        while (Input.inputString == "d" && xSpeed < maxSpeed) {
            xSpeed += acceleration;
        }


        /*switch (Input.inputString)
        {
            case "d":
                xSpeed += acceleration;
                Debug.Log("xSpeed: " + xSpeed);
                break;
            case "a":
                xSpeed -= acceleration;
                Debug.Log("xSpeed: " + xSpeed);
                break;
        }*/
    }
}