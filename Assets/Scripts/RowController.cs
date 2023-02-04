using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowController : MonoBehaviour
{
  [SerializeField] private List<Point> row1;

  public void RandomPoint()
  {
    foreach (var point in row1)
    {
      point.RandomWeight();
    }
  }
}
