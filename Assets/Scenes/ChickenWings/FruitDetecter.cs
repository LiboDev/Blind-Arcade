using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDetecter : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Fruit"))
        {
            Destroy(other.gameObject);
        }
    }
}
