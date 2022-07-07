using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] float _spawnTime;
    [SerializeField] Bird[] _birdPrefabs;
    [SerializeField] int _timeLimit;

    int _curTimeLimit;
    int _birdKilled;
    bool _isGameover;

    public bool IsGameover { get => _isGameover; set => _isGameover = value; }
    public int BirdKilled { get => _birdKilled; set => _birdKilled = value; }

    public override void Awake()
    {
        MakeSingleton(false);

        _curTimeLimit = _timeLimit;
    }

    public override void Start()
    {
        GameGUIManager.Ins.ShowGameGUI(false);

        GameGUIManager.Ins.UpdateKilledCounting(_birdKilled);
    }

    public void PlayGame() {
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIManager.Ins.ShowGameGUI(true);
    }

    IEnumerator TimeCountDown() {
        while (_curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            _curTimeLimit--;

            if (_curTimeLimit <= 0)
            {
                _isGameover = true;

                if (_birdKilled > Prefs.bestScore)
                {
                    GameGUIManager.Ins._gameDialog.UpdateDialog("NEW BEST", "BEST KILLED : x" + _birdKilled);
                }
                else
                {
                    GameGUIManager.Ins._gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED : x" + Prefs.bestScore);
                }

                Prefs.bestScore = _birdKilled;

                GameGUIManager.Ins._gameDialog.Show(true);
                GameGUIManager.Ins.CurrentDialog = GameGUIManager.Ins._gameDialog;

                
            }

            GameGUIManager.Ins.UpdateTimer(IntToTime(_curTimeLimit)); 
        }
    }

    IEnumerator GameSpawn() {
        while (!_isGameover)
        {
            SpawnBird();
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    void SpawnBird() {
        Vector3 spawnPos = Vector3.zero;

        float randomCheck = Random.Range(0f, 1f);

        if (randomCheck >= 0.5f)
        {
            spawnPos = new Vector3(12, Random.Range(-4f,4f), 0f);
        }
        else
        {
            spawnPos = new Vector3(-12, Random.Range(-4f, 4f), 0f);
        }

        if (_birdPrefabs != null && _birdPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, _birdPrefabs.Length);

            if (_birdPrefabs[randomIndex] != null)
            {
                Bird birdClone = Instantiate(_birdPrefabs[randomIndex], spawnPos, Quaternion.identity);
            }
        }

    }


    string IntToTime(int time) {
        float minute = Mathf.Floor(time / 60);
        float second = Mathf.RoundToInt(time % 60);

        return minute.ToString("00") + " : " + second.ToString("00");
    }


}
