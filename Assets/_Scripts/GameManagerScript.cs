using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player1, player2;
    public GameObject player1Spawn, player2Spawn;
    public GameObject player1Shoot, player2Shoot;
    public GameObject bullet, missile, gun1, gun2;
    public GameObject spriteMask;
    public GameObject maps;
    public GameObject ammoParent;
    public TMP_Text timeText, roundText;
    public GameObject victory1Text, victory2Text;
    public float rotationSpeed, moveSpeed, missileCooldown;
    
    private Camera mainCam;
    private GameObject shield1, shield2;
    private bool canShootP1 = true, canShootP2 = true, canMissileP1 = true, canMissileP2 = true;
    private float initialTime, elapsedSeconds, elapsedMinutes, elapsedHours;

    private void Start()
    {
        mainCam = Camera.main;

        shield1 = player1.transform.GetChild(0).gameObject;
        shield2 = player2.transform.GetChild(0).gameObject;

        initialTime = Time.time;

        maps.GetComponent<MapManager>().ChooseLevel();

        StartCoroutine(DisableMissileP1());
    }

    private void Update()
    {
        roundText.text = "Round " + (maps.GetComponent<MapManager>().roundNumber - 1);

        int elapsedTime = Mathf.FloorToInt(Time.time - initialTime);
        elapsedSeconds = elapsedTime % 60;
        elapsedMinutes = (elapsedTime / 60) % 60;
        elapsedHours = (elapsedTime / 3600) % 60;

        if (elapsedHours > 0) timeText.text = "You should probably stop playing.";
        else if (elapsedMinutes > 0) timeText.text = "Elasped Time:\n" + elapsedMinutes + "m " + elapsedSeconds + "s";
        else timeText.text = "Elasped Time:\n" + elapsedSeconds + "s";



        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();


        #region Player 1 Movement

        if (Input.GetKey(KeyCode.Mouse0) && canShootP1)
        {
            StartCoroutine(DisableGunP1());

            GameObject instantiatedBullet = Instantiate(bullet, player1Shoot.transform.position, player1.transform.rotation, ammoParent.transform);

            player1.GetComponent<AudioSource>().Play();

            instantiatedBullet.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
            instantiatedBullet.GetComponent<BulletBehaviour>().playerToKill = 2;
            instantiatedBullet.transform.GetChild(0).GetComponent<TrailRenderer>().startColor = new Color(0.0f, 1.0f, 0.0f, 0.5f);
            instantiatedBullet.transform.GetChild(0).GetComponent<TrailRenderer>().endColor = new Color(0.0f, 1.0f, 0.0f, 0.25f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && canMissileP1)
        {
            StartCoroutine(DisableMissileP1());

            GameObject instantiatedBullet = Instantiate(missile, player1Shoot.transform.position, Quaternion.identity, ammoParent.transform);

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

        if (Input.GetKey(KeyCode.I) && canShootP2)
        {
            StartCoroutine(DisableGunP2());

            GameObject instantiatedBullet = Instantiate(bullet, player2Shoot.transform.position, player2.transform.rotation, ammoParent.transform);

            player2.GetComponent<AudioSource>().Play();
            // instantiatedBullet.GetComponent<BulletBehaviour>().speed *= -1;
            instantiatedBullet.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
            instantiatedBullet.GetComponent<BulletBehaviour>().playerToKill = 1;
            instantiatedBullet.transform.GetChild(0).GetComponent<TrailRenderer>().startColor = new Color(1.0f, 0.0f, 1.0f, 0.5f);
            instantiatedBullet.transform.GetChild(0).GetComponent<TrailRenderer>().endColor = new Color(1.0f, 0.0f, 1.0f, 0.25f);
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

    public void StartRound()
    {
        player1.transform.position = player1Spawn.transform.position;
        player2.transform.position = player2Spawn.transform.position;
    }

    public IEnumerator EndRound(GameObject loser)
    {
        if (loser.transform.GetChild(1).tag == "Player")
        {
            canShootP1 = false;
            canMissileP1 = false;
        }
        else if (loser.transform.GetChild(1).tag == "Player2")
        {
            canShootP2 = false;
            canMissileP2 = false;
        }

        loser.transform.GetChild(0).gameObject.SetActive(false);
        loser.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        loser.transform.GetChild(1).GetComponent<CircleCollider2D>().enabled = false;
        loser.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(4.0f);

        for (int i = 0; i < ammoParent.transform.childCount; i++)
            Destroy(ammoParent.transform.GetChild(i).gameObject);

        if (loser.transform.GetChild(1).tag == "Player")
        {
            canShootP1 = true;
            canMissileP1 = true;
        }
        else if (loser.transform.GetChild(1).tag == "Player2")
        {
            canShootP2 = true;
            canMissileP2 = true;
        }

        loser.transform.GetChild(0).gameObject.SetActive(true);
        loser.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        loser.transform.GetChild(1).GetComponent<CircleCollider2D>().enabled = true;
        loser.transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = true;

        loser.transform.GetChild(1).GetComponent<PlayerManager>().RegainShields();
        maps.GetComponent<MapManager>().ChooseLevel();
        StartRound();
    }

    public void EndGame(Transform loser)
    {
        if (loser.GetChild(1).tag == "Player")
        {
            canShootP1 = false;
            canMissileP1 = false;

            loser.GetChild(0).gameObject.SetActive(false);
            loser.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            loser.GetChild(1).GetComponent<CircleCollider2D>().enabled = false;
            loser.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;

            victory2Text.SetActive(true);
        }
        else if (loser.GetChild(1).tag == "Player2")
        {
            canShootP2 = false;
            canMissileP2 = false;

            loser.GetChild(0).gameObject.SetActive(false);
            loser.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            loser.GetChild(1).GetComponent<CircleCollider2D>().enabled = false;
            loser.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;

            victory1Text.SetActive(true);
        }

        // SceneManager.LoadScene("MainScene");
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
