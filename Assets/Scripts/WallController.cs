using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float riseSpeed = 2f;// 壁の上昇速度
    public float targetHeight = 5f;// 壁の最終的な高さ
    private bool isRising = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRising)
        {
            // 壁が指定された高さに達するまで上昇する
            if (transform.position.y < targetHeight)
            {
                transform.position += Vector3.up * riseSpeed * Time.deltaTime;
            }
        }
    }

    public void StartRising()
    {
        isRising = true;// 上昇を開始するフラグを立てる
    }
}
