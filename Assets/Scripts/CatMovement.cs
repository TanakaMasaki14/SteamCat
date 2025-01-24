using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 0f;   // 移動速度
    public float jumpForce = 0f;   // ジャンプ力
    private bool isJumping = false; // ジャンプ中かどうか
    private Rigidbody rb;           // Rigidbodyコンポーネント
    private bool isFacingRight = true; // 現在右向きかどうか

    // 鳴き声用
    public float meowRadius = 2f; // 鳴き声の影響範囲の半径
    public LayerMask meowLayerMask; // 鳴き声が影響するオブジェクトのレイヤー

    // オブジェクトを持ち上げるための設定
    public Transform holdPoint;   // オブジェクトを持つ位置
    private GameObject pickedObject;  // 現在持っているオブジェクト

    private Animator animator; // Animatorコンポーネント

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbodyの取得

        // Animatorコンポーネントを取得
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // プレイヤー入力による移動 (AとDキーまたは←→キー)
        float moveInput = Input.GetAxis("Horizontal");  // "A"や"D"キーで移動
        Vector3 move = new Vector3(rb.velocity.x, rb.velocity.y, moveInput * moveSpeed);  // Z軸方向に移動

        rb.velocity = move;

        // 向きの変更を追加
        if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
        else if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }

        // スペースキーが押されたら遅延ジャンプを実行
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(DelayedJump());
        }

        // 鳴き声
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Meow();
        }

        // オブジェクトを持つ・離す
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
            else // すでに持っている場合は離す
            {
                DropObject();
            }
        }

        // アニメーションの切り替え
        if (moveInput != 0)
        {
            // Bool型のパラメータであるWalkをTrueにする
            animator.SetBool("Walk", true);
        }
        else
        {
            // Bool型のパラメータであるWalkをFalseにする
            animator.SetBool("Walk", false);
        }
    }

    // 0.5秒後にジャンプするコルーチン
    private IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(0.2f); // 0.2秒待つ

        if (!isJumping) // まだジャンプしていない場合のみ実行
        {
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true;

            // Bool型のAnimatorであるJampをTrueにする
            animator.SetBool("Jamp", true);
        }
    }

    private void Flip()
    {
        // 左右反転するためにY軸方向に回転
        isFacingRight = !isFacingRight;
        float rotationY = isFacingRight ? 0 : 180;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    // 鳴き声
    private void Meow()
    {
        // 鳴き声の影響範囲を計算 (SphereCastで範囲を指定)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meowRadius, meowLayerMask);
        // 範囲内のオブジェクトをハイライト
        foreach (Collider hitCollider in hitColliders)
        {
            Renderer objRenderer = hitCollider.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                // 動くオブジェクトを動かす
                MeowMove moveableObject = hitCollider.GetComponent<MeowMove>();
                if (moveableObject != null)
                {
                    moveableObject.MoveUpAndDown();
                }
                // 一定時間後に元の色に戻す
                //StartCoroutine(ResetColor(objRenderer));
            }
        }
    }

    // 色を元に戻す (コルーチン)
    private IEnumerator ResetColor(Renderer objRenderer)
    {
        yield return new WaitForSeconds(1f); // 1秒後に色を元に戻す
        objRenderer.material.color = Color.white; // 元の色に戻す
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

            // Bool型のAnimatorであるJampをFalseにする
            animator.SetBool("Jamp", false);
        }

        if (collision.gameObject.CompareTag("Pickupable"))
        {
            isJumping = false;
        }
    }

    // オブジェクトを持ち上げる
    private void PickupObject(GameObject obj)
    {
        pickedObject = obj;

        // Rigidbodyの設定変更
        Rigidbody objRb = pickedObject.GetComponent<Rigidbody>();
        if (objRb != null)
        {
            objRb.isKinematic = true; // 物理演算を無効化して持ち上げる
        }

        // オブジェクトをプレイヤーの指定位置に配置
        pickedObject.transform.position = holdPoint.position;
        pickedObject.transform.parent = holdPoint;
    }

    // オブジェクトを離す
    private void DropObject()
    {
        if (pickedObject != null)
        {
            Rigidbody objRb = pickedObject.GetComponent<Rigidbody>();
            if (objRb != null)
            {
                objRb.isKinematic = false; // 物理演算を再度有効化
            }

            // オブジェクトをリリース
            pickedObject.transform.parent = null;
            pickedObject = null;
        }
    }

    // Gizmosで鳴き声の範囲を可視化
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meowRadius);
    }
}
