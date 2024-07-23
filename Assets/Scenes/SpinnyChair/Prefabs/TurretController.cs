using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    //scene
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sound[] sounds;

    //tracking
    private MobileControls mobileControls;
    private Quaternion initialRotation;
    private float topDownRotation;

    private bool canShoot = true;

    private bool hoveringEnemy = false;

    private int enemiesDefeated;

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
        transform.rotation = Quaternion.Euler(0, topDownRotation, 0);

        //raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 100f, out hit, 20f, 1 << 4))
        {
            hoveringEnemy = true;
            Handheld.Vibrate();
        }
        else
        {
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

        PlaySFX("Shoot");

        if (didHit == true)
        {
            Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();

            if (rb.velocity.magnitude >= 5)
            {
                rb.velocity /= 2f;
                PlaySFX("Damage");
                Debug.Log("hit");
            }
            else
            {
                Destroy(hit.transform.gameObject);
                //play sfx
                PlaySFX("Destroy");
                Debug.Log("destroy");
            }
        }
        else
        {
            Debug.Log("miss");
        }

        yield return new WaitForSeconds(0.25f);
        canShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            //play sfx
            PlaySFX("GameOver");
            PlaySFX("SelfDestruct");
            //gameover

            enemySpawner.GameOver();
            Debug.Log("GAME OVER \n YOUR SCORE: " + enemySpawner.wave);
        }
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
