using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject transition, controlsPanel;

    private bool animIsPlaying = false;

    public void Play()
    {
        if (!animIsPlaying) StartCoroutine(PlayGame());
    }

    IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(1.0f);

        animIsPlaying = true;
        transition.SetActive(true);

        yield return new WaitForSeconds(3.0f);

        animIsPlaying = false;
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        if (!animIsPlaying) Application.Quit();
    }

    public void Controls()
    {
        controlsPanel.SetActive(true);
    }

    public void CancerControls()
    {
        controlsPanel.SetActive(false);
    }
}
