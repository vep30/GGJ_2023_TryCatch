using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private float timeMove = 5f;
    [SerializeField] private AudioSource moving;
    private Coroutine movingTrail;
    public Vector3 lasPosition;

    private void Start()
    {
        lasPosition = transform.position;
    }

    public void MoveTrail(Transform target)
    {
        Debug.Log($"movingTrail == null {movingTrail == null}" );
        if (movingTrail == null)
        {
            Debug.Log("start move");
            lasPosition = target.position;
            movingTrail = StartCoroutine(TrailMove(target));
        }
        else
        {
            StopCoroutine(movingTrail);
            lasPosition = target.position;
            movingTrail = StartCoroutine(TrailMove(target));
        }
    }

    public void StopTrail()
    {
        if (movingTrail != null)
        {
            StopCoroutine(movingTrail);
            movingTrail = null;
        }
    }

    private IEnumerator TrailMove(Transform target)
    {
        moving.Play();
        float curTime = 0f;
        while (curTime < timeMove)
        {
            curTime += Time.deltaTime;
            float progress = Mathf.Clamp01(curTime / timeMove);

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target.position, progress);
            yield return null;
        }

        moving.Stop();
        StopTrail();
    }
}
