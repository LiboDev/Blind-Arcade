using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileShake : MonoBehaviour
{
    public float magnitude;

    public bool shake = false;
    public bool shook = false;

    private bool canShake = true;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        magnitude = Input.acceleration.magnitude;

        if (magnitude > 2 && canShake)
        {
            shake = true;
            canShake = false;
            StartCoroutine(Shake());
        }
        else
        {
            shook = true;
            StartCoroutine(Shook());
        }
    }

    private IEnumerator Shake()
    {
        yield return new WaitForSeconds(0.03f);
        shake = false;
    }

    private IEnumerator Shook()
    {
        yield return null;
        shook = false;

        yield return new WaitForSeconds(0.1f);
        canShake = true;
    }
}
