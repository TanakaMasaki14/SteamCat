using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float riseSpeed = 2f; // 壁の上昇速度
    public float loweringSpeed = 2f; // 壁の下降速度
    public float targetHeight = 5f; // 壁の最大の高さ
    public float minHeight = 0f; // 壁の最小の高さ
    private bool isRising = false;
    private bool isLowering = false;

    void Update()
    {
        if (isRising)
        {
            // 壁が指定された高さに達するまで上昇する
            if (transform.position.y < targetHeight)
            {
                transform.position += Vector3.up * riseSpeed * Time.deltaTime;
            }
            else
            {
                // 上昇し過ぎた場合に位置を固定
                transform.position = new Vector3(transform.position.x, targetHeight, transform.position.z);
                isRising = false; // 上昇を停止
            }
        }
        else if (isLowering)
        {
            // 壁が最小の高さまで下降する
            if (transform.position.y > minHeight)
            {
                transform.position -= Vector3.up * loweringSpeed * Time.deltaTime;
            }
            else
            {
                // 下降し過ぎた場合に位置を固定
                transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
                isLowering = false; // 下降を停止
            }
        }
    }

    public void StartRising()
    {
        isRising = true; // 上昇を開始するフラグを立てる
        isLowering = false; // 下降を停止する
    }

    public void StartLowering()
    {
        isLowering = true; // 下降を開始するフラグを立てる
        isRising = false; // 上昇を停止する
    }
}