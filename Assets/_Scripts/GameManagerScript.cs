using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player1, player2;
    public GameObject player1Shoot, player2Shoot;
    public GameObject bullet;
    public float rotationSpeed, moveSpeed;

    private GameObject shield1, shield2;
    private bool canShootP1 = true, canShootP2 = true;

    private void Start()
    {
        shield1 = player1.transform.GetChild(0).gameObject;
        shield2 = player2.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        #region Player 1 Movement

        if (Input.GetKeyDown(KeyCode.W) && canShootP1)
        {
            StartCoroutine(DisableGunP1());

            GameObject instantiatedBullet = Instantiate(bullet, player1Shoot.transform.position, Quaternion.identity);

            instantiatedBullet.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        }


        float dir;

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            dir = 0.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir = 1.0f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dir = -1.0f;
        }
        else
        {
            dir = 0.0f;
        }

        player1.transform.Translate(new Vector3(dir * moveSpeed, 0.0f, 0.0f) * Time.deltaTime * 50, Space.World);


        float rDir;

        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
        {
            rDir = 0.0f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rDir = -1.0f;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rDir = 1.0f;
        }
        else
        {
            rDir = 0.0f;
        }

        shield1.transform.Rotate(new Vector3(0.0f, 0.0f, rDir * rotationSpeed) * Time.deltaTime * 50);

        #endregion

        #region Player 2 Movement

        if (Input.GetKeyDown(KeyCode.I) && canShootP2)
        {
            StartCoroutine(DisableGunP2());

            GameObject instantiatedBullet = Instantiate(bullet, player2Shoot.transform.position, Quaternion.Inverse(Quaternion.identity));

            instantiatedBullet.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            instantiatedBullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }


        float dir2;

        if (Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.L))
        {
            dir2 = 0.0f;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            dir2 = 1.0f;
        }
        else if (Input.GetKey(KeyCode.J))
        {
            dir2 = -1.0f;
        }
        else
        {
            dir2 = 0.0f;
        }

        player2.transform.Translate(new Vector3(dir2 * moveSpeed, 0.0f, 0.0f) * Time.deltaTime * 50, Space.World);


        float rDir2;

        if (Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.O))
        {
            rDir2 = 0.0f;
        }
        else if (Input.GetKey(KeyCode.O))
        {
            rDir2 = -1.0f;
        }
        else if (Input.GetKey(KeyCode.U))
        {
            rDir2 = 1.0f;
        }
        else
        {
            rDir2 = 0.0f;
        }

        shield2.transform.Rotate(new Vector3(0.0f, 0.0f, -rDir2 * rotationSpeed) * Time.deltaTime * 50);

        #endregion
    }

    IEnumerator DisableGunP1()
    {
        canShootP1 = false;

        yield return new WaitForSeconds(0.2f);

        canShootP1 = true;
    }

    IEnumerator DisableGunP2()
    {
        canShootP2 = false;

        yield return new WaitForSeconds(0.2f);

        canShootP2 = true;
    }
}
