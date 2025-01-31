using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public WallController wallController; // 壁コントローラー
    float bottomY = -0.01f;// 押下時の位置
    float speed = 0.5f;// スイッチの移動速度
    Vector3 initialPosition;// 元の位置を保存
    bool active;// スイッチが押下中かどうか判定

    // Start is called before the first frame update
    void Start()
    {
        // スイッチ初期位置記録
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        // 押されている時は下に移動
        if (active && transform.position.y > bottomY)
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }
        // 押されていない時は元の位置に戻る
        else if(!active && transform.position.y < initialPosition.y)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;

            if(transform.position.y > initialPosition.y)
            {
                transform.position = initialPosition;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが乗った時に押す
        if(!active && other.CompareTag("Player")||other.CompareTag("Pullable"))
        {
            active = true;
            wallController.StartRising();// 壁の上昇
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // プレイヤーが離れた時に戻す
        if (active && other.CompareTag("Player")|| other.CompareTag("Pullable"))
        {
            active = false;
            wallController.StartLowering(); // 壁を下げる
        }
    }
}
