using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBackScript : MonoBehaviour
{
    public GameObject next, current, back;
    public void Next()
    {
        Debug.Log("zi ceva mon");
        next.SetActive(true);
        current.SetActive(false);
    }
    public void Back()
    {
        back.SetActive(true);
        current.SetActive(false);
    }
}
