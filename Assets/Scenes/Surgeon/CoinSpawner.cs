using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    //scene
    [SerializeField] private GameObject coin;
    private GameObject coinObject;
    [SerializeField] private CarController carController;

    //tracking
    private int x = 4;
    private float speed = 5f;

    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoin());
    }

    private IEnumerator SpawnCoin()
    {
        x++;
        speed = Mathf.Log(x,10) * 5f;
        Debug.Log("speed:" + speed);
        

        coinObject = Instantiate(coin, new Vector3(Random.Range(-10f, 10f),0, 25), Quaternion.identity);
        coinObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed);

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
}
