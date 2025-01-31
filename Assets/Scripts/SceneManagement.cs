using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理に必要
using System.Collections;
using Prime31.TransitionKit;

public class SceneManagement : MonoBehaviour
{
    public Texture2D maskTexture;
    private bool _isUiVisible = true;
    private int NextScene = 0;
    private int spacePressCount = 0;
    private const int requiredPressCount = 4; // 必要な押下回数
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public string TITLE = "Title"; // Inspectorで設定 0
    public string STORY = "Story"; // Inspectorで設定 1
    public string STAGE = "Stage1"; // Inspectorで設定 2


    void Update()
    {
        if (SceneManager.GetActiveScene().name == TITLE)
        {
            NextScene = 1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var wind = new WindTransition()
                {
                    nextScene = NextScene,
                    duration = 1.0f, // 演出の時間
                    size = 0.3f // 風の効果のサイズ
                };

                if (TransitionKit.instance == null)
                {
                    Debug.LogError("TransitionKit.instance が初期化されていません。シーンに TransitionKitManager が含まれているか確認してください。");
                }
                else
                {
                    TransitionKit.instance.transitionWithDelegate(wind);
                }
            }
        }

        if (SceneManager.GetActiveScene().name == STORY)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                spacePressCount++;

                if (spacePressCount >= requiredPressCount)
                {
                    NextScene = 2;
                    var wind = new WindTransition()
                    {
                        duration = 1.0f, // 演出の時間
                        size = 0.3f // 風の効果のサイズ
                    };

                    if (TransitionKit.instance == null)
                    {
                        Debug.LogError("TransitionKit.instance が初期化されていません。シーンに TransitionKitManager が含まれているか確認してください。");
                    }
                    else
                    {
                        TransitionKit.instance.transitionWithDelegate(wind);
                    }
                }  

            }
        }

    }

    void OnEnable()
    {
        TransitionKit.onScreenObscured += onScreenObscured;
        TransitionKit.onTransitionComplete += onTransitionComplete;
    }


    void OnDisable()
    {
        // as good citizens we ALWAYS remove event handlers that we added
        TransitionKit.onScreenObscured -= onScreenObscured;
        TransitionKit.onTransitionComplete -= onTransitionComplete;
    }


    void onScreenObscured()
    {
        _isUiVisible = false;
    }


    void onTransitionComplete()
    {
        _isUiVisible = true;
    }
}
