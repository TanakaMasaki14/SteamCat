using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Vector3 openOffset; // 開くときの位置の変化量 (相対的な座標)
    public float moveTime = 5.0f; // 扉が開く・閉じるのにかかる時間
    public float waitTime = 3.0f; // 扉が開いた後に待つ時間

    private Vector3 closedPosition; // 扉が閉じているときの位置
    private Vector3 openPosition;   // 扉が開いているときの位置
    private bool isMoving = false;  // 扉が現在動いているかどうか

    void Start()
    {
        // 初期位置を閉じた状態として保存
        closedPosition = transform.position;
        openPosition = closedPosition + openOffset; // 開いた状態の位置を計算
    }

    public void OpenDoor()
    {
        if (!isMoving) // すでに動作中でない場合のみ
        {
            StartCoroutine(OpenAndCloseDoor());
        }
    }

    private System.Collections.IEnumerator OpenAndCloseDoor()
    {
        isMoving = true; // 扉が動作中であることを記録

        // 扉を開く
        yield return MoveDoor(closedPosition, openPosition);

        // 指定した時間待機
        yield return new WaitForSeconds(waitTime);

        // 扉を閉じる
        yield return MoveDoor(openPosition, closedPosition);

        isMoving = false; // 扉の動作が完了
    }

    private System.Collections.IEnumerator MoveDoor(Vector3 fromPosition, Vector3 toPosition)
    {
        float elapsedTime = 0;

        // 扉を滑らかに動かす
        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(fromPosition, toPosition, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最後に位置を正確に設定
        transform.position = toPosition;
    }
}
