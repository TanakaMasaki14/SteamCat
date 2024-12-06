using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    // プレイヤーのTransform
    public Vector3 offset;      // プレイヤーとの距離（オフセット）
    public float smoothSpeed = 0.125f;  // カメラの追尾のスムーズさ（任意のデフォルト値）

    void LateUpdate()
    {
        // プレイヤーの位置にオフセットを加えて、Y軸を固定しない位置を設定
        Vector3 desiredPosition = player.position + offset;

        // スムーズにカメラを追尾
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // プレイヤーを注視する
        transform.LookAt(player.position + Vector3.up * 2.0f);  // 高さオフセットを2.0に設定（調整可）
    }
}
