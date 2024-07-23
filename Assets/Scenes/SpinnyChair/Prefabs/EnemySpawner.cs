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
            wave++;
            PlaySFX("NextWave");
            yield return new WaitForSeconds(spawnDelay * 2f);
        }
    }

    private void SpawnEnemy()
    {
        //calculate spawn point
        float angle = Random.Range(0f, 2 * Mathf.PI);
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

    private void PlaySFX(string name)
    {
        Sound s = null;

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                s = sounds[i];
            }
        }

        audioSource.pitch = Random.Range(0.9f, 1.1f);

        if (s == null)
        {
            Debug.LogError("SoundNotFound");
        }
        else
        {
            audioSource.PlayOneShot(s.clip);
        }
    }
}
