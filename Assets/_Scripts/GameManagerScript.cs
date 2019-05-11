using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player1, player2;
    public GameObject player1Shoot, player2Shoot;
    public GameObject bullet, missile, gun1, gun2;
    public GameObject spriteMask;
    public GameObject maps;

    public float rotationSpeed, moveSpeed, missileCooldown;
    
    private Camera mainCam;
    private GameObject shield1, shield2;
    private bool canShootP1 = true, canShootP2 = true, canMissileP1 = true, canMissileP2 = true;

    private void Start()
    {
        mainCam = Camera.main;

        shield1 = player1.transform.GetChild(0).gameObject;
        shield2 = player2.transform.GetChild(0).gameObject;

        maps.GetComponent<MapManager>().ChooseLevel();

        StartCoroutine(DisableMissileP1());
    }

    private void Update()
    {
        #region Player 1 Movement

        if (Input.GetKey(KeyCode.Mouse0) && canShootP1)
        {
            StartCoroutine(DisableGunP1());

            GameObject instantiatedBullet = Instantiate(bullet, player1Shoot.transform.position, player1.transform.rotation);

            instantiatedBullet.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            instantiatedBullet.GetComponent<BulletBehaviour>().playerToKill = 2;
            instantiatedBullet.transform.GetChild(0).GetComponent<TrailRenderer>().startColor = new Color(0.0f, 1.0f, 0.0f, 0.5f);
            instantiatedBullet.transform.GetChild(0).GetComponent<TrailRenderer>().endColor = new Color(0.0f, 1.0f, 0.0f, 0.25f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canMissileP1)
        {
            StartCoroutine(DisableMissileP1());

            GameObject instantiatedBullet = Instantiate(missile, player1Shoot.transform.position, Quaternion.identity);

            instantiatedBullet.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            instantiatedBullet.GetComponent<MissileBehaviour>().enemy = player2;
            instantiatedBullet.GetComponent<MissileBehaviour>().playerToKill = 2;
        }

        float dirHor, dirVer;

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            dirHor = 0.0f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dirHor = 1.0f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dirHor = -1.0f;
        }
        else
        {
            dirHor = 0.0f;
        }

        // Vertical Movement P1

        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                dirVer = 0.0f;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                dirVer = 1.0f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dirVer = -1.0f;
            }
            else
            {
                dirVer = 0.0f;
            }
        }
        
        player1.transform.Translate(new Vector3(dirHor * moveSpeed, dirVer * moveSpeed, 0.0f) * Time.deltaTime * 50, Space.World);

        float x1, y1;

        x1 = player1.transform.position.x;
        y1 = player1.transform.position.y;

        player1.transform.position = new Vector3(Mathf.Clamp(x1, -19.2f, -2.35f), Mathf.Clamp(y1, -10.5f, 10.5f), 0.0f);


        float rDir = rotationSpeed;

        {
            /*if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
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
        }*/
        }


        shield1.transform.Rotate(new Vector3(0.0f, 0.0f, rDir * rotationSpeed) * Time.deltaTime * 50);
        // player1.transform.Rotate(new Vector3(0.0f, 0.0f, 2*rDir * rotationSpeed) * Time.deltaTime * 50);


        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCam.ScreenToWorldPoint(mousePos);
        Vector2 dir = new Vector2(mousePos.x - player1.transform.position.x, mousePos.y - player1.transform.position.y);
        dir.Normalize();

        player1Shoot.transform.position = new Vector2(player1.transform.position.x, player1.transform.position.y) + dir;
        player1.transform.up = dir;


        #endregion

        #region Player 2 Movement

        if (Input.GetKeyDown(KeyCode.I) && canShootP2)
        {
            StartCoroutine(DisableGunP2());

            GameObject instantiatedBullet = Instantiate(bullet, player2Shoot.transform.position, player2.transform.rotation);
            // instantiatedBullet.GetComponent<BulletBehaviour>().speed *= -1;
            instantiatedBullet.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            instantiatedBullet.GetComponent<BulletBehaviour>().playerToKill = 1;
            //instantiatedBullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }

        // instantiatedBullet.transform.GetChild(0).GetComponent<TrailRenderer>().startColor = new Color(1.0f, 0.0f, 1.0f, 0.25f);


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

        float x2, y2;

        x2 = player2.transform.position.x;
        y2 = player2.transform.position.y;

        player2.transform.position = new Vector3(Mathf.Clamp(x2, 2.35f, 19.2f), Mathf.Clamp(y2, -10.5f, 10.5f), 0.0f);


        float rDir2 = rotationSpeed;
        /*
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
        }*/

        shield2.transform.Rotate(new Vector3(0.0f, 0.0f, rDir2 * rotationSpeed) * Time.deltaTime * 50);
        // player2.transform.Rotate(new Vector3(0.0f, 0.0f, 2*rDir2 * rotationSpeed) * Time.deltaTime * 50);
        #endregion
    }

    public static void EndGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator DisableGunP1()
    {
        canShootP1 = false;

        yield return new WaitForSeconds(0.25f);

        canShootP1 = true;
    }

    IEnumerator DisableGunP2()
    {
        canShootP2 = false;

        yield return new WaitForSeconds(0.3f);

        canShootP2 = true;
    }

    IEnumerator DisableMissileP1()
    {
        canMissileP1 = false;

        spriteMask.transform.parent.GetComponent<Animator>().Play("New Animation");

        yield return new WaitForSeconds(missileCooldown);
        
        canMissileP1 = true;
        
    }

    IEnumerator DisableMissileP2()
    {
        canMissileP2 = false;
        yield return new WaitForSeconds(missileCooldown);
        canMissileP2 = true;
    }
}
