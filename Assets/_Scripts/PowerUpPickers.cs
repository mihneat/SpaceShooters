using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickers : MonoBehaviour
{
    public int player;
    public GameObject ability;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "powerup")
        {
            collision.gameObject.GetComponent<PowerUpBehaviour>().Activate(player, ability);
            Destroy(collision.gameObject);
        }
    }
}
