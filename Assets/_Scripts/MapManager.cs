using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int roundNumber = 1;

    private int[] order = new int[10];
    private int noOfMaps;

    private void Awake()
    {
        noOfMaps = transform.childCount;

        CreateOrder();

        for (int i = 1; i <= noOfMaps; i++)
            Debug.Log(order[i]);
    }

    private void CreateOrder()
    {
        for (int i = 1; i <= noOfMaps; i++)
            order[i] = i - 1;

        for (int i = noOfMaps; i >= 2; i--)
        {
            int randomMap = (Random.Range(1, i - 1) * Random.Range(1, i - 1)) % (i - 1) + 1;

            int x = order[randomMap];
            order[randomMap] = order[i];
            order[i] = x;
        }
    }

    public void ChooseLevel()
    {
        if (roundNumber == 1)
        {
            transform.GetChild(order[roundNumber]).gameObject.SetActive(true);
            roundNumber++;
        }
        else if (roundNumber <= 5)
        {
            transform.GetChild(order[roundNumber - 1]).gameObject.SetActive(false);
            transform.GetChild(order[roundNumber]).gameObject.SetActive(true);

            roundNumber++;
        }
    }
}
