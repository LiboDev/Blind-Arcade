using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

    //scene
    private MobileShake mobileShake;
    private Rigidbody rb;

    //tracking
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        mobileShake = GetComponent<MobileShake>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mobileShake.shake)
        {
            rb.velocity = new Vector3(0,Mathf.Clamp(mobileShake.magnitude,3,5) * 2,0);
        }
    }

    public void GameOver()
    {
        enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Fruit"))
        {
            Destroy(other.gameObject);
            score++;

            Debug.Log("Score: " + score);
        }
    }
}
