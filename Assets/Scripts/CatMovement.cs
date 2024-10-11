using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 0f;   // 移動速度
    public float jumpForce = 0f;   // ジャンプ力
    private bool isJumping = false; // ジャンプしているかどうか
    private Rigidbody rb;           // Rigidbodyコンポーネント

    // オブジェクトを持ち上げるための設定
    public Transform holdPoint;   // オブジェクトを持つ位置
    private GameObject pickedObject;  // 持ち上げたオブジェクト

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbodyを取得
    }

    void Update()
    {
        // プレイヤーのZ軸移動 (AとDキーまたは左・右矢印キー)
        float moveInput = Input.GetAxis("Horizontal");  // "A"と"D"または左・右キーで移動
        Vector3 move = new Vector3(rb.velocity.x, rb.velocity.y, moveInput * moveSpeed);  // Z軸に移動

        rb.velocity = move;

        // ジャンプ処理
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true;
        }

        // オブジェクトを持ち上げる・放す処理
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickedObject == null) // 何も持っていない場合
            {
                RaycastHit hit;
                // プレイヤーの前方にあるオブジェクトをRaycastで検出
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
                {
                    if (hit.collider.CompareTag("Pickupable"))
                    {
                        PickupObject(hit.collider.gameObject);
                    }
                }
            }
            else // すでに持っている場合は放す
            {
                DropObject();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if (collision.gameObject.CompareTag("Pickupable"))
        {
            isJumping = false;
        }
    }

    // オブジェクトを持ち上げる処理
    private void PickupObject(GameObject obj)
    {
        pickedObject = obj;

        // Rigidbodyの設定変更
        Rigidbody objRb = pickedObject.GetComponent<Rigidbody>();
        if (objRb != null)
        {
            objRb.isKinematic = true; // 物理演算を無効にして持ち上げられるようにする
        }

        // オブジェクトをプレイヤーの指定の位置に持ってくる
        pickedObject.transform.position = holdPoint.position;
        pickedObject.transform.parent = holdPoint;
    }

    // オブジェクトを放す処理
    private void DropObject()
    {
        if (pickedObject != null)
        {
            Rigidbody objRb = pickedObject.GetComponent<Rigidbody>();
            if (objRb != null)
            {
                objRb.isKinematic = false; // 物理演算を再度有効にする
            }

            // 親オブジェクトから外す
            pickedObject.transform.parent = null;
            pickedObject = null;
        }
    }
}
