using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //scene
    [SerializeField] private MobileSteering mobileSteering;
    private Transform engine;
    private CoinSpawner coinSpawner;

    private Rigidbody rb;

    [SerializeField] private Sound[] sounds;
    private AudioSource audioSource;

    //tracking
    private float speed = 10f;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        coinSpawner = GameObject.Find("CoinSpawner").GetComponent<CoinSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        float magnitude = Mathf.Abs(mobileSteering.dirX);

        if(magnitude < 0.05f)
        {
            rb.velocity = new Vector3(0, 0, 0);
            engine.position = new Vector3(0, 0, -2);
        }
        else if (magnitude < 0.5f)
        {
            rb.velocity = new Vector3(mobileSteering.dirX, 0, 0) * speed;
            engine.position = new Vector3(mobileSteering.dirX, 0, -2);
        }
        else
        {
            rb.velocity = new Vector3(mobileSteering.dirX*0.5f/magnitude, 0, 0) * speed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaySFX("Honk", 0.01f);
        }
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

        audioSource.pitch = Random.Range(1f-variation, 1f+variation);

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
        if (other.gameObject.name.Contains("Coin"))
        {
            PlaySFX("Coin",0.05f);

            Destroy(other.gameObject);
            score++;

            Debug.Log("Score: " + score);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Coin"))
        {
            Destroy(other.gameObject);

            PlaySFX("GameOver",0f);

            coinSpawner.gameOver = true;

            Debug.Log("Final Score: " + score);
            enabled = false;
        }
    }
}
