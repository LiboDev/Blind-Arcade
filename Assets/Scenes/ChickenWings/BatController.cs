using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{

    //scene
    private MobileShake mobileShake;
    private Rigidbody rb;

    [SerializeField] private Sound[] sounds;
    private AudioSource audioSource;

    //tracking
    private bool canJump = true;
    public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        mobileShake = GetComponent<MobileShake>();
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float magnitude = mobileShake.magnitude;

        if (mobileShake.shake)
        {
            if (canJump)
            {
                canJump = false;
                if(magnitude < 4)
                {
                    PlaySFX("Sjump", 0.05f);
                }
                else
                {
                    PlaySFX("Ljump", 0.05f);
                }
            
                rb.velocity = new Vector3(0,Mathf.Clamp(magnitude, 3,5) * 2,0);
            }

            if (magnitude<1)
            {
                rb.velocity -= new Vector3(0, 10, 0) * Time.deltaTime;
            }
            else
            {
                rb.velocity += new Vector3(0, magnitude*2, 0) * Time.deltaTime;
            }
        
        }
    }

    public void GameOver()
    {
        PlaySFX("Lose", 0.05f);
        Debug.Log("GAME OVER\nSCORE: " + score);
        enabled = false;
    }


    private void PlaySFX(string name, float variation)
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
            audioSource.PlayOneShot(s.clip);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Floor") && canJump == false)
        {
            PlaySFX("land", 0.05f);
            canJump = true;
        }
        else if (other.gameObject.name.Contains("Fruit") && enabled == true)
        {
            Destroy(other.gameObject);
            GameObject.Find("FruitSpawner").GetComponent<FruitSpawner>().GameOver();
            GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Fruit"))
        {
            PlaySFX("Point", 0.05f);
            score++;
        }
    }
}
