using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position)/2f;

        bc.size = new Vector3(distance, distance, distance);
    }
}