using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehaviour : MonoBehaviour
{
    public int playerToKill;
    public float speed, rotateSpeed;
    public GameObject enemy;

    private GameObject colliderObj;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(15.0f);

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Vector2 dir = (Vector2)enemy.transform.position - rb.position;

        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.up).z;

        rb.angularVelocity = rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shield" && playerToKill == 1)
        {
            colliderObj = collision.gameObject;

            colliderObj.GetComponent<ShieldManager>().LoseHP(3);

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player" && playerToKill == 1)
        {
            colliderObj = collision.gameObject;

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Shield2" && playerToKill == 2)
        {
            colliderObj = collision.gameObject;

            colliderObj.GetComponent<ShieldManager>().LoseHP(3);

            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player2" && playerToKill == 2)
        {
            colliderObj = collision.gameObject;

            Destroy(gameObject);
        }
        //"I don't pay you to put evil notions in my head. Those already there don't need company" -Tyrion Lannister, GoT season 3
    }
}
