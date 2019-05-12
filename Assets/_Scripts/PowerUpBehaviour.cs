using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public int type;
    public float increase, duration, speed, frequency, magnitude;
    public GameObject gm;
    public Sprite spriteToReplaceWith;
    public Color spriteColor;
    public int randomNumber;

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
        Vector2 dir = Vector2.zero;

        if (randomNumber == 0)
        {
            dir = new Vector2(0f, -1f * speed);
        }
        else
        {
            dir = new Vector2(0f, speed);
        }
        
        dir.x = Mathf.Sin(frequency * (Time.time - initialTime)) * magnitude;
        transform.Translate(dir);
    }

    public void Activate(int player, GameObject abilityObj)
    {
        Color col = abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().color;
        abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, 1.0f);

        if (type == 1)
        {
            Debug.Log(spriteToReplaceWith.name);

            abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = spriteToReplaceWith;
            abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1.0f);
            abilityObj.GetComponent<Animator>().Play("P2AbilityBarAnim");

            gms.PowerUpFireRate(increase, duration, player, abilityObj);
        }
        else if (type == 2)
        {
            Debug.Log(spriteToReplaceWith.name);

            abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = spriteToReplaceWith;
            abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1.0f);
            abilityObj.GetComponent<Animator>().Play("P2AbilityBarAnim");

            gms.SpeedIncrease(increase, duration, player, abilityObj);
        }
        else if (type == 3)
        {
            Debug.Log(spriteToReplaceWith.name);

            abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = spriteToReplaceWith;
            abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1.0f);

            gms.ShieldRegenPowerUp(player, abilityObj);
        }
        else if (type == 4)
        {
            Debug.Log(spriteToReplaceWith.name);

            abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = spriteToReplaceWith;
            abilityObj.transform.GetChild(4).GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1.0f);
            abilityObj.GetComponent<Animator>().Play("P2AbilityBarAnim");

            gms.ActivateThroughWalls(duration, player, abilityObj);
        }
    }
}
