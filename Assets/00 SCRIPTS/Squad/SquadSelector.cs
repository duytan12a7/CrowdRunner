using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelector : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform runnersParent;
    [SerializeField] private RunnerSelector runnerSelectorPrefab;
    private int currentSkinIndex;

    private void Start()
    {
        ShopManager.OnSkinSelected += SelectSkin;
    }

    private void OnDestroy()
    {
        ShopManager.OnSkinSelected -= SelectSkin;
    }

    public void SelectSkin(int skinIndex)
    {
        currentSkinIndex = skinIndex;

        for (int i = 0; i < runnersParent.childCount; i++)
            runnersParent.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);

        runnerSelectorPrefab.SelectRunner(skinIndex);
    }
}
