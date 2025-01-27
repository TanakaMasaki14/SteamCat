using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Vector3 openOffset; // 扉が開くときの移動量
    public float moveTime = 2.0f; // 扉が開閉にかかる時間
    public float waitTime = 3.0f; // 扉が開いたまま待機する時間
    public float cooldownTime = 2.0f; // 扉のループ間のクールタイム

    private Vector3 closedPosition; // 扉が閉じているときの位置
    private Vector3 openPosition;   // 扉が開いているときの位置
    private bool isMoving = false;  // 扉が現在動いているかどうか
    private bool isTriggered = false; // 扉の動作をループするトリガー

    void Start()
    {
        // 扉の閉じた位置を初期状態として保存
        closedPosition = transform.position;
        openPosition = closedPosition + openOffset; // 開いた位置を計算
    }

    void Update()
    {
        // 永続動作トリガーが有効で、動作中でない場合に動作を開始
        if (isTriggered && !isMoving)
        {
            StartCoroutine(OpenCloseLoop());
        }
    }

    public void StartLoop()
    {
        isTriggered = true; // 永続動作を開始
    }

    private IEnumerator OpenCloseLoop()
    {
        isMoving = true;

        // 扉を開く
        yield return MoveDoor(closedPosition, openPosition);

        // 開いた状態で一定時間待機
        yield return new WaitForSeconds(waitTime);

        // 扉を閉じる
        yield return MoveDoor(openPosition, closedPosition);

        // クールタイムの待機
        yield return new WaitForSeconds(cooldownTime);

        isMoving = false;
    }

    private IEnumerator MoveDoor(Vector3 fromPosition, Vector3 toPosition)
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
