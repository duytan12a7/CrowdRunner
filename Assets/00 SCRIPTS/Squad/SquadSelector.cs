using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelector : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform runnersParent;
    [SerializeField] private RunnerSelector runnerSelectorPrefab;
    private int currentSkinIndex;

    private void Awake()
    {
        ShopManager.OnSetSkin += SetSkin;
        LoadData();
    }

    private void OnDestroy()
    {
        ShopManager.OnSetSkin -= SetSkin;
    }

    private void Start()
    {
        SetSkin(currentSkinIndex);
    }

    public void SetSkin(int skinIndex)
    {
        currentSkinIndex = skinIndex;

        for (int i = 0; i < runnersParent.childCount; i++)
            runnersParent.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);

        runnerSelectorPrefab.SelectRunner(skinIndex);
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("LastSkin", currentSkinIndex);
    }

    private void LoadData()
    {
        currentSkinIndex = PlayerPrefs.GetInt("LastSkin");
    }
}
