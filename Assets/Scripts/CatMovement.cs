using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 0f;   // 
    public float jumpForce = 0f;   // 

    private bool isJumping = false; // 
    private Rigidbody rb;           // 

    // 
    public float meowRadius = 2f; // 
    public LayerMask meowLayerMask; // 

    // 
    public Transform holdPoint;   // 
    private GameObject pickedObject;  //

    private bool isFacingRight = true;// åªç›âEå¸Ç´Ç©Ç«Ç§Ç©

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // 
    }

    void Update()
    {
        // 
        float moveInput = Input.GetAxis("Horizontal");  // 
        Vector3 move = new Vector3(rb.velocity.x, rb.velocity.y, moveInput * moveSpeed);  // 

        rb.velocity = move;
        // 
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true;
        }

        // 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Meow();
        }

        // 
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickedObject == null) // 
            {
                RaycastHit hit;
                // 
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
                {
                    if (hit.collider.CompareTag("Pickupable"))
                    {
                        PickupObject(hit.collider.gameObject);
                    }

                    if (hit.collider.CompareTag("Pickupable2"))
                    {
                        PickupObject(hit.collider.gameObject);
                    }
                }
            }
            else // 
            {
                DropObject();
            }
        }

        // âÒì]ÇÃèàóùÇí«â¡(âÒì]Ç≈ç∂âEÇÃå¸Ç´ÇïœçX)
        if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
        else if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    // 
    private void Meow()
    {
        Debug.Log("");

        // 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meowRadius, meowLayerMask);
        // 
        foreach (Collider hitCollider in hitColliders)
        {
            Renderer objRenderer = hitCollider.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                // 
                objRenderer.material.color = Color.red;

                // 
                StartCoroutine(ResetColor(objRenderer));
            }
        }
    }
    private void Flip()
    {
        // ç∂âEîΩì]Ç∑ÇÈÇΩÇﬂÇ…Yé≤ï˚å¸Ç…âÒì]
        isFacingRight = !isFacingRight;
        float rotationY = isFacingRight ? 0 : 180;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    // 
    private IEnumerator ResetColor(Renderer objRenderer)
    {
        yield return new WaitForSeconds(1f); // 
        objRenderer.material.color = Color.white; // 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Pickupable"))
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Pickupable2"))
        {
            isJumping = false;
        }

    }

    // 
    private void PickupObject(GameObject obj)
    {
        pickedObject = obj;

        // 
        Rigidbody objRb = pickedObject.GetComponent<Rigidbody>();
        if (objRb != null)
        {
            objRb.isKinematic = true; //
        }

        // 
        pickedObject.transform.position = holdPoint.position;
        pickedObject.transform.parent = holdPoint;
    }

    // 
    private void DropObject()
    {
        if (pickedObject != null)
        {
            Rigidbody objRb = pickedObject.GetComponent<Rigidbody>();
            if (objRb != null)
            {
                objRb.isKinematic = false; // 
            }

            // 
            pickedObject.transform.parent = null;
            pickedObject = null;
        }
    }

    // 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meowRadius);
    }

}
