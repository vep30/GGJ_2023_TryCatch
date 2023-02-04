using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class RowsController : MonoBehaviour
    {
        [SerializeField] private PointsController pointsController1,
            pointsController2,
            pointsController3,
            pointsController4,
            pointsController5;

        private int _positivePoints = 9, _negativePoints = 9, neutralPoints = 2;

        public event Action<int, ItemChoice.Item> ChosenItemAction; 
        public void InitPoints()
        {
            StartCoroutine(InitPointsCor());
        }

        private IEnumerator InitPointsCor()
        {
            yield return InitPointsNonNeutral(pointsController1);
            yield return InitPointsNonNeutral(pointsController2);
            yield return InitPointsNonNeutral(pointsController3);
            yield return InitPointsWithNeutral(pointsController4);
            yield return InitPointsNonNeutral(pointsController5);
        }

        private IEnumerator InitPointsNonNeutral(PointsController pointsController)
        {
            while (pointsController.haveFreeItem)
            {
                var rnd = Random.Range(0, 91);
                if (rnd > 0 && rnd <= 45 && _negativePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Negative,ChosenItem);
                    _negativePoints--;
                    Debug.Log("Set Negative point");
                }
                else if (rnd>45 && _positivePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Positive,ChosenItem);
                    _positivePoints--;
                    Debug.Log("Set Positive point");
                }
            }

            return null;
        }

        private IEnumerator InitPointsWithNeutral(PointsController pointsController)
        {
            while (pointsController.haveFreeItem)
            {
                if (neutralPoints>0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Neutral,ChosenItem);
                    neutralPoints--;
                    Debug.Log("Set Neutral point");
                }
                var rnd = Random.Range(0, 91);
                if (rnd > 0 && rnd <= 45 && _negativePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Negative,ChosenItem);
                    _negativePoints--;
                    Debug.Log("Set Negative point");
                }
                else if (rnd > 45 && _positivePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Positive,ChosenItem);
                    _positivePoints--;
                    Debug.Log("Set Positive point");
                }
            }

            return null;
        }

        public void ChosenItem(int weight, ItemChoice.Item item)
        {
            ChosenItemAction?.Invoke(weight,item);
        }
    }
}