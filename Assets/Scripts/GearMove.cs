using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMove : MonoBehaviour
{
    // ‰ñ“]‘¬“x‚ğ’²®‚·‚é‚½‚ß‚Ì•Ï”
    public float rotationSpeed = 50.0f;

    void Update()
    {
        // X²‚ğ’†S‚É‰ñ“]‚³‚¹‚é
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}