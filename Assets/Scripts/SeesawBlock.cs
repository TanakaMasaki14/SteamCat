using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class SeesawBlock : MonoBehaviour
{
    public Transform leftBlock; //　左側のオブジェクト
    public Transform rightBlock; // 右側のオブジェクト
    public float leftWeight = 1.0f; // 左側のオブジェクトの重さ
    public float rightWeight = 1.0f; // 右側のオブジェクトの重さ
    public float tiltSpeed = 2.0f; // シーソーが傾く角度

    private HingeJoint hinge;
    private Rigidbody seesawRb;

    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        seesawRb = GetComponent<Rigidbody>();

        // HingeJointの設定
        hinge.useLimits = true;
        JointLimits limits = hinge.limits;
        limits.min = 30f; // シーソーの最大傾き角度(左側)
        limits.max = 30f; // シーソーの最大傾き角度(右側)
        hinge.limits = limits;
    }

    // Update is called once per frame
    void Update()
    {
        if (leftBlock != null && rightBlock != null)
        {
            // 左右のブロックの位置からシーソーの中心までの距離を計算
            float leftDistance = leftBlock.position.y - transform.position.y; // y軸方向の距離
            float rightDistance = rightBlock.position.y - transform.position.y; // y軸方向の距離

            // 左右の距離と重さに基づいて傾く力を計算
            float leftForce = leftWeight * leftDistance;
            float rightForce = rightWeight * rightDistance;

            // 左右の力差に基づいて傾く方向を決定
            float tiltDirection = rightForce - leftForce;

            // 傾きの強さを決定（最大傾き角度までの範囲で力を適用）
            float tiltAmount = Mathf.Clamp(tiltDirection, -1f, 1f);

            // シーソーに適用するトルク
            Vector3 torque = Vector3.up * tiltAmount * tiltSpeed;

            // シーソーにトルクを適用
            seesawRb.AddTorque(torque);
        }
    }
}
