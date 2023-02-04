using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Point : MonoBehaviour
{
    [SerializeField] private int weight;
    [SerializeField] private ItemChoice foodButton, waterButton, happinessButton;
    private bool isPositive;


    private void Awake()
    {
        foodButton.ChosenItem += DisableItemsChoice;
        waterButton.ChosenItem += DisableItemsChoice;
        happinessButton.ChosenItem += DisableItemsChoice;
    }

    public void RandomWeight()
    {
        var rnd = Random.Range(100, 400);
        weight = Mathf.RoundToInt(rnd / 100) * 10;
    }

    private void OnMouseDown()
    {
        Debug.Log("Click to main point");
        foodButton.gameObject.SetActive(true);
        waterButton.gameObject.SetActive(true);
        happinessButton.gameObject.SetActive(true);
    }

    public void DisableItemsChoice(ItemChoice.Item item)
    {
        Debug.Log($"Choice is {item}");
        foodButton.gameObject.SetActive(false);
        waterButton.gameObject.SetActive(false);
        happinessButton.gameObject.SetActive(false);
    }
}