using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Doors : MonoBehaviour
{
    enum BonusType { Add, Subtract, Multiply, Divide }

    [Header(" Elements ")]
    [SerializeField] private SpriteRenderer rightDoorRenderer;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private SpriteRenderer leftDoorRenderer;
    [SerializeField] private TextMeshPro leftDoorText;

    [Header("Settings")]
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmount;
    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmount;
    [SerializeField] private Color bonusColor;
    [SerializeField] private Color penaltyColor;

    private void Start()
    {
        ConfigureDoors();
    }

    private void ConfigureDoors()
    {
        switch (rightDoorBonusType)
        {
            case BonusType.Add:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "+" + rightDoorBonusAmount;
                break;
            case BonusType.Subtract:
                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "-" + rightDoorBonusAmount;
                break;
            case BonusType.Multiply:
                rightDoorRenderer.color = bonusColor;
                rightDoorText.text = "x" + rightDoorBonusAmount;
                break;
            case BonusType.Divide:
                rightDoorRenderer.color = penaltyColor;
                rightDoorText.text = "/" + rightDoorBonusAmount;
                break;
        }

        switch (leftDoorBonusType)
        {
            case BonusType.Add:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "+" + leftDoorBonusAmount;
                break;
            case BonusType.Subtract:
                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "-" + leftDoorBonusAmount;
                break;
            case BonusType.Multiply:
                leftDoorRenderer.color = bonusColor;
                leftDoorText.text = "x" + leftDoorBonusAmount;
                break;
            case BonusType.Divide:
                leftDoorRenderer.color = penaltyColor;
                leftDoorText.text = "/" + leftDoorBonusAmount;
                break;
        }
    }
}
