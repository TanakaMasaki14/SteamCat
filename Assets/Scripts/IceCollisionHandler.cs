using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class IceCollisionHandler : MonoBehaviour
    {
        public GameObject movingCube; // MovingCube（旧StretchCube）の参照


        public float delay = 2f; // 遅延時間（秒）

        void Start()
        {
            // 必要なら初期化処理を追加
        }

        void Update()
        {
            // 必要なら更新処理を追加
        }

        private void OnCollisionEnter(Collision collision)
        {
            // "Invizible"タグに対する処理
            if (collision.gameObject.CompareTag("Invizible"))
            {
                StretchableBlock movingBlock = movingCube.GetComponent<StretchableBlock>();
                if (movingBlock != null)
                {
                    // 遅延を加えて上下移動を開始
                    movingBlock.StartMovingWithDelay(delay);
                }
            }

        }
    }
}