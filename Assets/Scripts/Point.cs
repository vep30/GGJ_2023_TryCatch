using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private int weight;
    [SerializeField] private ItemChoice foodButton, waterButton, happinessButton;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private TrailManager trailManager;

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