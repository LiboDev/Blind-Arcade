using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //scene
    [SerializeField] private Sound[] sounds;
    private AudioSource audioSource;
    private Rigidbody rb;

    

    //stats
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speed = 10f;

    //tracking

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

/*    void Update()
    {
        if(transform.position.z > 10f)
        {
            //player game over
        }
    }*/

    private void PlaySFX(string name, float variation, float volume)
    {
        Sound s = null;

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                s = sounds[i];
            }
        }

        audioSource.pitch = Random.Range(1f - variation, 1f + variation);

        if (s == null)
        {
            Debug.LogError("SoundNotFound");
        }
        else
        {
            audioSource.PlayOneShot(s.clip, volume);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(speed < maxSpeed)
            {
                //PlaySFX("Bounce", 0.01f, 0.1f);
                speed++;
            }
            //PlaySFX("HardBounce", 0.01f, 0.25f);

            float difference = transform.position.x - other.transform.position.x;
            Vector3 direction = new Vector3(difference / 2f, 0,1);
            direction.Normalize();

            rb.velocity = direction * speed;
        }
        else if (other.gameObject.tag == "Terrain")
        {
            if(speed < maxSpeed)
            {
                PlaySFX("Collide", 0.01f, 0.25f);
            }
            else
            {
                PlaySFX("Collide", 0.01f, 0.5f);
            }
        }
    }
}
