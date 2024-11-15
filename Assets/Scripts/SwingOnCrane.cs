using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingOnCrane : MonoBehaviour
{
    public Transform anchorPoint;// クレーン先端にあるAnchorPoint
    public float swingDistance = 3.0f;// ぶら下がり可能な距離
    public float springForce = 10f;// バネの強さ
    public float damper = 5f;// ダンパー
    private SpringJoint springJoint;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToAnchor = Vector3.Distance(transform.position, anchorPoint.position);
       

        // ぶら下がる動作のトリガー
        if (Input.GetKeyDown(KeyCode.E) && distanceToAnchor < swingDistance) 
        {
            AttachToAnchorPoint();
        }
        // 離れる動作のトリガー
        if(Input.GetKeyDown(KeyCode.E) && springJoint != null)
        {
            DetachFromAnchorPoint();
        }
    }

    void AttachToAnchorPoint()
    {
        if(springJoint == null)
        {
            springJoint = gameObject.AddComponent<SpringJoint>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.connectedAnchor = anchorPoint.position;
            springJoint.spring = springForce;
            springJoint.damper = damper;
        }
    }

    void DetachFromAnchorPoint()
    {
        if(springJoint != null) 
        {
            Destroy(springJoint);// SpringJointを削除
            springJoint=null;
        }
    }
}
