using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeSystem : MonoBehaviour
{
    //エンジンの回転数
    //数値を上げれば排気量が増える
    public float engineRevs;
    //排気量のレート
    //回転数とかけ合わせる事で量を計算する
    public float exhaustRate;
    //排気量のエミッションをコントロールする
    ParticleSystem exhaust;

    void Start()
    {
        exhaust = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var emission = exhaust.emission;
        emission.rateOverTime = engineRevs * exhaustRate;
    }
}