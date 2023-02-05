using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGame : MonoBehaviour
{
    [SerializeField] private RowsController rowsController;
    [SerializeField] private BarController barController;
    [SerializeField] private GameObject moveBack;
    [SerializeField] private float timeDownHand;
    [SerializeField] private Image finishSprite;
    [SerializeField] private Sprite baby, goblin, govnyashka;
    [SerializeField] private AudioSource finishSounds;
    [SerializeField] private AudioClip goodClip, badClip;
    private int numberMoves = 9;
    private float targetBackground = 242;

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
        Debug.Log($"food = {barController.FoodValue} water = {barController.WaterValue} happiness = {barController.HappinessValue}");
        Debug.Log("EndGame");
        if (
            (barController.FoodValue >= 0 && barController.FoodValue <= 30) &&
            (barController.WaterValue >= 0 && barController.WaterValue <= 30) &&
            (barController.HappinessValue >= 0 && barController.HappinessValue <= 30))
        {
            Debug.Log("HappyEndGame");
            finishSounds.clip = goodClip;
            finishSounds.Play();
            finishSprite.sprite = baby;
        }
        else if (barController.FoodValue > 30 || barController.WaterValue > 30 || barController.HappinessValue > 30 ||
                 ((barController.FoodValue >= -30 && barController.FoodValue < 0) || (barController.WaterValue >= -30 &&
                  barController.WaterValue < 0) || (barController.HappinessValue >= -30 &&
                  barController.HappinessValue < 0)))
        {
            Debug.Log("Goblin");
            finishSounds.clip = badClip;
            finishSounds.Play();
            finishSprite.sprite = goblin;
        }
        else if (barController.FoodValue < -30 || barController.WaterValue < -30 || barController.HappinessValue < -30)
        {
            Debug.Log("PieceDeath");
            finishSounds.clip = badClip;
            finishSounds.Play();
            finishSprite.sprite = govnyashka;
        }
        rowsController.DisableRows();
        StartCoroutine(MoveBackground());
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

    private IEnumerator MoveBackground()
    {
        var curTime = 0f;
        var startBackgroundpos = moveBack.transform.localPosition;
        while (curTime < timeDownHand)
        {
            curTime += Time.deltaTime;
            var progress = Mathf.Clamp01(curTime / timeDownHand);
            moveBack.transform.localPosition =
                Vector3.Lerp(startBackgroundpos, new Vector3(0, targetBackground, 0), progress);
            yield return null;
        }
    }

}