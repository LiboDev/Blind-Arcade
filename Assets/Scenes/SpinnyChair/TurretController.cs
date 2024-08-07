using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class TurretController : MonoBehaviour
{
    //scene
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sound[] sounds;
    //

    //tracking
    [SerializeField] private GameObject gameOverObject;

    private MobileControls mobileControls;
    private Quaternion initialRotation;
    private float topDownRotation;
    private float previousTopDownRotation;

    private bool canShoot = true;

    private bool hoveringEnemy = false;

    private int enemiesDefeated;

    private GameObject hoveredEnemy;

    // Start is called before the first frame update
    void Start()
    {

        mobileControls = GetComponent<MobileControls>();
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //set rotation
        transform.rotation = mobileControls.rot;
        topDownRotation = (Mathf.Atan2(-transform.right.x - transform.position.x, -transform.right.z - transform.position.z)) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, topDownRotation+90, 0);

        //raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 100f, out hit, 20f, 1 << 4))
        {
            Vibration.Vibrate(100, 25);

            if (hoveringEnemy == false)
            {
                Debug.Log("vibrate"); //vibrate
                hoveredEnemy = hit.transform.gameObject;
                hoveredEnemy.GetComponent<AudioSource>().volume = 1;
            }
            hoveringEnemy = true;
        }
        else
        {
            if(hoveredEnemy != null)
            {
                Vibration.Cancel();

                hoveredEnemy.GetComponent<AudioSource>().volume = 0.1f;
            }
            
            hoveringEnemy = false;
        }

        if (Input.GetMouseButton(0) && canShoot)
        {
            StartCoroutine(Shoot(hit, hoveringEnemy));
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);
    }

    private IEnumerator Shoot(RaycastHit hit, bool didHit)
    {
        canShoot = false;
        PlaySFX("Shoot",0.05f, 0.5f);

        if (didHit == true)
        {
            Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();

            if (rb.velocity.magnitude >= 5)
            {
                rb.velocity /= 2f;
                PlaySFX("Damage", 0.05f, 1f);
                Debug.Log("hit");
            }
            else
            {
                Destroy(hit.transform.gameObject);
                //play sfx
                PlaySFX("Destroy", 0.05f,1f);
                Debug.Log("destroy");
            }
        }
        else
        {
            Debug.Log("miss");
        }

        Vibration.VibratePredefined(0);

        yield return new WaitForSeconds(0.25f);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            //play sfx
            PlaySFX("GameOver",0,1);
            PlaySFX("SelfDestruct",0.05f,1);
            //gameover

            enemySpawner.GameOver();
            Debug.Log("GAME OVER \n YOUR SCORE: " + enemySpawner.wave);

            gameOverObject.SetActive(true);

            Vibration.Vibrate(200, 100);

            enabled = false;
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
}
