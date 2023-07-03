using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject GameWinPanel;
    public GameObject GameLosePanel;
    public Image health_bar;
    public Text timerText;
    public Text pointText;
    public Text healthText;
    public int pointCount;
    public int pointToWin;
    public float max_health_count;

    private float elapsedGameTime;
    private float elapsedInstructionTime;
    private float instructionDisplayTime;
    private bool isGameOver;
    private bool isGameWin;
    private float healthCount;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        pointCount = 0;
        healthCount = max_health_count;

        SetHealthText();
        SetPointText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        elapsedGameTime += Time.deltaTime;
        SetTimeText(elapsedGameTime);
    }

    public void SetGameOver(bool isGameWin)
    {
        isGameOver = true;

        if (!GameWinPanel.activeSelf && !GameLosePanel.activeSelf)
        {
            if (isGameWin)
            {
                GameWinPanel.SetActive(true);
                AudioManager.instance.StopBGM();
                AudioManager.instance.PlayGameWinSfx();
            }
            else
            {
                GameLosePanel.SetActive(true);
                AudioManager.instance.StopBGM();
                AudioManager.instance.PlayGameLoseSfx();
            }
        }
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    public int GetPointCount()
    {
        return pointCount;
    }
    public int GetPointToWin()
    {
        return pointToWin;
    }

    public void AddPoint()
    {
        pointCount++;
        SetPointText();
    }

    public void AddHealth(float addHealthValue)
    {
        healthCount += addHealthValue;

        if (healthCount > max_health_count)
            healthCount = max_health_count;

        SetHealthText();
    }

    public void MinusHealth(float minusHealthValue)
    {
        healthCount -= minusHealthValue;
        AudioManager.instance.PlayDamageSfx();

        if (healthCount < 0)
        {
            healthCount = 0;
            SetGameOver(false);
        }

        SetHealthText();
    }

    private void SetPointText()
    {
        pointText.text = "Treasure: " + pointCount;
    }

    private void SetHealthText()
    {
        health_bar.fillAmount = healthCount / max_health_count;
        healthText.text = "Health: " + System.String.Format("{0:00}", healthCount);
    }

    private void SetTimeText(float time)
    {
        timerText.text = "Time: " + FormatTime(time);
    }

    public void MenuBtn() 
    {
        SceneManager.LoadScene("MenuScene");  
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = System.String.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }
}
