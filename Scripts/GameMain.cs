using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public static GameMain instance;
    public bool gameOver;

    private void Start()
    {
        InitGame();
    }

    void InitGame()
    {
        instance = this;
        gameOver = false;
    }
}
