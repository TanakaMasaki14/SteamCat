using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2.0f;
    private bool movingToB = true;

    // プレイヤーを乗せるために、トリガーコライダーで接触を検知する
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player landed on platform"); // ログ追加
            collision.transform.SetParent(transform); // プレイヤーを床の子に設定
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player left platform"); // ログ追加
            collision.transform.SetParent(null); // プレイヤーを親子関係から外す
        }
    }

    void Update()
    {
        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);

            if (transform.position == pointB)
            {
                movingToB = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);

            if (transform.position == pointA)
            {
                movingToB = true;
            }
        }
    }
}