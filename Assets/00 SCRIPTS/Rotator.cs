using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header(" Rotation ")]
    [SerializeField] private float rotationSpeed;

    [Header(" Movement ")]
    [SerializeField] private Vector2 minMaxX;
    [SerializeField] private float maxDuration;
    [SerializeField] private float minDuration;
    private Vector3 targetPosition;

    private void Start()
    {
        transform.position = new Vector3(minMaxX.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(minMaxX.y, transform.position.y, transform.position.z);
        MoveToTargetPosition();
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    private void MoveToTargetPosition()
    {
        float patrolDuration = Random.Range(minDuration, maxDuration);
        transform.DOMove(targetPosition, patrolDuration).SetEase(Ease.Linear).OnComplete(SetNextTargetPosition);
    }

    private void SetNextTargetPosition()
    {
        if (Mathf.Abs(targetPosition.x - minMaxX.x) < 0.1f)
            targetPosition.x = minMaxX.y;
        else
            targetPosition.x = minMaxX.x;

        MoveToTargetPosition();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 p0 = transform.position;
        p0.x = minMaxX.x;

        Vector3 p1 = p0;
        p1.x = minMaxX.y;

        float cubeSize = 0.5f;

        Gizmos.DrawCube(p0, cubeSize * Vector3.one);
        Gizmos.DrawCube(p1, cubeSize * Vector3.one);
    }
}
