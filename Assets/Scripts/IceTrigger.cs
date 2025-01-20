using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrigger : MonoBehaviour
{
    public RisingBlock risingBlock;  // 上昇ブロックの参照
    private bool isFlagOn = false;   // フラグの初期状態はオフ

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("OnCollisionEnterが呼び出されました。");  // コリジョンイベントが呼ばれているか確認

        // Iceタグを持つオブジェクトがTriggerタグを持つオブジェクトに触れたとき
        if (other.gameObject.tag=="Trigger"&& other.gameObject.tag == "Pickupable")
        {
            Debug.Log("IceタグがTriggerタグに触れました。");  // タグの条件が一致したか確認

            if (!isFlagOn)  // フラグがオフの場合のみ処理を実行
            {
                isFlagOn = true;  // フラグをオンにする
                Debug.Log("IceTrigger: フラグがオンになりました。");

                if (risingBlock != null)
                {
                    risingBlock.EnableRising();  // 上昇ブロックのフラグをオンにする
                }
                else
                {
                    Debug.LogWarning("risingBlockが設定されていません。");
                }
            }
        }
    }

    public bool IsFlagOn()
    {
        return isFlagOn;
    }
}