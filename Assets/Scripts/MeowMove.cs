using System.Collections;
using UnityEngine;

public class MeowMove : MonoBehaviour
{
    public float moveDistance = 0f; // 動く距離
    public float moveSpeed = 0f;    // 動く速度
    private Vector3 originalPosition; // 元の位置
    private bool isMoving = false;    // 動いているかどうか

    void Start()
    {
        originalPosition = transform.position; // 初期位置を記録
    }

    public void MoveUpAndDown()
    {
        if (!isMoving) // 動作中でない場合のみ実行
        {
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine()
    {
        isMoving = true;

        // 上に移動
        Vector3 targetPosition = originalPosition + new Vector3(0, moveDistance, 0);
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}