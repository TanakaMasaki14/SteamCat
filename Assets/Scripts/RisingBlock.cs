using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class RisingBlock : MonoBehaviour
{
    public float riseHeight = 5f;  // 上昇する高さ
    public float riseSpeed = 2f;   // 上昇するスピード
    private bool isReadyToRise = false;  // 上昇フラグ
    private Vector3 initialPosition;  // 元の位置
    private Renderer blockRenderer;   // ブロックのレンダラー

    private void Start()
    {
        initialPosition = transform.position;  // 元の位置を保存
        blockRenderer = GetComponent<Renderer>();  // Rendererを取得
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag=="Player" && isReadyToRise)
        {
            StartCoroutine(RiseAfterDelay(2f));  // 2秒後に上昇を開始
        }
    }

    public void EnableRising()
    {
        isReadyToRise = true;  // 上昇フラグをオンにする
        StartCoroutine(BlinkingEffect());  // 点滅を開始
    }

    public bool IsReadyToRise()
    {
        return isReadyToRise;
    }

    private IEnumerator RiseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // 指定した秒数待機

        Vector3 targetPosition = initialPosition + new Vector3(0, riseHeight, 0);  // 目標位置

        while (transform.position.y < targetPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // 点滅処理
    private IEnumerator BlinkingEffect()
    {
        while (isReadyToRise)  // フラグがオンの間は点滅する
        {
            blockRenderer.enabled = !blockRenderer.enabled;  // 表示/非表示を切り替える
            yield return new WaitForSeconds(0.5f);  // 0.5秒ごとに切り替え
        }

        // 最後にレンダラーを有効にしておく
        blockRenderer.enabled = true;
    }
}