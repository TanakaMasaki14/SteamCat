using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchableBlock : MonoBehaviour
{
    public float stretchFactor = 0.5f; // �L�яk�݂��鋭�x
    public float recoverySpeed = 2f; // ���ɖ߂鑬�x
    private Vector3 originalScale; // ���̃X�P�[��
    private Rigidbody rb;
    private bool isStretching = false;// �L�яk�݂�L���ɂ���t���O
    public float moveHeight = 0.5f; // �㉺�ړ��̍���
    public float moveSpeed = 2f; // �㉺�ړ��̑��x
    private Vector3 originalPosition; // ���̈ʒu
    private bool isMoving = false; // �㉺�ړ���L���ɂ���t���O
    private float startTime; // �ړ��J�n���̊����

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;// �����X�P�[�����L�^
        rb = GetComponent<Rigidbody>();

        if (rb != null )
        {
            rb.isKinematic = true;
        }  
        // �����ʒu���L�^
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isStretching)
        {
            // ��ɐL�яk�݂���X�P�[����K�p
            Vector3 stretchScale = originalScale;
            float newScaleY = originalScale.y + Mathf.PingPong(Time.time * recoverySpeed, stretchFactor);
            stretchScale.y = newScaleY;
            transform.localScale = stretchScale;
        }
    }

    // �L�яk�݂��J�n
    public void StartStretching()
    {
        isStretching = true;
    }

    // �L�яk�݂��~
    public void StopStretching()
    {
        isStretching = false;
        transform.localScale = originalScale;// ���̃X�P�[���ɖ߂�


        if (isMoving)
        {
            // �㉺�ړ�����ʒu���v�Z�i���Ԃ̊�����Z�b�g�j
            Vector3 newPosition = originalPosition;
            float elapsedTime = Time.time - startTime; // �J�n���Ԃ���ɂ����o�ߎ���
            newPosition.y = 0 + Mathf.PingPong(elapsedTime * moveSpeed, moveHeight); // y=0����Ɉړ�
            transform.position = newPosition;
        }
    }
    

    // �㉺�ړ����J�n�i�x������j
    public void StartMovingWithDelay(float delay)
    {
        StartCoroutine(StartMovingAfterDelay(delay));
    }

    // �㉺�ړ����~
    public void StopMoving()
    {
        isMoving = false;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z); // y=0�Ƀ��Z�b�g
    }

    // �R���[�`���Œx��������
    private IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �w��b���ҋ@

        // �ړ��J�n���̊���Ԃ��L�^
        startTime = Time.time;

        // �㉺�ړ����J�n
        isMoving = true;
    }
}