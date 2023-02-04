
using UnityEngine;

namespace DefaultNamespace
{
    public class RowController : MonoBehaviour
    {
        [SerializeField] private PointsController pointsController;

        private int 
            _positiveFoodPoints = 3, 
            _negativeFoodPoints = 3,
            _positiveWaterPoints = 3,
            _negativeWaterPoints = 3,
            _positiveHappinessPoints = 3,
            _negaiveHappinesPoints = 3,
            neutralPoints = 2;

        private void InitPoints()
        {
            
        }

    }
}