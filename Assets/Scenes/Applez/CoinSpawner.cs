using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class CoinSpawner : MonoBehaviour
{
    //scene
    [SerializeField] private GameObject coin;
    private GameObject coinObject;
    [SerializeField] private Transform car;

    private AudioSource audioSource;
    public AudioClip clip;

    //tracking
    private int x = 4;
    private float speed = 5f;
    private float pos;

    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(SpawnCoin());
    }

    private IEnumerator FirstCoin()
    {
        x++;
        speed = Mathf.Log(x, 10) * 5f;
        Debug.Log("speed:" + speed);

        pos = -5;

        coinObject = Instantiate(coin, new Vector3(pos, 0, 25), Quaternion.identity);
        coinObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);

        StartCoroutine(Direction());

        yield return new WaitForSeconds(30f / speed);

        if (gameOver == false)
        {
            StartCoroutine(SpawnCoin());
        }
        else
        {
            enabled = false;
        }
    }

    private IEnumerator SpawnCoin()
    {
        x++;
        speed = Mathf.Log(x, 10) * 5f;
        Debug.Log("speed:" + speed);

        pos = Random.Range(-10f, 10f);

        coinObject = Instantiate(coin, new Vector3(pos, 0, 25), Quaternion.identity);
        coinObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);

        StartCoroutine(Direction());

        yield return new WaitForSeconds(30f / speed);


        if (gameOver == false)
        {
            StartCoroutine(SpawnCoin());
        }
        else
        {
            enabled = false;
        }
    }

    private IEnumerator Direction()
    {
        while (coinObject != null)
        {
            float distance = Mathf.Abs(pos - car.transform.position.x);

            if (distance < 2)
            {
                //play on target sound effect
                Debug.Log("on target");
                audioSource.panStereo = 0;
                audioSource.pitch = 1f;
                audioSource.volume = 1f;
            }
            else if (pos < car.transform.position.x)
            {
                //play left sound effect
                Debug.Log("Left");
                audioSource.panStereo = -1;
                audioSource.pitch = 0.95f;
                audioSource.volume = (20 - distance) / 20f;
            }
            else if (pos > car.transform.position.x)
            {
                //play right sound effect
                Debug.Log("Right");
                audioSource.panStereo = 1;
                audioSource.pitch = 0.95f;
                audioSource.volume = (20 - distance) / 20f;
            }
            audioSource.Play();

            yield return new WaitForSeconds(Mathf.Max(coinObject.transform.position.z / 20, 0.25f));
        }
    }
}
