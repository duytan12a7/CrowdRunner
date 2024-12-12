using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private Button purchaseButton;

    [Header(" Skins ")]
    [SerializeField] private Sprite[] skins;

    [Header(" Pricing")]
    [SerializeField] private int currentPrice;
    private int basePrice = 500;
    private int priceIncreaseStep = 250;
    private int productsSold;
    [SerializeField] private TextMeshProUGUI priceText;

    [Header(" Events")]
    public static Action<int> OnSetSkin;

    private void Start()
    {
        CalculatePrice();
        ConfigureButtons();
        UpdatePurchaseButton();
    }
    private void CalculatePrice()
    {
        productsSold = PlayerPrefs.GetInt(Global.PrefsTag.ProductsSold, 0);
        currentPrice = basePrice + (priceIncreaseStep * productsSold);
        priceText.text = currentPrice.ToString();
    }

    private void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("SkinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlock();
    }

    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }

    private void ConfigureButtons()
    {
        UnlockSkin(0);
        
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("SkinButton" + i) == 1;
            skinButtons[i].Configure(skins[i], unlocked);

            int skinIndex = i;
            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    private void SelectSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        SelectSkin(skinIndex);
    }

    private void SelectSkin(int skinIndex)
    {
        OnSetSkin?.Invoke(skinIndex);

        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (skinIndex == i)
                skinButtons[i].Select();
            else
                skinButtons[i].Deselect();
        }
    }

    public void PurchaseSkin()
    {
        List<SkinButton> skinButtonList = new();

        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (!skinButtons[i].IsUnlocked())
                skinButtonList.Add(skinButtons[i]);
        }
        if (skinButtonList.Count <= 0)
            return;

        SkinButton randomSkinButton = skinButtonList[Random.Range(0, skinButtonList.Count)];

        UnlockSkin(randomSkinButton);
        SelectSkin(randomSkinButton);

        DataManager.Instance.UseCoins(currentPrice);

        PlayerPrefs.SetInt(Global.PrefsTag.ProductsSold, PlayerPrefs.GetInt(Global.PrefsTag.ProductsSold) + 1);
        CalculatePrice();

        UpdatePurchaseButton();
    }

    public void UpdatePurchaseButton()
    {
        if (DataManager.Instance.GetCoins() < currentPrice)
            purchaseButton.interactable = false;
        else
            purchaseButton.interactable = true;
    }
}
