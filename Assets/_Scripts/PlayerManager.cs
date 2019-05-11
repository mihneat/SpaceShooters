using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHp = 3;

    [HideInInspector]
    public int hp;
    public GameObject HealthP1;

    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private GameObject HP3P1, HP2P1, HP1P1;

    private void Start()
    {
        bc = gameObject.GetComponent<BoxCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        HP1P1 = HealthP1.transform.GetChild(0).gameObject;
        HP2P1 = HealthP1.transform.GetChild(1).gameObject;
        HP3P1 = HealthP1.transform.GetChild(2).gameObject;

        hp = maxHp;
    }

    public void LoseHP(int amount)
    {
        hp = Mathf.Max(hp - amount, 0);

        if (hp == 2)
        {
            HP3P1.SetActive(false);

            RegainShields();
        }
        else if (hp == 1)
        {
            HP2P1.SetActive(false);

            RegainShields();
        }
        else if (hp == 0)
        {
            HP1P1.SetActive(false);

            GameManagerScript.EndGame();
        }
    }

    void RegainShields()
    {
        Transform shield = transform.parent.GetChild(0);

        for (int i=0; i < shield.childCount; i++)
        {
            shield.GetChild(i).gameObject.GetComponent<ShieldManager>().RegainShield();
        }
    }

    IEnumerator Disable(float timeToWaitFor)
    {
        bc.enabled = false;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f);

        yield return new WaitForSeconds(timeToWaitFor);

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
        hp = maxHp;
        bc.enabled = true;
    }
}
