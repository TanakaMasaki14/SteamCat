using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM: MonoBehaviour
{
    public AudioSource bgmSource;
    public string titleSceneName = "Title";

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == titleSceneName)
        {
            RestartBGM();
        }
    }

    void RestartBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
            bgmSource.Play();
        }
    }
}
