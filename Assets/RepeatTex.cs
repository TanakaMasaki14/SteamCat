using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatTex : MonoBehaviour
{
    // スクロール速度
    public float scrollSpeedX = 0.1f; // X方向
    public float scrollSpeedY = 0.1f; // Y方向

    // マテリアルの参照
    private Material material;

    void Start()
    {
        // Cubeのマテリアルを取得
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // 現在のテクスチャオフセットを計算
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;

        // マテリアルのオフセットを更新
        material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
