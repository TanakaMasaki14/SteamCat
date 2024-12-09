using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 0f;  
    public float jumpForce = 0f;   
    private bool isJumping = false;
    private Rigidbody rb;          

    void Start()
    {
        rb = GetComponent<Rigidbody>();  
    }

    void Update()
    {
        
        float moveInput = Input.GetAxis("Horizontal"); 
        Vector3 move = new Vector3(rb.velocity.x, rb.velocity.y, moveInput * moveSpeed);  

        rb.velocity = move;

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}