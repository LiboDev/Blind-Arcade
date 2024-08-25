using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public float speed;

    private BatController batController;

    void Start()
    {
        batController = GameObject.Find("Player").GetComponent<BatController>();
    }

    void Update()
    {
        if (transform.position.x / speed < 1)
        {
            //haptic feedback

            batController.Vibrate();

            enabled = false;
        }
    }
}
