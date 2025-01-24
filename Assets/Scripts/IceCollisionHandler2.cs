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

        // "Invizible2"タグに対する処理（扉の動作）
        if (collision.gameObject.CompareTag("Invizible2"))
        {
            DoorController door1Controller = door1.GetComponent<DoorController>();
            DoorController door2Controller = door2.GetComponent<DoorController>();

            if (door1Controller != null && door2Controller != null)
            {
                // Door1とDoor2を開く動作を開始
                door1Controller.OpenDoor();
                door2Controller.OpenDoor();
            }
        }
    }
 }
