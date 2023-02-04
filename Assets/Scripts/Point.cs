using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Point : MonoBehaviour
{
    [SerializeField] private int weight;
    [SerializeField] private GameObject foodButton, waterButton, happinessButton;
    private bool isPositive;

    public void RandomWeight()
    {
        var rnd = Random.Range(100, 400);
        weight = Mathf.RoundToInt(rnd / 100) * 10;
    }

    private void OnMouseDown()
    {
        Debug.Log("Click to main point");
        foodButton.SetActive(true);
        waterButton.SetActive(true);
        happinessButton.SetActive(true);
    }
}
