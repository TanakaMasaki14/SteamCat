using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 0f;   // 移動速度
    public float jumpForce = 0f;   // ジャンプ力
    private bool isJumping = false;　　//ジャンプしているか
    private Rigidbody rb;          // Rigidbodyコンポーネント

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbodyを取得
    }

    void Update()
    {
        // プレイヤーのZ軸移動 (AとDキーまたは左・右矢印キー)
        float moveInput = Input.GetAxis("Horizontal");  // "A"と"D"または左・右キーで移動
        Vector3 move = new Vector3(rb.velocity.x, rb.velocity.y, moveInput * moveSpeed);  // Z軸に移動

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
