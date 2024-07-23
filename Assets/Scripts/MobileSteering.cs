using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSteering : MonoBehaviour
{
    public float dirX;
    public float dirY;
    public float dirZ;

    [SerializeField] bool x;
    [SerializeField] bool y;
    [SerializeField] bool z;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;

        if (x)
        {
            StartCoroutine(TrackX());
        }
        if (y)
        {
            StartCoroutine(TrackY());
        }
        if (z)
        {
            StartCoroutine(TrackZ());
        }
    }

    private IEnumerator TrackX()
    {
        while (true)
        {
            dirX = Input.acceleration.x;
            yield return null;
        }
    }
    private IEnumerator TrackY()
    {
        while (true)
        {
            dirY = Input.acceleration.y;
            yield return null;
        }
    }
    private IEnumerator TrackZ()
    {
        while (true)
        {
            dirZ = Input.acceleration.z;
            yield return null;
        }
    }

}
