using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // 回転速度を指定する変数（デフォルトでは1）
    public float rotationSpeed = 1.0f;

    // 毎フレーム呼び出されるメソッド
    void Update()
    {
        // X軸方向に回転させる
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }
}
