using System;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private int weight;
    [SerializeField] private ItemChoice foodButton, waterButton, happinessButton;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer food, water, happiness;
    [SerializeField] private SpriteRenderer dotPoint;

    [SerializeField]
    private List<Sprite> spiteMinus10, spiteMinus20, spiteMinus30, spitePlus10, spitePlus20, spitePlus30;

    [SerializeField] private Sprite dotPointPlus10,
        dotPointPlus20,
        dotPointPlus30,
        dotPointMinus10,
        dotPointMinus20,
        dotPointMinus30;
    public int positionOnRow;
    public int numberRow;
    public bool isActive;
    public bool isFinish;

    public event Action<int, ItemChoice.Item> ChosenItemAction;
    public event Action<Vector3,int,int> ChosenStartPointAction; 
    public event Action<Vector3,int,int,Point> ChosenEndPointAction;
    public event Action<Vector3> FinishAction; 

    private void Awake()
    {
        foodButton.ChosenItem += DisableItemsChoice;
        waterButton.ChosenItem += DisableItemsChoice;
        happinessButton.ChosenItem += DisableItemsChoice;
    }

    public void SetWeight(int value)
    {
        weight = value;
        switch (value)
        {
            case -30:
                food.sprite = spiteMinus30[0];
                water.sprite = spiteMinus30[1];
                dotPoint.sprite = dotPointMinus30;
                break;
            case -20:
                food.sprite = spiteMinus20[0];
                water.sprite = spiteMinus20[1];
                dotPoint.sprite = dotPointMinus20;
                break;
            case -10:
                food.sprite = spiteMinus10[0];
                water.sprite = spiteMinus10[1];
                dotPoint.sprite = dotPointMinus10;
                break;
            case 10:
                food.sprite = spitePlus10[0];
                water.sprite = spitePlus10[1];
                dotPoint.sprite = dotPointPlus10;
                break;
            case 20:
                food.sprite = spitePlus20[0];
                water.sprite = spitePlus20[1];
                dotPoint.sprite = dotPointPlus20;
                break;
            case 30:
                food.sprite = spitePlus30[0];
                water.sprite = spitePlus30[1];
                dotPoint.sprite = dotPointPlus30;
                break;
        }
        isActive = true;
    }

    private void OnMouseDown()
    {
        if (isFinish)
        {
            FinishAction?.Invoke(transform.position);
            Debug.Log("finish");
            return;
        }
        if (!isActive)
        {
            ChosenStartPointAction?.Invoke(transform.position,positionOnRow,numberRow);
            Debug.Log("StartPoint");
            return;
        }
        ChosenEndPointAction?.Invoke(transform.position,positionOnRow,numberRow,this);
        Debug.Log("EndPoint");
    }

    public void ActivatePoint()
    {
        foodButton.gameObject.SetActive(true);
        waterButton.gameObject.SetActive(true);
        happinessButton.gameObject.SetActive(true);
    }

    private void DisableItemsChoice(ItemChoice.Item item)
    {
        Debug.Log($"Choice is {item}");
        foodButton.gameObject.SetActive(false);
        waterButton.gameObject.SetActive(false);
        happinessButton.gameObject.SetActive(false);
        ChosenItemAction?.Invoke(weight,item);
        isActive = false;
    }

    public void SetColor(Color color)
    {
        var spriteColor = sprite.color;
        spriteColor = new Color(color.r,color.g,color.b);
        sprite.color = spriteColor;
    }
}