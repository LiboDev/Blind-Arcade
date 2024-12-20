using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    //scene
    [SerializeField] private GameObject fruit;
    private GameObject fruitObject;

    [SerializeField] private GameManager gameManager;

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
        

        float rand = Random.Range(0, 3);

        //reduce tripple probability
        if(rand == 2)
        {
            rand = Random.Range(0,3);
        }

        //spawn extra "fruits"
        if(speed >= 10 && rand != 0)
        {
            for(int i = 0; i < rand; i++)
            {
                InstantiateFruit("Fruit" + (i + 1));
                yield return new WaitForSeconds(0.5f);
            }
        }

        InstantiateFruit("Fruit0");

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

    private void InstantiateFruit(string name)
    {
        fruitObject = Instantiate(fruit, new Vector3(50, 0, 0), Quaternion.identity);
        fruitObject.GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0, 0);
        fruitObject.GetComponent<FruitController>().speed = speed;
        fruitObject.GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);

        fruitObject.name = name;
    }

    public void GameOver()
    {
        gameOver = true;
        gameManager.StartCoroutine(gameManager.GameOver());
    }
}
