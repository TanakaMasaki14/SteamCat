using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    public float moveSpeed = 0f;   // �ړ����x
    public float jumpForce = 0f;   // �W�����v��
    private bool isJumping = false; // �W�����v���Ă��邩�ǂ���
    private Rigidbody rb;           // Rigidbody�R���|�[�l���g
    private bool isFacingRight = true; // ���݉E�������ǂ���

    // ������p
    public float meowRadius = 2f; // �����̓����蔻��͈̔�
    public LayerMask meowLayerMask; // �������e����^���郌�C���[

    // �I�u�W�F�N�g�������グ�邽�߂̐ݒ�
    public Transform holdPoint;   // �I�u�W�F�N�g�����ʒu
    private GameObject pickedObject;  // �����グ���I�u�W�F�N�g

    private Animator animator; // Animatorをanimatorという変数で定義する

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbody���擾
        
        // 変数walkAnimeに、Animetorコンポーネントを設定する
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �v���C���[��Z���ړ� (A��D�L�[�܂��͍��E�E���L�[)
        float moveInput = Input.GetAxis("Horizontal");  // "A"��"D"�܂��͍��E�E�L�[�ňړ�
        Vector3 move = new Vector3(rb.velocity.x, rb.velocity.y, moveInput * moveSpeed);  // Z���Ɉړ�

        rb.velocity = move;

        // ��]�̏�����ǉ�
        if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
        else if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }

        // �W�����v����
        // �W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = Vector3.up * jumpForce;
            isJumping = true;

            // Bool型のAnimatorであるJampをTrueにする
            animator.SetBool("Jamp", true);
        }

        // ������
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Meow();
        }

        // �I�u�W�F�N�g�������グ��E��������
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickedObject == null) // ���������Ă��Ȃ��ꍇ
            {
                RaycastHit hit;
                // �v���C���[�̑O���ɂ���I�u�W�F�N�g��Raycast�Ō��o
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
                {
                    if (hit.collider.CompareTag("Pickupable"))
                    {
                        PickupObject(hit.collider.gameObject);
                    }
                }
            }
            else // ���łɎ����Ă���ꍇ�͕���
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

        // 回転の処理を追加(回転で左右の向きを変更)
        if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
        else if(moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        // ���E���]���邽�߂�Y�������ɉ�]
        isFacingRight = !isFacingRight;
        float rotationY = isFacingRight ? 0 : 180;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    // ������
    private void Meow()
    {
        Debug.Log("�L�������I");

        // �����̓����蔻��𔭐������� (SphereCast�Ŕ͈͂��w��)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meowRadius, meowLayerMask);
        // �͈͓��̃I�u�W�F�N�g���n�C���C�g
        foreach (Collider hitCollider in hitColliders)
        {
            Renderer objRenderer = hitCollider.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                // �����I�u�W�F�N�g�𓮂���������ǉ�
                MeowMove moveableObject = hitCollider.GetComponent<MeowMove>();
                if (moveableObject != null)
                {
                    moveableObject.MoveUpAndDown();
                }
                // �I�u�W�F�N�g�̐F��Ԃ��ύX���ăn�C���C�g
                objRenderer.material.color = Color.red;

                // ��莞�Ԍ�ɐF�����ɖ߂��R���[�`�����J�n
                StartCoroutine(ResetColor(objRenderer));
            }
        }
    }

    // �F�����ɖ߂����� (�R���[�`��)
    private IEnumerator ResetColor(Renderer objRenderer)
    {
        yield return new WaitForSeconds(1f); // 1�b��ɐF�����ɖ߂�
        objRenderer.material.color = Color.white; // ���̐F�ɖ߂�
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

            // Bool型のAnimatorであるJampをTrueにする
            animator.SetBool("Jamp", false);
        }

        if (collision.gameObject.CompareTag("Pickupable"))
        {
            isJumping = false;
        }

    }

    // �I�u�W�F�N�g�������グ�鏈��
    private void PickupObject(GameObject obj)
    {
        pickedObject = obj;

        // Rigidbody�̐ݒ�ύX
        Rigidbody objRb = pickedObject.GetComponent<Rigidbody>();
        if (objRb != null)
        {
            objRb.isKinematic = true; // �������Z�𖳌��ɂ��Ď����グ����悤�ɂ���
        }

        // �I�u�W�F�N�g���v���C���[�̎w��̈ʒu�Ɏ����Ă���
        pickedObject.transform.position = holdPoint.position;
        pickedObject.transform.parent = holdPoint;
    }

    // �I�u�W�F�N�g���������
    private void DropObject()
    {
        if (pickedObject != null)
        {
            Rigidbody objRb = pickedObject.GetComponent<Rigidbody>();
            if (objRb != null)
            {
                objRb.isKinematic = false; // �������Z���ēx�L���ɂ���
            }

            // �e�I�u�W�F�N�g����O��
            pickedObject.transform.parent = null;
            pickedObject = null;
        }
    }

    // Gizmos�Ŗ����͈̔͂����o�I�ɕ\��
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meowRadius);
    }
}