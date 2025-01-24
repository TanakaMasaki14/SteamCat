using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bertMove : MonoBehaviour
{
    // 横方向への速度
    public float speed = 2.0f;

    // 反転するまでの時間（秒）
    public float changeDirectionInterval = 3.0f;

    private float timeElapsed = 0.0f;
    private int direction = 1; // 1で右方向、-1で左方向

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null)
        {
            // 経過時間をカウント
            timeElapsed += Time.deltaTime;

            // 指定した時間が経過したら方向を反転
            if (timeElapsed >= changeDirectionInterval)
            {
                direction *= -1; // 方向を反転
                timeElapsed = 0.0f; // 経過時間をリセット
            }

            // 現在の方向に基づいて速度を適用
            Vector3 moveDirection = transform.right * direction; // ベルトの横方向
            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
        }
    }
}