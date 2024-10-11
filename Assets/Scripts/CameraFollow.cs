using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    // プレイヤーのTransform
    public Vector3 offset;      // プレイヤーとの距離（オフセット）
    public float fixedYPosition = 0f;  // カメラの固定Y位置
    public float smoothSpeed = 0f;  // カメラの追尾のスムーズさ

    void LateUpdate()
    {
        // プレイヤーのXおよびZの位置にオフセットを加えて、Yを固定した位置を設定
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, fixedYPosition, player.position.z + offset.z);

        // スムーズにカメラを追尾
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // プレイヤーを注視する
        transform.LookAt(player.position + Vector3.up * 2.0f);  // 高さオフセットを2.0に設定（調整可）
    }
}
