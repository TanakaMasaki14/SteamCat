using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreate : MonoBehaviour
{
    public GameObject icePrefab; // ICEのPrefab
    public Transform spawnPoint; // ICEを生成する場所

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        // ICEがIceDestroyに触れた場合
        if (other.CompareTag("ICE"))
        {
            // ICEオブジェクトを削除
            Destroy(other.gameObject);

            // 新しいICEを生成
            Instantiate(icePrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
