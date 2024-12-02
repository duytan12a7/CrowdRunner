using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform menuPanel;
    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);

        menuPanel.gameObject.SetActive(false);
    }
}
