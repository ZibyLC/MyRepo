using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;
    public GameObject hud;
    public GameObject startMenu;
    public GameObject player;
    public GameObject gameoverMenu;
    public Text gameoverScore;

    void Start()
    {
        Time.timeScale = 0;
        player.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        hud.SetActive(false);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        hud.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startMenu.SetActive(false);
        hud.SetActive(true);
        player.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        player.SetActive(false);
        hud.SetActive(false);
        ScoreController scoreController = player.GetComponent<ScoreController>();
        gameoverScore.text = "YOUR "+scoreController.t_score.text;
        gameoverMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
