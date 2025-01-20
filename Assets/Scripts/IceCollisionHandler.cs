using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCollisionHandler : MonoBehaviour
{
    public GameObject movingCube; // MovingCube（旧StretchCube）の参照
    public float delay = 2f; // 遅延時間（秒）

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // 接触したオブジェクトが"Invizible"のタグを持っているのか確認
        if (collision.gameObject.CompareTag("Invizible"))
        {
            // MovingBlock スクリプトを取得して実行
            StretchableBlock movingBlock = movingCube.GetComponent<StretchableBlock>();
            if (movingBlock != null)
            {
                // 遅延を加えて上下移動を開始
                movingBlock.StartMovingWithDelay(delay);
            }
        }
    }
}