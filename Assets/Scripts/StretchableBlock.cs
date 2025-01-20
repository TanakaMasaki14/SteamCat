using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchableBlock : MonoBehaviour
{
    public float moveHeight = 0.5f; // 上下移動の高さ
    public float moveSpeed = 2f; // 上下移動の速度
    private Vector3 originalPosition; // 元の位置
    private bool isMoving = false; // 上下移動を有効にするフラグ
    private float startTime; // 移動開始時の基準時間

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を記録
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // 上下移動する位置を計算（時間の基準をリセット）
            Vector3 newPosition = originalPosition;
            float elapsedTime = Time.time - startTime; // 開始時間を基準にした経過時間
            newPosition.y = 0 + Mathf.PingPong(elapsedTime * moveSpeed, moveHeight); // y=0を基準に移動
            transform.position = newPosition;
        }
    }

    // 上下移動を開始（遅延あり）
    public void StartMovingWithDelay(float delay)
    {
        StartCoroutine(StartMovingAfterDelay(delay));
    }

    // 上下移動を停止
    public void StopMoving()
    {
        isMoving = false;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z); // y=0にリセット
    }

    // コルーチンで遅延を実現
    private IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定秒数待機

        // 移動開始時の基準時間を記録
        startTime = Time.time;

        // 上下移動を開始
        isMoving = true;
    }
}