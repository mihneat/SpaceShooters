using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed;

    private GameObject colliderObj;

    private void Start()
    {
        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(10.0f);

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shield")
        {
            colliderObj = collision.gameObject;

            colliderObj.GetComponent<ShieldManager>().LoseHP(1);

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            colliderObj = collision.gameObject;

            Destroy(gameObject);
        }
    }
}
