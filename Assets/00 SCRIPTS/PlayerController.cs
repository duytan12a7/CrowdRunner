using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;

    [Header(" Control ")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedPlayerPosition;
    private Vector3 clickedScreenPosition;


    private void Update()
    {
        MoveFoward();
        ManageControl();
    }

    private void MoveFoward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime; /* Vector3.forward (0, 0, 1) */
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifferent = Input.mousePosition.x - clickedScreenPosition.x;
            xScreenDifferent /= Screen.width;
            xScreenDifferent *= slideSpeed;

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifferent;
            transform.position = position;
        }
    }
}
