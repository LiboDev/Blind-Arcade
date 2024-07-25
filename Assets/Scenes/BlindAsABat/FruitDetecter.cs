using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDetecter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Fruit"))
        {
            GameObject.Find("Player").GetComponent<BatController>().GameOver();
            GameObject.Find("FruitSpawner").GetComponent<FruitSpawner>().GameOver();
        }
    }
}
