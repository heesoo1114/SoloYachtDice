using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private SceneContoller _sceneController;

    public Action GameDoneEvent;

    private int playerCnt = 1;

    private int turnCnt;
    public int TurnCnt
    {
        get => turnCnt;
        set
        {
            turnCnt = value;
            if (TurnCnt == playerCnt * 10)
            {
                Debug.Log("Game Done");
                GameDoneEvent?.Invoke();
                GameDone();
            }
        }
    }

    private bool isGameOver = false;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    public int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogError("multiple gamemanager is running");
        }
    }

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _sceneController.LoadScene("Intro");
            }
        }
    }

    private void GameDone()
    {
        gameOverPanel.SetActive(true);
        scoreTxt.text = "Score : " + score.ToString();

        isGameOver = true;
    }
}
