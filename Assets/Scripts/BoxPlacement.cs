using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlacement : MonoBehaviour
{
    public string correctBoxName; // 正しい箱の名前
    public GameObject placedBox = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box") && placedBox == null)
        {
            placedBox = other.gameObject;
            Debug.Log(gameObject.name + " に " + placedBox.name + " を配置");
            CheckCorrect();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == placedBox)
        {
            placedBox = null;
        }
    }

    void CheckCorrect()
    {
        if (placedBox.name == correctBoxName)
        {
            Debug.Log("正解の箱が配置されました！");
            // クリア演出を追加できる
        }
    }
}
