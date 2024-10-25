using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 0f;   // 移動速度
    public float jumpForce = 0f;   // ジャンプ力
    private bool isJumping = false;　　//ジャンプしているか
    private Rigidbody rb;          // Rigidbodyコンポーネント
    private bool isFacingRight = true; // 現在右向きかどうか

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

        // 回転の処理を追加
        if(moveInput < 0 && isFacingRight)
        {
            Flip();
        }
        else if(moveInput > 0 && !isFacingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true;
        }


    }

    private void Flip()
    {
        // 左右反転するためにY軸方向に回転
        isFacingRight = !isFacingRight;
        float rotationY = isFacingRight ? 0 : 180;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

}
