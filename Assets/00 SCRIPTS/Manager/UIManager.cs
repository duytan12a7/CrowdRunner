using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;

    private void Start()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);

        progressBar.value = 0f;
        levelText.text = "Level " + (ChunkManager.Instance.GetIntLevel() + 1);
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState())
            return;

        float progress = PlayerController.Instance.transform.position.z / ChunkManager.Instance.GetFinishLineZ();
        progressBar.value = progress;
    }
}
