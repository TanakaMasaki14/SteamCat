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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // プレイヤーを床の子に設定
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null); // プレイヤーを親子関係から外す
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
