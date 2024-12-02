using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform runnersParent;

    [Header(" Settings ")]
    [SerializeField] private float radius;

    private void Update()
    {
        PlaceRunners();
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float r = Mathf.Sqrt(index) * radius;
        float theta = index * Global.GOLDEN_ANGLE * Mathf.Deg2Rad;

        // Chuyển từ toạ độ cực (r, θ) sang toạ độ Đề-các (Descartes)
        float x = r * Mathf.Cos(theta);
        float z = r * Mathf.Sin(theta);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius() => radius * Mathf.Sqrt(runnersParent.childCount);
}
