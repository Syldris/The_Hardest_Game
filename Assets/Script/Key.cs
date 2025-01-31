using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static Key instance;
    public GameObject Wall;

    public void Eat()
    {
        Wall.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Retry()
    {
        gameObject.SetActive(true);
        Wall.SetActive(true);
    }
}
