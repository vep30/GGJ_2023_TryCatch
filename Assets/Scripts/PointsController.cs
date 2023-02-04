using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
  [SerializeField] private List<Point> row;

  public TypePoint typePoint; 

  public void RandomPoint()
  {
   
  }
  public enum TypePoint { 
    Positive,
    Negative,
    Neutral
  }
}
