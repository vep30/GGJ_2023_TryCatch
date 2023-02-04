using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private int weight;
    [SerializeField] private ItemChoice foodButton, waterButton, happinessButton;
    private bool isActive; 

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
        if (!isActive)
            return;
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
        isActive = false;
    }
}