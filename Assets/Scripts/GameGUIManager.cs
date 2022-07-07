using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameGUIManager : Singleton<GameGUIManager>
{
    public GameObject _homeGUI;
    public GameObject _gameGUI;

    public Dialog _gameDialog;
    public Dialog _pauseDialog;

    public Image _fireRateFilled;
    public TMP_Text _timerText;
    public TMP_Text _killedCountingText;

    Dialog _currentDialog;

    public Dialog CurrentDialog { get => _currentDialog; set => _currentDialog = value; }

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGameGUI(bool isShow)
    {
        if (_gameGUI) _gameGUI.SetActive(isShow);
        if (_homeGUI) _homeGUI.SetActive(!isShow);
    }//neu hien thi gameGUI thi tat homeGUI va nguoc lai

    public void UpdateTimer(string time) {
        if (_timerText) _timerText.text = time;
    }

    public void UpdateKilledCounting(int killed) {
        if (_killedCountingText) _killedCountingText.text = "x" + killed.ToString();
    }

    public void UpdateFireRate(float rate)
    {
        if (_fireRateFilled) _fireRateFilled.fillAmount = rate;
    }


    public void PauseGame() {
        Time.timeScale = 0f; //Pause moi thu

        if (_pauseDialog)
        {
            _pauseDialog.Show(true);
            _pauseDialog.UpdateDialog("GAME PAUSE", "BEST KILLED : x" + Prefs.bestScore);
            _currentDialog = _pauseDialog;
        }
    }


    public void ResumeGame()
    {
        Time.timeScale = 1f;

        if (_currentDialog)
        {
            _currentDialog.Show(false);
        }
    }

    public void BackToHome()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Replay()
    {
        if (_currentDialog)
        {
            _currentDialog.Show(false);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        GameManager.Ins.PlayGame();
    }
    public void ExitGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Application.Quit();
    }

}
