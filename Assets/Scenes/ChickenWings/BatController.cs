using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class BatController : MonoBehaviour
{

    //scene
    [SerializeField] private Transform teleportPos;
    private MobileShake mobileShake;
    private Rigidbody rb;

    [SerializeField] private Sound[] sounds;
    private AudioSource audioSource;

    //tracking
    private bool canJump = false;
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

    private IEnumerator Score()
    {
        yield return new WaitForSeconds(3f);

        int ones = score % 10;

        Debug.Log(ones);

        int tens = ((score - ones) / 10) % 10;

        Debug.Log(tens);

        for (int i = 0; i < tens; i++)
        {
            PlaySFX("10", 0f);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < ones; i++)
        {
            PlaySFX("01", 0f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Vibrate()
    {
        if(score < 5)
        {
            Handheld.Vibrate();
            Debug.Log("JUMPPPPP");
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
            Vibration.VibratePredefined(0);
            canJump = true;
        }
        else if (other.gameObject.name.Contains("Fruit") && enabled == true)
        {
            transform.position = teleportPos.position + Vector3.up;
            Vibration.Vibrate(200, 100);
            Destroy(other.gameObject);
            GameObject.Find("FruitSpawner").GetComponent<FruitSpawner>().GameOver();

            StartCoroutine(Score());

            GameOver();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Fruit0"))
        {
            if (score == 100)
            {
                PlaySFX("NextWave", 0f);
                GameOver();
            }

            PlaySFX("Point", 0.05f);
            score++;
        }
    }
}
