using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理を使用するために追加

public class Story : MonoBehaviour
{
    public Transform objectToMove; // Inspectorで設定する
    public Vector3 moveOffset = new Vector3(0, 0, 0); // 移動量
    private int pressCount = 0; // スペースキー押下回数
   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (objectToMove != null)
            {
                objectToMove.position += moveOffset; // オブジェクトを移動
                pressCount++; // 押下回数を増やす

                if (pressCount >= 4) // 4回押されたらシーン遷移
                {
                    SceneManager.LoadScene("Stage1"); // シーン名を変更
                }
            }
        }
    }
}
