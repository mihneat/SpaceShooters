using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private List<GameObject> maps = new List<GameObject>();
    private int remaining = 5, randomNumber;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject currentMap = transform.GetChild(i).gameObject;
            maps.Add(currentMap);
        }
    }

    public void ChooseLevel()
    {
        if (remaining < 5)
        {
            maps.RemoveAt(randomNumber);
            Destroy(transform.GetChild(randomNumber).gameObject);
        }

        if (remaining >= 0)
        {
            randomNumber = Random.Range(0, remaining);
            remaining--;

            transform.GetChild(randomNumber).gameObject.SetActive(true);
        }

        Debug.Log(randomNumber + " is the random number!");
    }
}
