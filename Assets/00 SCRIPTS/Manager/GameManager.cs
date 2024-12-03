using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public enum GameState { Menu, Game, LevelComplete, GameOver }

    private GameState gameState;

    public static Action<GameState> OnGameStateChanged;

    [field: Header(" Elements ")]

    [field: SerializeField]
    public Transform RunnersParent { get; private set; }

    [field: SerializeField]
    public CrowdSystem CrowdSystem { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        OnGameStateChanged?.Invoke(gameState);
    }

    public bool IsGameState() => gameState == GameState.Game;
}
