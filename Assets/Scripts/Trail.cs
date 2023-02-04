using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private float timeMove = 5f;
    private Coroutine movingTrail;

    public void MoveTrail(Transform target)
    {
        if (movingTrail == null)
            movingTrail = StartCoroutine(TrailMove(target));
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
        float curTime = 0f;
        while (curTime < timeMove)
        {
            curTime += Time.deltaTime;
            float progress = Mathf.Clamp01(curTime / timeMove);

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target.localPosition, progress);
            yield return null;
        }

        StopTrail();
    }
}
