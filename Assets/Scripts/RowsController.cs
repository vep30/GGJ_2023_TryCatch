using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private TrailManager trailManager;
        [SerializeField] private Transform startPosition;

        [SerializeField]private List<Trail> trails;

        private int _positivePoints = 9, _negativePoints = 9, neutralPoints = 2;

        private Transform _startPos, _endPos;
        private int startPositionOnRow, startNumberRow;

        public event Action FinishAction; 
        public void InitPoints(Action<int, ItemChoice.Item> updateStatusBar)
        {
            StartCoroutine(InitPointsCor(updateStatusBar));
            finishPoint.FinishAction += Finish;
            _startPos = startPosition;
            var trail = trailManager.InitTrail(startPosition);
            trails.Add(trail);
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
                        ChosenEndPoint);
                    _negativePoints--;
                    Debug.Log("Set Negative point");
                }
                else if (rnd > 45 && _positivePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Positive, updateStatusBar, ChosenStartPoint,
                        ChosenEndPoint);
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
                        ChosenEndPoint);
                    neutralPoints--;
                }

                var rnd = Random.Range(0, 91);
                if (rnd > 0 && rnd <= 45 && _negativePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Negative, updateStatusBar, ChosenStartPoint,
                        ChosenEndPoint);
                    _negativePoints--;
                }
                else if (rnd > 45 && _positivePoints > 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Positive, updateStatusBar, ChosenStartPoint,
                        ChosenEndPoint);
                    _positivePoints--;
                }
            }

            return null;
        }

        private void ChosenStartPoint(Transform startPos, int positionOnRow, int numberRow)
        {
            _startPos = startPos;
            startPositionOnRow = positionOnRow;
            startNumberRow = numberRow;
        }

        private void ChosenEndPoint(Transform endPos, int positionOnRow, int numberRow, Point point)
        {
            if (numberRow != startNumberRow)
            {
                if (!(Mathf.Abs(startNumberRow - numberRow) > 1))
                {
                    Debug.Log("canMove");
                    MoveTrail(endPos);
                    point.ActivatePoint();
                }
            }
            else
            {
                if (!(Mathf.Abs(startPositionOnRow - positionOnRow) > 1))
                {
                    Debug.Log("canMove");
                    MoveTrail(endPos);
                    point.ActivatePoint();
                }
            }
        }

        private void Finish(Transform position)
        {
         FinishAction?.Invoke();   
        }

        public void DisableRows()
        {
            gameObject.SetActive(false);
        }

        public void MoveTrail(Transform target)
        {
            var needNewTrail = false;
            foreach (var trail in trails.Where(trail => _startPos.position == trail.lasPosition))
            {
                trail.MoveTrail(target);
                Debug.Log("Trail найден");
                return;
            }

            var newTrail = trailManager.InitTrail(_startPos);
            trails.Add(newTrail);
            newTrail.MoveTrail(target);
        }

        public void DisableTrails()
        {
            foreach (var trail in trails)
            {
                trail.gameObject.SetActive(false);
            }
        }

        private void LoseGame()
        {
            
        }
    }
}