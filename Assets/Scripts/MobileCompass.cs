using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileCompass : MonoBehaviour
{
    public float rot;

    // Start is called before the first frame update
    void Awake()
    {
        Input.compass.enabled = true;
        rot = Input.compass.trueHeading;
    }

    // Update is called once per frame
    void Update()
    {
        rot = Input.compass.trueHeading;
    }
}
