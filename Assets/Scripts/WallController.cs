using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float riseSpeed = 2f;// �ǂ̏㏸���x
    public float targetHeight = 5f;// �ǂ̍ŏI�I�ȍ���
    private bool isRising = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isRising)
        {
            // �ǂ��w�肳�ꂽ�����ɒB����܂ŏ㏸����
            if (transform.position.y < targetHeight)
            {
                transform.position += Vector3.up * riseSpeed * Time.deltaTime;
            }
        }
    }

    public void StartRising()
    {
        isRising = true;// �㏸���J�n����t���O�𗧂Ă�
    }
}
