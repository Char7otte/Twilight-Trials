using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void start_game() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void restart_level() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void go_to_main_menu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void quit_game() {
        Application.Quit();
    }
}
