using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchableBlock : MonoBehaviour
{
    public float stretchFactor = 0.5f; // 伸び縮みする強度
    public float recoverySpeed = 2f; // 元に戻る速度
    private Vector3 originalScale; // 元のスケール
    private Rigidbody rb;
    private bool isStretching = false;// 伸び縮みを有効にするフラグ

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;// 初期スケールを記録
        rb = GetComponent<Rigidbody>();

        if (rb != null )
        {
            rb.isKinematic = true;
        }  
    }

    // Update is called once per frame
    void Update()
    {
        if(isStretching)
        {
            // 常に伸び縮みするスケールを適用
            Vector3 stretchScale = originalScale;
            float newScaleY = originalScale.y + Mathf.PingPong(Time.time * recoverySpeed, stretchFactor);
            stretchScale.y = newScaleY;
            transform.localScale = stretchScale;
        }
    }

    // 伸び縮みを開始
    public void StartStretching()
    {
        isStretching = true;
    }

    // 伸び縮みを停止
    public void StopStretching()
    {
        isStretching = false;
        transform.localScale = originalScale;// 元のスケールに戻す
    }
}
