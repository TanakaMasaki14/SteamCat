using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCollisionHandler : MonoBehaviour
{
    public GameObject stretchCube;//StretchCubeの参照
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
        // 接触したオブジェクトが"InvizibleCollider"のタグを持っているのか確認
        if(collision.gameObject.CompareTag("Invizible"))
        {
            // stretchCubeのスクリプトを取得して実行
            StretchableBlock stretchableBlock = stretchCube.GetComponent<StretchableBlock>();
            if(stretchableBlock != null)
            {
                stretchableBlock.StartStretching();// 伸び縮みを開始
            }
        }
    }
}
