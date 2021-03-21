using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpable : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 7.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }
    void OnCollisionExit()
    {
        isGrounded = false;
    }
    void FixedUpdate()
    {
        if (Input.GetKey("space") && isGrounded)
        {

            rb.AddForce(jump * jumpForce * Time.deltaTime, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
