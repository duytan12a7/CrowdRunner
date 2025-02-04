using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance => instance;

    [Header(" Coin Texts ")]
    [SerializeField] private Text[] coinsTexts;
    private int coins;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        coins = PlayerPrefs.GetInt("coins", 0);
    }

    // Start is called before the first frame update
    private void Start()
    {
        UpdateCoinsTexts();
    }

    private void UpdateCoinsTexts()
    {
        foreach (Text coinText in coinsTexts)
        {
            coinText.text = coins.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        UpdateCoinsTexts();

        PlayerPrefs.SetInt("coins", coins);
    }

    public void UseCoins(int amount)
    {
        coins -= amount;

        UpdateCoinsTexts();

        PlayerPrefs.SetInt("coins", coins);
    }

    public int GetCoins() => PlayerPrefs.GetInt("coins", coins);
}
