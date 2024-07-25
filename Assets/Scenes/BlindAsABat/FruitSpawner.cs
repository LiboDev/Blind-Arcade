using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    //scene
    [SerializeField] private GameObject fruit;
    private GameObject fruitObject;

    //tracking
    private int level = 0;
    private float speed = 5f;
    
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFruit());
    }

    private IEnumerator SpawnFruit()
    {
        fruitObject = Instantiate(fruit, new Vector3(30, Random.Range(20f,0f), 0), Quaternion.identity);
        fruitObject.GetComponent<Rigidbody>().velocity = new Vector3(-speed,0,0);

        yield return new WaitForSeconds(30f/speed);
        speed++;

        if(gameOver == false)
        {
            StartCoroutine(SpawnFruit());
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
