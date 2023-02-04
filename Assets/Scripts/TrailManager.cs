using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    [SerializeField] private Trail trail;

    public void InitTrail()
    {
        Instantiate(trail, transform.localPosition, Quaternion.identity);
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