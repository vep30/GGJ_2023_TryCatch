using System;
using UnityEngine;

public class ItemChoice : MonoBehaviour
{
    public enum Item
    {
        None = 0,
        Food = 1,
        Water = 2,
        Happiness = 3,
    }
    public Item item;
    public event Action<Item> ChosenItem;

    private void OnMouseDown()
    {
        ChosenItem?.Invoke(item);
    }
}