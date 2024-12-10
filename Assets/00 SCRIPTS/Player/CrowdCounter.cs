using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdCounter : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private Transform parent;

    private void Update()
    {
        crowdCounterText.text = parent.childCount.ToString();

        if (parent.childCount < 1)
        {
            SquadController.Instance.SetMoveSpeed(Global.MOVE_SPEED);
            Destroy(gameObject);
        }
    }
}
