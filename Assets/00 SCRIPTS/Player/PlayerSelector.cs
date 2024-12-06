using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform runnersParent;
    [SerializeField] private RunnerSelector runnerSelectorPrefab;

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
        for (int i = 0; i < runnersParent.childCount; i++)
            runnersParent.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);

        runnerSelectorPrefab.SelectRunner(skinIndex);
    }
}
