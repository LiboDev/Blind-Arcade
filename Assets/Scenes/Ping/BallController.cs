using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //scene
    private AudioSource audioSource;
    private Rigidbody rb;

    //stats
    private float speed = 5f;

    //tracking

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        rb.velocity = -transform.forward * speed;
    }

    void Update()
    {
        if(transform.position.z < -10f)
        {
            //player game over
            //gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            speed++;
            if(speed > 25)
            {
                speed = 25;
            }

            float difference = transform.position.x - other.transform.position.x;
            Vector3 direction = new Vector3(difference / 2f, 0,1);
            direction.Normalize();

            rb.velocity = direction * speed;
        }
        else if (other.gameObject.tag == "Terrain")
        {
            audioSource.Play();
        }
    }
}
