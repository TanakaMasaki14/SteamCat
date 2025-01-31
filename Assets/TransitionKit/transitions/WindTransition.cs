using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;


namespace Prime31.TransitionKit
{
	public class WindTransition : TransitionKitDelegate
	{
		/// <summary>
		/// if true, the CurvedWind shader will be used which has the wind come from the top-left to bottom-left then across to the right.
		/// </summary>
		public bool useCurvedWind = false;
		public float duration = 0.5f;
		public int nextScene = -1;
		/// <summary>
		/// how much of the screen horizontally should the transition encompass? Higher numbers mean a wider transition.
		/// </summary>
		public float size = 0.3f;
		/// <summary>
		/// how many vertical sections of "wind" should we use? Higher numbers mean more whispy wind.
		/// </summary>
		public float windVerticalSegments = 100.0f;


		#region TransitionKitDelegate implementation

		public Shader shaderForTransition()
		{
			return useCurvedWind ? Shader.Find( "prime[31]/Transitions/CurvedWind" ) : Shader.Find( "prime[31]/Transitions/Wind" );
		}


		public Mesh meshForDisplay()
		{
			return null;
		}


		public Texture2D textureForDisplay()
		{
			return null;
		}


		public IEnumerator onScreenObscured( TransitionKit transitionKit )
		{
            //transitionKit.transitionKitCamera.clearFlags = CameraClearFlags.Nothing;

            //// set some material properties
            //transitionKit.material.SetFloat( "_Size", size );
            //transitionKit.material.SetFloat( "_WindVerticalSegments", windVerticalSegments );

            //// we dont transition back to the new scene unless it is loaded
            //if( nextScene >= 0 )
            //{
            //	SceneManager.LoadSceneAsync( nextScene );
            //	yield return transitionKit.StartCoroutine( transitionKit.waitForLevelToLoad( nextScene ) );
            //}

            //yield return transitionKit.StartCoroutine( transitionKit.tickProgressPropertyInMaterial( duration ) );
            Debug.Log("onScreenObscured が開始されました");

            transitionKit.transitionKitCamera.clearFlags = CameraClearFlags.Nothing;

            transitionKit.material.SetFloat("_Size", size);
            transitionKit.material.SetFloat("_WindVerticalSegments", windVerticalSegments);

            if (nextScene >= 0)
            {
                Debug.Log($"次のシーンのインデックス: {nextScene}");
                var asyncOperation = SceneManager.LoadSceneAsync(nextScene);
                if (asyncOperation == null)
                {
                    Debug.LogError($"シーンの非同期ロードに失敗しました。nextScene: {nextScene}");
                }
                else
                {
                    Debug.Log("シーンの非同期ロードを開始しました...");

                    asyncOperation.allowSceneActivation = true;

                    while (!asyncOperation.isDone)
                    {
                        Debug.Log($"ロード進行中: {asyncOperation.progress} (allowSceneActivation: {asyncOperation.allowSceneActivation}, isDone: {asyncOperation.isDone})");
                        yield return null;
                    }
                    Debug.Log("シーンの非同期ロードが完了しました。");
                }

                Debug.Log("waitForLevelToLoad を開始します");
                yield return transitionKit.StartCoroutine(transitionKit.waitForLevelToLoad(nextScene));
                Debug.Log("waitForLevelToLoad が完了しました");
            }

            Debug.Log("tickProgressPropertyInMaterial を呼び出します");
            yield return transitionKit.StartCoroutine(transitionKit.tickProgressPropertyInMaterial(duration));

            Debug.Log("onScreenObscured が終了しました");
        }

		#endregion

	}
}