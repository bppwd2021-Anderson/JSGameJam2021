
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    float xSpeed;
    float ySpeed;
    public float speed = 5;
    public float jumpVelocity = 10f;
    public Rigidbody2D rb;
    public bool isGrounded = true;
    public LayerMask groundLayer;
    public Transform raycastPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Player Movement
        xSpeed = Input.GetAxis("Horizontal");
        ySpeed = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(xSpeed, 0, 0);

        if (moveVector.magnitude > 0.1)
        {
            GetComponent<Animator>().SetBool("walking", true);

            transform.position += moveVector * speed * Time.deltaTime;
        }
        else
        {
            GetComponent<Animator>().SetBool("walking", false);
        }
        #endregion

        #region Player Jump
        if (Input.GetButtonDown("Space") && isGrounded)
        {
            Debug.Log("JUMPING");
            rb.velocity = Vector2.up * jumpVelocity;
            isGrounded = false;
        }

        else if(!isGrounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(raycastPos.position, Vector2.down, 0.5f, groundLayer);
            Debug.DrawRay(raycastPos.position, Vector2.down * 0.5f, Color.black);

            if (hit.collider != null)
            {
                isGrounded = true;
            }
             
        }
        #endregion
    }

}