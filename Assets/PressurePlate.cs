using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject attachedObject; // 感圧板にアタッチされているオブジェクト
    public float pressDepth = 0.1f; // 感圧板が押し込まれる深さ (Y軸の移動量)
    public float pressSpeed = 5f; // 感圧板が押される速度
    public bool isPressed = false; // 感圧板が押されたかどうかのフラグ

    private Vector3 originalPosition; // 感圧板の元の位置
    private Vector3 pressedPosition; // 感圧板が押されたときの位置

    void Start()
    {
        // 感圧板の初期位置を記録
        originalPosition = attachedObject.transform.localPosition;
        // 感圧板が押されたときの目標位置を計算
        pressedPosition = originalPosition - new Vector3(0, pressDepth, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        // プレイヤーが感圧板に乗ったかどうかをチェック
        if (other.CompareTag("Player"))
        {
            isPressed = true; // フラグを立てる
        }
    }

    void OnTriggerExit(Collider other)
    {
        // プレイヤーが感圧板から離れた場合
        if (other.CompareTag("Player"))
        {
            isPressed = false; // フラグをリセット
        }
    }

    void Update()
    {
        // 感圧板が押されたかどうかで位置を調整
        if (isPressed)
        {
            // 感圧板を押された位置まで移動
            attachedObject.transform.localPosition = Vector3.Lerp(attachedObject.transform.localPosition, pressedPosition, pressSpeed * Time.deltaTime);
        }
        else
        {
            // 元の位置に戻す
            attachedObject.transform.localPosition = Vector3.Lerp(attachedObject.transform.localPosition, originalPosition, pressSpeed * Time.deltaTime);
        }
    }
}
