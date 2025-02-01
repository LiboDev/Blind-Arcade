using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingSpawner : MonoBehaviour
{
    //scene
    [SerializeField] private GameObject ping;
    [SerializeField] private Vector3 spawnPos;

    //tracking



    //stats
    [SerializeField] private float speed;


    // Start is called before the first frame update
    void Start()
    {
        /*        int rand = Random.Range(0, 2);
                GameObject ball = Instantiate(ping, spawnPos, Quaternion.Euler(new Vector3(0, 90 * rand + 135, 0)));
                ball.GetComponent<Rigidbody>().velocity = ball.transform.forward * speed;*/

        Instantiate(ping, spawnPos, Quaternion.identity);
    }

/*    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Ball"))
        {
            other.GetComponent<BallController>().GameOver();
        }
    }*/
}
