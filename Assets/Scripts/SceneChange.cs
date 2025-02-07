using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // 衝突を検出する
    private void OnCollisionEnter(Collision collision)
    {
        // 他のオブジェクトと衝突した場合、シーンを切り替える
        if (collision.gameObject.CompareTag("Player")) // 例えばプレイヤーと衝突した場合
        {
            // シーンを切り替える
            SceneManager.LoadScene("Clear");
        }
    }
}