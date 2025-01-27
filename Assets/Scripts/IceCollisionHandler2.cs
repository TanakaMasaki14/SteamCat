using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCollisionHandler2 : MonoBehaviour
{
    public GameObject door1; // Door1オブジェクトの参照
    public GameObject door2; // Door2オブジェクトの参照
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        // "Invizible2"タグとの衝突を検知
        if (collision.gameObject.CompareTag("Invizible2"))
        {
            Debug.Log("Invizible2に衝突しました！");

            // Door1とDoor2のスクリプトを取得して動作を開始
            DoorController door1Controller = door1.GetComponent<DoorController>();
            DoorController door2Controller = door2.GetComponent<DoorController>();

            if (door1Controller != null)
            {
                door1Controller.StartLoop(); // 永続的な動作を開始
            }

            if (door2Controller != null)
            {
                door2Controller.StartLoop(); // 永続的な動作を開始
            }
        }
    }
 }
