using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private MobileControls mobileControls;
    private Quaternion initialRotation;

    public float x;
    public float y;
    public float z;

    // Start is called before the first frame update
    void Start()
    {
        mobileControls = GetComponent<MobileControls>();

        //initialRotation = mobileControls.rot;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = mobileControls.rot;
    }
}
