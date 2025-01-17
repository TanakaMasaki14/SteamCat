using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理に必要
using System.Collections;
using Prime31.TransitionKit;

public class TitleScene : MonoBehaviour
{
    public string sceneName = "Story"; // Inspectorで設定

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(TransitionWithWind(0.7f)); // 遷移を開始
        }
    }

    IEnumerator TransitionWithWind(float delay)
    {
        // WindTransition の設定
        var wind = new WindTransition()
        {
            nextScene = sceneName == "Story" ? (SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1) : SceneManager.GetSceneByName(sceneName).buildIndex,
            duration = 1.0f, // 演出の時間
            size = 0.3f // 風の効果のサイズ
        };

        TransitionKit.instance.transitionWithDelegate(wind); // WindTransition を実行

        yield return new WaitForSeconds(delay); // 指定時間だけ待機
        SceneManager.LoadScene(wind.nextScene); // 次のシーンに移動
    }
}
