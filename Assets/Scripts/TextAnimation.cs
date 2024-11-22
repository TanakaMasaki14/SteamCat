using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour
{
    public Text uiText; // テキストコンポーネントをここにアタッチ
    public float minSize = 0f; // 最小サイズ
    public float maxSize = 0f; // 最大サイズ
    public float speed = 0f; // 拡大・縮小の速度

    private bool increasing = true;
    private bool isAnimating = true; // アニメーションのON/OFFを制御するフラグ

    void Update()
    {
        // スペースキーが押されたらアニメーションの状態を切り替える
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAnimating = !isAnimating; // ON/OFFを切り替える
        }

        // アニメーションが有効でない場合は何もしない
        if (!isAnimating || uiText == null)
        {
            return;
        }

        // サイズが最大に達したら縮小に切り替える
        if (uiText.fontSize >= maxSize)
        {
            increasing = false;
        }
        // サイズが最小に達したら拡大に切り替える
        else if (uiText.fontSize <= minSize)
        {
            increasing = true;
        }

        // フォントサイズを更新
        if (increasing)
        {
            uiText.fontSize += Mathf.CeilToInt(speed * Time.deltaTime);
        }
        else
        {
            uiText.fontSize -= Mathf.CeilToInt(speed * Time.deltaTime);
        }
    }
}
