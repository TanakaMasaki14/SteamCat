using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchableBlock : MonoBehaviour
{
    public float stretchFactor = 0.5f; // 伸び縮みする強度
    public float recoverySpeed = 2f; // 元に戻る速度
    private Vector3 originalScale; // 元のスケール
    private Rigidbody rb;

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
        // 常に伸び縮みするスケールを適用
        Vector3 stretchScale = originalScale;
        float newScaleY = originalScale.y + Mathf.PingPong(Time.time * recoverySpeed,stretchFactor);
        stretchScale.y = newScaleY;
        transform.localScale = stretchScale;
    }
}
