using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool moveType; // 往復するかの選択
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2.0f;
    private bool movingToB = true;
    private bool isActive = false; // ← 追加: 動作スイッチ

    // プレイヤーを乗せるために、トリガーコライダーで接触を検知する
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player landed on platform");
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player left platform");
            collision.transform.SetParent(null);
        }
    }

    void Update()
    {
        if (!isActive) return; // ← 追加: アクティブになるまで動かない

        if (moveType)
        {
            if (movingToB)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
                if (transform.position == pointB) movingToB = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
                if (transform.position == pointA) movingToB = true;
            }
        }
        else
        {
            if (movingToB)
            {
                transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
                if (transform.position == pointB) movingToB = false;
            }
        }
    }

    // 🔹 外部から呼び出して動作開始する関数を追加
    public void StartMoving()
    {
        isActive = true;  // ← パズル成功で動作開始！
    }
}