using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 今範囲内にいてPキー押したら反応するようになている。
public class MovingCrane : MonoBehaviour
{
    // 移動のための2つのポジションを設定するための変数
    public Transform pos1; // 最初の位置
    public Transform pos2; // 目的の位置
    public float speed = 2f; // 移動速度
    public float rotationSpeed = 200f; // 回転速度
    public float detectionRange = 5f; // 反応する範囲 クレーンが反応する範囲

    // 範囲内に反応するオブジェクトのタグを設定する（自由に変更可能）
    public string targetTag = "Target";

    private bool movingToPos2 = true; // 最初の移動先を指定
    private Vector3 targetPosition;
    private Transform targetObject;

    void Start()
    {
        // 初期の移動先を設定
        targetPosition = pos2.position;
    }

    void Update()
    {
        // 指定範囲内のオブジェクトを検出
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);

        // 対象のオブジェクトが範囲内にあるかを確認
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                targetObject = collider.transform;
                break;
            }
        }

        // Pキーが押されたとき、かつターゲットオブジェクトが範囲内にある場合
        if (Input.GetKeyDown(KeyCode.P) && targetObject != null)
        {
            // 現在の位置からターゲット位置までのベクトルを計算
            Vector3 direction = targetPosition - transform.position;

            // 足場の移動
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // 回転処理（目的地に向かって回転）
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 目標位置に到達したかをチェック
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                // 目標位置を切り替え
                if (movingToPos2)
                {
                    targetPosition = pos1.position;
                }
                else
                {
                    targetPosition = pos2.position;
                }
                movingToPos2 = !movingToPos2; // フラグを反転
            }
        }
    }

    // クレーンの反応範囲を視覚化する（デバッグ用）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
