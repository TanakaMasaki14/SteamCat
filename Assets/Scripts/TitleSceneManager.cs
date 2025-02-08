using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    void Update()
    {
        // SPACEキーが押されたらStoryシーンへ遷移
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Story");
        }
    }
}
