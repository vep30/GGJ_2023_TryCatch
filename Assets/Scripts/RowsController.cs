using UnityEngine;

namespace DefaultNamespace
{
    public class RowsController : MonoBehaviour
    {
        [SerializeField] private PointsController pointsController;

        private int _positivePoints = 9, _negativePoints = 9, neutralPoints = 2;
        
        public void InitPoints()
        {
            while (pointsController.haveFreeItem)
            {
                var rnd = Random.Range(0, 91);
                if (rnd > 0 && rnd <= 30 && neutralPoints != 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Neutral);
                }
                else if (rnd > 30 && rnd <= 60 && _negativePoints != 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Negative);
                }
                else if (rnd > 60 && _positivePoints != 0)
                {
                    pointsController.RandomPoint(PointsController.TypePoint.Positive);
                }
            }
        }
    }
}