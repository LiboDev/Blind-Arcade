using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    //scene
    [SerializeField] private GameObject fruit;
    private GameObject fruitObject;

    [SerializeField] private GameObject gameOverObject;

    //tracking
    private float speed = 5f;
    
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFruit());
    }

    public IEnumerator SpawnFruit()
    {
        fruitObject = Instantiate(fruit, new Vector3(50, 0, 0), Quaternion.identity);
        fruitObject.GetComponent<Rigidbody>().velocity = new Vector3(-speed,0,0);
        fruitObject.GetComponent<FruitController>().speed = speed;
        fruitObject.GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);

        yield return new WaitForSeconds(51f/speed);
        
        if(speed < 25)
        {
            speed++;
        }

        if(gameOver == false)
        {
            StartCoroutine(SpawnFruit());
        }
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverObject.SetActive(true);
    }
}
