using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerDestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<Runner>(out Runner runner))
            runner.Explode();
    }
}
