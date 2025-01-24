using UnityEngine;

public class ParticlePlayback : MonoBehaviour
{
    public ParticleSystem particleSystem; // 対象のParticle System

    public float simulateTime = 5.0f; // 開始時に進めたい時間（秒）
    public bool withChildren = true; // 子パーティクルシステムも対象にするか

    void Start()
    {
        if (particleSystem != null)
        {
            // 再生せずにシミュレート状態を進める
            particleSystem.Simulate(simulateTime, withChildren, true);

            // シミュレート後に再生
            particleSystem.Play();
        }
    }
}
