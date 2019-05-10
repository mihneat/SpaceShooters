using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public int maxHp = 3;

    [HideInInspector]
    public int hp;

    private BoxCollider2D bc;
    private SpriteRenderer sr;

    private void Start()
    {
        bc = gameObject.GetComponent<BoxCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        hp = maxHp;
    }

    public void LoseHP(int amount)
    {
        hp = Mathf.Max(hp - amount, 0);

        if (hp == 2)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.66f);
        }
        else if (hp == 1)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.33f);
        }
        else if (hp == 0)
        {
            StartCoroutine(Disable(15.0f));
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
