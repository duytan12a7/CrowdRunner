using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdCounter : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshPro crowdCounterText;

    private void Update()
    {
        crowdCounterText.text = GameManager.Instance.RunnersParent.childCount.ToString();
    }
}
