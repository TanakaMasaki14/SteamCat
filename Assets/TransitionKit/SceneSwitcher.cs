using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;


/// <summary>
/// To use the demo just add all three scenes to your build settings making sure the BoostrapScene is scene 0
/// </summary>
//public class SceneSwitcher : MonoBehaviour
//{
//	public Texture2D maskTexture;
//	private bool _isUiVisible = true;


//	void Awake()
//	{
//		DontDestroyOnLoad( gameObject );
//		SceneManager.LoadScene( 1 );
//	}


//	void OnGUI()
//	{
//		// hide the UI during transitions
//		if( !_isUiVisible )
//			return;

//		if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android )
//		{
//			// bigger buttons for higher res mobile devices
//			if( Screen.width >= 1500 || Screen.height >= 1500 )
//				GUI.skin.button.fixedHeight = 60;
//		}

		

//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//			var wind = new WindTransition()
//			{
//				nextScene = SceneManager.GetActiveScene().buildIndex == 1 ? 2 : 1,
//				duration = 1.0f,
//				size = 0.3f
//			};
//			TransitionKit.instance.transitionWithDelegate( wind );
//		}

//	}


//	void OnEnable()
//	{
//		TransitionKit.onScreenObscured += onScreenObscured;
//		TransitionKit.onTransitionComplete += onTransitionComplete;
//	}


//	void OnDisable()
//	{
//		// as good citizens we ALWAYS remove event handlers that we added
//		TransitionKit.onScreenObscured -= onScreenObscured;
//		TransitionKit.onTransitionComplete -= onTransitionComplete;
//	}


//	void onScreenObscured()
//	{
//		_isUiVisible = false;
//	}


//	void onTransitionComplete()
//	{
//		_isUiVisible = true;
//	}
//}
