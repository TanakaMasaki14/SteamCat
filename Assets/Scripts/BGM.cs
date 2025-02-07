using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance;

    void Awake()
    {
        // すでにインスタンスが存在する場合は、新しいものを破棄
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // インスタンスを設定し、シーンをまたいでも削除されないようにする
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
