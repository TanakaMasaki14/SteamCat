using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理に必要
using System.Collections;

public class TitleScene : MonoBehaviour
{
    // シーンの名前を指定する
    public string sceneName = "";

    void Update()
    {
        // SPACEキーが押されたときに1.5秒後に指定のシーンに移動する
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadSceneAfterDelay(0.7f));
        }
    }

    // シーンを遅延してロードするコルーチン
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}