using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    private static SquadController instance;
    public static SquadController Instance => instance;

    [Header(" Elements ")]
    [SerializeField] private SquadFormation squadFormation;
    [SerializeField] private SquadAnimator squadAnimator;
    [SerializeField] private BonusRunnersParent bonusRunnersParent;
    private bool canMove;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float roadWidth;

    [Header(" Control ")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedPlayerPosition;
    private Vector3 clickedScreenPosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        squadAnimator = GetComponent<SquadAnimator>();

        GameManager.OnGameStateChanged += GameStateChanged;
    }

    private void Update()
    {
        if (!canMove) return;

        MoveFoward();
        ManageControl();
    }

    private void MoveFoward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime; /* Vector3.forward (0, 0, 1) */
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
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
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;

            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            Vector3 position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + squadFormation.GetSquadRadius(),
                roadWidth / 2 - squadFormation.GetSquadRadius());

            transform.position = position;
        }
    }

    private void GameStateChanged(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.Game)
            StartMoving();
        else if (gameState == GameManager.GameState.GameOver || gameState == GameManager.GameState.LevelComplete)
            StopMoving();
    }

    private void StartMoving()
    {
        canMove = true;

        squadAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;

        squadAnimator.Idle();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChanged;
    }
}
