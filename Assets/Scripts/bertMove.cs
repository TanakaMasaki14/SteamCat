using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bertMove : MonoBehaviour
{
    // 横方向への速度
    public float speed = 2.0f;

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null)
        {
            // Y軸方向の速度を保持しつつ、横（X軸）方向にのみ速度を適用
            Vector3 moveDirection = transform.right; // ベルトの横方向
            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
        }
    }
}