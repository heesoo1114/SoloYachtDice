using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

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
            }
        }
    }

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
}
