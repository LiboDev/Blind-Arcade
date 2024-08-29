using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //scene
    [SerializeField] private Transform player;
    private TurretController turretController;

    [SerializeField] private AudioSource audioSource;

    //prefabs
    [SerializeField] private GameObject enemy;

    //presets
    [SerializeField] private float spawnDistance = 30f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float spawnDelay = 3f;

    [SerializeField] private Sound[] sounds;

    //tracking
    private Sound[] enemySources;

    public int wave = 1;

    // Start is called before the first frame update
    void Start()
    {
        turretController = player.gameObject.GetComponent<TurretController>();

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while(true)
        {
            Debug.Log("Wave " + wave);

            //spawn enemies for wave
            for (int i = 0; i < wave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnDelay);
            }

            yield return new WaitUntil(() => transform.childCount == 0);

            //next wave
            StartCoroutine(Score());
            wave++;
            
            yield return new WaitForSeconds(spawnDelay * 2f);
        }
    }

    private IEnumerator Score()
    {
        if(wave == 100)
        {
            PlaySFX("NextWave", 0f, 1f);
            GameOver();
        }

        int ones = wave % 10;

        Debug.Log(ones);

        int tens = ((wave-ones)/10) % 10;

        Debug.Log(tens);

        for (int i = 0;i < tens; i++)
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

    private void SpawnEnemy()
    {
        //calculate spawn point
        float angle = Random.Range(0f, Mathf.PI);
        Vector3 spawnPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * spawnDistance;

        //spawn enemy
        GameObject enemyObject = Instantiate(enemy, spawnPos, Quaternion.identity);
        Transform enemyTransform = enemyObject.transform;
        enemyTransform.parent = transform;

        //push enemy toward player
        enemyTransform.LookAt(player);
        enemyObject.GetComponent<Rigidbody>().velocity = enemyTransform.forward * Mathf.Min(wave/2f,maxSpeed);
    }

    public void GameOver()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        //enabled = false;
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

        if (s == null)
        {
            Debug.LogError("SoundNotFound");
        }
        else
        {
            audioSource.PlayOneShot(s.clip, volume);
        }
    }
}
