using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float moveSpeed = 2f;  // ドアが開く速さ
    public float openHeight = 3f; // ドアが上がる高さ
    private Vector3 closedPosition; // 元の位置
    private bool isOpening = false;

    public string targetTag = "Player"; // 検出するオブジェクトのタグ（デフォルトは"Player"）

    void Start()
    {
        closedPosition = transform.position; // 初期位置を記録
    }

    void Update()
    {
        if (isOpening)
        {
            // ドアを上方向に移動
            transform.position = Vector3.MoveTowards(transform.position, closedPosition + Vector3.up * openHeight, moveSpeed * Time.deltaTime);

            // 目標地点に到達したら停止
            if (transform.position.y >= closedPosition.y + openHeight)
            {
                isOpening = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // 特定のタグを持つオブジェクトが範囲に入ったら
        {
            isOpening = true;
        }
    }
}
