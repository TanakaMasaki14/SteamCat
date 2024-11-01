using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理に必要

public class TitleScene : MonoBehaviour
{
    // シーンの名前を指定する
    public string sceneName = "";

    void Update()
    {
        // SPACEキーが押されたときに指定のシーンに移動する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
