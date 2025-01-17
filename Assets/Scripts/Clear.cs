using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理のための名前空間を追加

public class Clear : MonoBehaviour
{
    // ボタンがクリックされたときに呼ばれるメソッド
    public void GoToTitle()
    {
        // "Title"というシーンに戻る
        SceneManager.LoadScene("Title");
    }
}
