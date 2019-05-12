using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public int type;
    public float increase, duration, speed, frequency, magnitude;
    public GameObject gm;
    private float player;
    private float initialTime;
    private GameManagerScript gms;
    Vector2 pos;

    private void Start()
    {
        gms = FindObjectOfType<GameManagerScript>();

        initialTime = Time.time;
    }
    private void FixedUpdate()
    {
        Vector2 dir = new Vector2(0f, -1f * speed);
        dir.x = Mathf.Sin(frequency * (Time.time - initialTime)) * magnitude;
        transform.Translate(dir);
    }

    public void Activate(int player)
    {
        if (type == 1)
            gms.PowerUpFireRate(increase, duration, player);
        else if (type == 2)
            gms.SpeedIncrease(increase, duration, player);
        else if (type == 3)
            gms.ShieldRegenPowerUp(player);
        else if (type == 4)
            gms.ActivateThroughWalls(duration, player);
    }


}
