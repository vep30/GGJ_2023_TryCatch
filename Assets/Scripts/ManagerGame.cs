using System;
using DefaultNamespace;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    [SerializeField] private RowsController rowsController;
    [SerializeField] private BarController barController;
    private int numberMoves = 9;

    public void StartGame()
    {
        rowsController.InitPoints(UpdateBar);
        rowsController.FinishAction += EndGame;
        UpdateNumberMoves();
        barController.StartResetSlider();
    }

    private void RowsControllerOnUpdateNumberMove()
    {
        numberMoves--;
        if (numberMoves <= 0)
        {
            EndGame();
        }

        UpdateNumberMoves();
    }

    private void EndGame()
    {
        Debug.Log("EndGame");
        if (
            (barController.FoodValue >= 0 && barController.FoodValue <= 30) &&
            (barController.WaterValue >= 0 && barController.WaterValue <= 30) &&
            (barController.HappinessValue >= 0 && barController.HappinessValue <= 30))
        {
            Debug.Log("HappyEndGame");
        }
        else if (barController.FoodValue > 30 || barController.WaterValue > 30 || barController.HappinessValue > 30 ||
                 (barController.FoodValue >= -30 && barController.FoodValue < 0 && barController.WaterValue >= -30 &&
                  barController.WaterValue < 0 && barController.HappinessValue >= -30 &&
                  barController.HappinessValue < 0))
        {
            Debug.Log("Goblin");
        }
        else if (barController.FoodValue < -30 || barController.WaterValue < -30 || barController.HappinessValue < -30)
        {
            Debug.Log("PieceDeath");
        }
    }

    public void UpdateBar(int weight, ItemChoice.Item item)
    {
        switch (item)
        {
            case ItemChoice.Item.Food:
                barController.SetFoodValue(weight);
                break;
            case ItemChoice.Item.Water:
                barController.SetWaterValue(weight);
                break;
            case ItemChoice.Item.Happiness:
                barController.SetHappinessValue(weight);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(item), item, null);
        }

        RowsControllerOnUpdateNumberMove();
    }

    public void UpdateNumberMoves()
    {
        barController.SetNumberMoves(numberMoves);
    }
}