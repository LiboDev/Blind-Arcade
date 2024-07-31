using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public float speed;

    void Update()
    {
        if (transform.position.x / speed < 1)
        {
            //haptic feedback
            Debug.Log("JUMPPPPP");
            HapticFeedbackController.HapticFeedback();
            Handheld.Vibrate();

            enabled = false;
        }
    }
}
