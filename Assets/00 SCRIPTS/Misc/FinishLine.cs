using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private Collider collider;

    public void Disable()
    {
        collider.enabled = false;
    }
}
