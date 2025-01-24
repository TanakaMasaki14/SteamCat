using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    // �v���C���[��Transform
    public Vector3 offset;      // �v���C���[�Ƃ̋����i�I�t�Z�b�g�j
    public float smoothSpeed = 0.125f;  // �J�����̒ǔ��̃X���[�Y��

    void LateUpdate()
    {
        // �v���C���[�̈ʒu�ɃI�t�Z�b�g���������ڕW�ʒu���v�Z
        Vector3 desiredPosition = player.position + offset;

        // �X���[�Y�ɃJ������ǔ�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // �v���C���[�𒍎�����
        transform.LookAt(player.position + Vector3.up * 2.0f);  // �����I�t�Z�b�g��2.0�ɐݒ�i�����j
    }
}
