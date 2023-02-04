using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private int weight;
    [SerializeField] private ItemChoice foodButton, waterButton, happinessButton;

    public int positionOnRow;
    public int numberRow;
    public bool isActive;
    public bool isFinish;

    public event Action<int, ItemChoice.Item> ChosenItemAction;
    public event Action<Vector3,int,int> ChosenStartPointAction; 
    public event Action<Vector3,int,int,Point> ChosenEndPointAction;
    public event Action FinishAction; 

    private void Awake()
    {
        foodButton.ChosenItem += DisableItemsChoice;
        waterButton.ChosenItem += DisableItemsChoice;
        happinessButton.ChosenItem += DisableItemsChoice;
        isActive = true;
    }

    public void SetWeight(int value)
    {
        weight = value;
    }

    private void OnMouseDown()
    {
        if (isFinish)
        {
            FinishAction?.Invoke();
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
}