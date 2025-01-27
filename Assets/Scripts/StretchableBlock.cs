using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchableBlock : MonoBehaviour
{
    public float moveHeight = 0.5f; // 上下移動の高さ
    public float moveSpeed = 2f; // 上下移動の速度
    public float shakeIntensity = 0.1f; // ぐらつきの強さ
    public int shakeCount = 5; // ぐらつく回数
    public float shakeDuration = 0.05f; // ぐらつき1回の時間
    private Vector3 originalPosition; // 元の位置
    private bool isMoving = false; // 上下移動を有効にするフラグ
    private bool isWaiting = false; // 停止中かどうかのフラグ
    private bool movingUp = true; // 現在上昇中か下降中かを管理

    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を記録
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && !isWaiting)
        {
            // 上昇または下降を処理
            Vector3 newPosition = transform.position;

            if (movingUp)
            {
                // 上昇
                newPosition.y += moveSpeed * Time.deltaTime;

                // 上限に達したらぐらついて停止して下降に切り替える
                if (newPosition.y >= originalPosition.y + moveHeight)
                {
                    newPosition.y = originalPosition.y + moveHeight;
                    StartCoroutine(ShakeWaitAndChangeDirection(false)); // ぐらつき＋停止後に下降
                }
            }
            else
            {
                // 下降
                newPosition.y -= moveSpeed * Time.deltaTime;

                // 下限に達したら上昇に切り替える（ぐらつきなし）
                if (newPosition.y <= originalPosition.y)
                {
                    newPosition.y = originalPosition.y;
                    movingUp = true; // 上昇に切り替え
                }
            }

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
        isWaiting = false;
        transform.position = new Vector3(transform.position.x, originalPosition.y, transform.position.z); // 元の高さにリセット
    }

    // コルーチンで遅延を実現
    private IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定秒数待機

        // 上下移動を開始
        isMoving = true;
    }

    // コルーチンでぐらつきを再現してから2秒間停止し、その後に方向を切り替える
    private IEnumerator ShakeWaitAndChangeDirection(bool nextMovingUp)
    {
        isWaiting = true; // 停止中フラグを有効化

        Vector3 targetPosition = transform.position; // 現在の位置を基準にぐらつきを計算

        // ぐらつきを再現
        for (int i = 0; i < shakeCount; i++)
        {
            // ランダムなぐらつき（上方向と下方向に揺れる）
            float randomShake = Random.Range(-shakeIntensity, shakeIntensity);
            transform.position = new Vector3(transform.position.x, targetPosition.y + randomShake, transform.position.z);
            yield return new WaitForSeconds(shakeDuration);
        }

        // ぐらつき終了後に位置を元に戻す
        transform.position = targetPosition;

        // 2秒間停止
        yield return new WaitForSeconds(2f);

        isWaiting = false; // 停止中フラグを解除
        movingUp = nextMovingUp; // 方向を切り替える
    }
}
