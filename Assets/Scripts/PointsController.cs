using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class PointsController : MonoBehaviour
{
  [SerializeField] private List<Point> row;

  public bool haveFreeItem => tempList.Count>0;
  private List<Point> tempList;

  private void Start()
  {
    tempList = new List<Point>(row);
  }
  
  public void RandomPoint(TypePoint type, Action<int, ItemChoice.Item> action)
  {
    if (!tempList.IsNullOrEmpty())
    {
      tempList.Shuffle();
      switch (type)
      {
        case TypePoint.Positive:
          tempList[0].SetWeight(RandomWeight());
          break;
        case TypePoint.Negative:
          tempList[0].SetWeight(RandomWeight() * -1);
          break;
        case TypePoint.Neutral:
          tempList[0].SetWeight(0);
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }

      tempList[0].ChosenItem += action;
      tempList.RemoveAt(0);
    }
  }

  private int RandomWeight()
  {
    var rnd = Random.Range(100, 400);
    return Mathf.RoundToInt(rnd / 100) * 10;
  }
  
  public enum TypePoint { 
    Positive,
    Negative,
    Neutral
  }
}
