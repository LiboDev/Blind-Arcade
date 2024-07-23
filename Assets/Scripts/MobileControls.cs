using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    public Quaternion rot = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        rot = GyroToUnity(Input.gyro.attitude);
    }

    private Quaternion GyroToUnity(Quaternion q)
    {
        var attitude = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(q.x, q.y, -q.z, -q.w);
        var eulerAngles = attitude.eulerAngles;

        eulerAngles.z = -eulerAngles.z;
        eulerAngles.x = -eulerAngles.x;

        attitude = Quaternion.Euler(eulerAngles);

        return attitude;
    }
}
