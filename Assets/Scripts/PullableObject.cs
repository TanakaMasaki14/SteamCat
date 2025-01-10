using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullableObject : MonoBehaviour
{
    public Transform player;// プレイヤーのTransform指定
    public float pullDistance = 2f;// 引っ張る距離
    public KeyCode pullKey = KeyCode.E;// アクションキー
    public float pullSpeed = 2f;// 引っ張る速度
    //public WallController correspondingwall;// 上昇させる壁のコントローラー
    public Transform targetPosition;// 引っ張るべき目標地点
    private bool isPulling = false;// 現在引っ張っているか
    private Rigidbody rb;
    private bool wallTriggered = false;// 壁が既に上昇したか
    private bool reachedTarget = false;// 目標地点に到達したか

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (reachedTarget) return;// 目標地点に到達したら何もしない

        float distance = Vector3.Distance(player.position, transform.position);
        float moveInput = Input.GetAxis("Horizontal");  // 移動方向の入力

        if (distance <= pullDistance && Input.GetKey(pullKey) && moveInput > 0)
        {
            isPulling = true;
        }
        else
        {
            isPulling = false;
        }

        // オブジェクトが指定された位置に到達し、壁がまだ上昇していない場合
        if (!wallTriggered && Vector3.Distance(transform.position, targetPosition.position) < 1.0f)
        {
            //correspondingwall.StartRising();// 壁を上昇させる
            wallTriggered = true;// 壁がすでに上昇した
            isPulling = false;// 引っ張り動作を停止
            reachedTarget = true;// 目標地点に到達したことを記録
            rb.velocity = Vector3.zero;// オブジェクトの速度をゼロにする
            rb.isKinematic = true;// オブジェクトを物理エンジンの影響を受けないようにする
        }


    }

    private void FixedUpdate()
    {
        if (isPulling)
        {
            Vector3 pullDirection = player.position - transform.position;
            pullDirection.y = 0;
            rb.velocity = pullDirection.normalized * pullSpeed; // 速度を直接設定
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0); // 引っ張らないときは停止
        }
    }
}