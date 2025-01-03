using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class CarController : MonoBehaviour
{
    //scene
    [SerializeField] private MobileSteering mobileSteering;
    private Transform engine;
    private CoinSpawner coinSpawner;

    private Rigidbody rb;

    [SerializeField] private Sound[] sounds;
    private AudioSource audioSource;

    [SerializeField] private AudioSource engineAudio;

    [SerializeField] private GameManager gameManager;

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
        float magnitude = Mathf.Abs(mobileSteering.dirY);

        if(magnitude < 0.05f)
        {
            rb.velocity = new Vector3(0, 0, 0);
            //engine.position = new Vector3(0, 0, -2);
        }
        else if (magnitude < 0.5f)
        {
            rb.velocity = new Vector3(-mobileSteering.dirY, 0, 0) * speed;
        }
        else
        {
            rb.velocity = new Vector3(-mobileSteering.dirY*0.5f/magnitude, 0, 0) * speed;
        }

/*        if (Input.GetMouseButtonDown(0))
        {
            PlaySFX("Honk", 0.01f,0.5f);
        }*/

/*        if (Mathf.Abs(transform.position.x)<9.49f)
        {
            engineAudio.volume = rb.velocity.magnitude / 5f;
        }
        else
        {
            engineAudio.volume = 0;
        }*/
    }

    private IEnumerator Score()
    {
        yield return new WaitForSeconds(3f);

        int ones = score % 10;
        int tens = ((score - ones) / 10) % 10;

        Debug.Log(tens);
        Debug.Log(ones);

        for (int i = 0; i < tens; i++)
        {
            PlaySFX("10", 0f, 1f);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < ones; i++)
        {
            PlaySFX("01", 0f, 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

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

        audioSource.pitch = Random.Range(1f-variation, 1f+variation);

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
        if (other.gameObject.name.Contains("Coin"))
        {
            PlaySFX("Coin",0.01f, 0.5f);

            Destroy(other.gameObject);
            score++;

            if (score == 100)
            {
                PlaySFX("NextWave", 0f, 1f);
                GameOver();
            }

            Debug.Log("Score: " + score);
        }
        else if (other.gameObject.name.Contains("Quad"))
        {
            PlaySFX("Hit", 0.01f, 1f);
            Vibration.VibratePredefined(1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Coin"))
        {
            Destroy(other.gameObject);

            GameOver();
        }
    }

    private void GameOver()
    {
        PlaySFX("GameOver", 0f, 1f);
        Vibration.Vibrate(200, 100);

        coinSpawner.gameOver = true;
        engineAudio.volume = 0;

        Debug.Log("Final Score: " + score);
        StartCoroutine(Score());

        gameManager.StartCoroutine(gameManager.GameOver());
        enabled = false;
    }
}
