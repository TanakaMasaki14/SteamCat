using UnityEngine;

public class MeowSound : MonoBehaviour
{
    public AudioClip soundEffect; // 再生するSEを設定
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource コンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Qキーが押されたときに音を再生
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlaySoundEffect();
        }
    }

    void PlaySoundEffect()
    {
        if (soundEffect != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }
}
