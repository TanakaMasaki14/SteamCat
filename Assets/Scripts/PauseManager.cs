using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;  // UIのパネルをInspectorでセット

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false); // ゲーム開始時は非表示
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // パネルを表示
        Time.timeScale = 0f; // ゲームを一時停止
    }

    void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // パネルを非表示
        Time.timeScale = 1f; // ゲームを再開
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1f; // 時間を元に戻す
        SceneManager.LoadScene(0); // タイトルシーンに戻る
    }
}
