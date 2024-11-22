using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light pointLight; // 点滅させるライト
    public float minIntensity = 0f; // 最小輝度
    public float maxIntensity = 0f; // 最大輝度
    public float flickerSpeed = 0f; // 点滅の速さ
    public bool enableRandomFlicker = true; // ランダム点滅を有効にするか

    private float flickerTimer = 0f;

    void Start()
    {
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
        }
    }

    void Update()
    {
        // ランダムな点滅
        if (enableRandomFlicker)
        {
            pointLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
        else
        {
            // 規則的な点滅
            flickerTimer += Time.deltaTime;
            pointLight.intensity = Mathf.PingPong(flickerTimer * flickerSpeed, maxIntensity - minIntensity) + minIntensity;
        }
    }
}
