using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;

    private void Start()
    {
        DefaultPanel();

        progressBar.value = 0f;
        levelText.text = "Level " + (RoadManager.Instance.GetIntLevel() + 1);

        GameManager.OnGameStateChanged += GameStateChange;
    }

    private void DefaultPanel()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        settingPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    private void GameStateChange(GameManager.GameState state)
    {
        if (state == GameManager.GameState.GameOver)
        {
            ShowGameOver();
            SoundsManager.OnGameOver?.Invoke();
        }
        else if (state == GameManager.GameState.LevelComplete)
        {
            ShowLevelComplete();
            SoundsManager.OnLevelComplete?.Invoke();
        }
    }

    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowGameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ShowLevelComplete()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState())
            return;

        float progress = PlayerController.Instance.transform.position.z / RoadManager.Instance.GetFinishLineZ();
        progressBar.value = progress;
    }

    public void ShowSettingPanel() => settingPanel.SetActive(true);

    public void HideSettingPanel() => settingPanel.SetActive(false);

    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
        shopManager.UpdatePurchaseButton();
    }

    public void HideShopPanel() => shopPanel.SetActive(false);
}
