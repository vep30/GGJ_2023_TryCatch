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

        [SerializeField] private Point finishPoint;
        
        private int _positivePoints = 9, _negativePoints = 9, neutralPoints = 2;

        private Vector3 _startPos, _endPos;
        private int startPositionOnRow, startNumberRow;

        public event Action FinishAction; 
        public void InitPoints(Action<int, ItemChoice.Item> updateStatusBar)
        {
            StartCoroutine(InitPointsCor(updateStatusBar));
            finishPoint.FinishAction += Finish;
        }

        private IEnumerator InitPointsCor(Action<int, ItemChoice.Item> updateStatusBar)
        {
            yield return InitPointsNonNeutral(pointsController1, updateStatusBar);
            yield return InitPointsNonNeutral(pointsController2, updateStatusBar);
            yield return InitPointsNonNeutral(pointsController3, updateStatusBar);
            yield return InitPointsWithNeutral(pointsController4, updateStatusBar);
            yield return InitPointsNonNeutral(pointsController5, updateStatusBar);
        }

        private IEnumerator InitPointsNonNeutral(PointsController pointsController,
            Action<int, ItemChoice.Item> updateStatusBar)
        {
            while (pointsController.haveFreeItem)
            {
                var rnd = Random.Range(0, 91);
                if (rnd > 0 && rnd <= 45 && _negativePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Negative, updateStatusBar, ChosenStartPoint,
                        ChosenEndPoint,Finish);
                    _negativePoints--;
                    Debug.Log("Set Negative point");
                }
                else if (rnd > 45 && _positivePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Positive, updateStatusBar, ChosenStartPoint,
                        ChosenEndPoint,Finish);
                    _positivePoints--;
                    Debug.Log("Set Positive point");
                }
            }

            return null;
        }

        private IEnumerator InitPointsWithNeutral(PointsController pointsController,
            Action<int, ItemChoice.Item> updateStatusBar)
        {
            while (pointsController.haveFreeItem)
            {
                if (neutralPoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Neutral, updateStatusBar, ChosenStartPoint,
                        ChosenEndPoint,Finish);
                    neutralPoints--;
                }

                var rnd = Random.Range(0, 91);
                if (rnd > 0 && rnd <= 45 && _negativePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Negative, updateStatusBar, ChosenStartPoint,
                        ChosenEndPoint,Finish);
                    _negativePoints--;
                }
                else if (rnd > 45 && _positivePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Positive, updateStatusBar, ChosenStartPoint,
                        ChosenEndPoint,Finish);
                    _positivePoints--;
                }
            }

            return null;
        }

        private void ChosenStartPoint(Vector3 startPos, int positionOnRow, int numberRow)
        {
            _startPos = startPos;
            startPositionOnRow = positionOnRow;
            startNumberRow = numberRow;
        }

        private void ChosenEndPoint(Vector3 endPos, int positionOnRow, int numberRow, Point point)
        {
            if (numberRow != startNumberRow)
            {
                if (!(Mathf.Abs(startNumberRow - numberRow) > 1))
                {
                    Debug.Log("canMove");
                    point.ActivatePoint();
                }
            }
            else
            {
                if (!(Mathf.Abs(startPositionOnRow - positionOnRow) > 1))
                {
                    Debug.Log("canMove");
                    point.ActivatePoint();
                }
            }
        }

        private void Finish(Vector3 position)
        {
         FinishAction?.Invoke();   
        }

        public void DisableRows()
        {
            gameObject.SetActive(false);
        }
    }
}