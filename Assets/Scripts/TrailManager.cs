using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    [SerializeField] private Trail trail;

    public Trail InitTrail(Transform position)
    {
        return Instantiate(trail, position.position, Quaternion.identity);
    }

    public void MoveTrail(Transform target)
    {
        trail.MoveTrail(target);
    }

    public void StopTrail()
    {
        trail.StopTrail();
    }
}