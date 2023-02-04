using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
   [SerializeField] private RowsController rowsController;
   [SerializeField] private BarController barController;

   public void StartGame()
   {
      rowsController.InitPoints();
      rowsController.ChosenItemAction += UpdateBar;
      barController.StartResetSlider();
      
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
   }
}
