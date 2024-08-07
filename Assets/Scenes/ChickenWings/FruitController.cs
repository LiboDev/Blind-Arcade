using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public float speed;

    void Start()
    {

    }

    void Update()
    {
        if (transform.position.x / speed < 1)
        {
            //haptic feedback
            Handheld.Vibrate();
            Debug.Log("JUMPPPPP");

            enabled = false;
        }
    }
}
