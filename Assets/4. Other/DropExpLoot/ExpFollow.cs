using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpFollow : MonoBehaviour
{
    public Transform target;
    public float minMod;
    public float maxMod;
    Vector3 _velocity = Vector3.zero;
    bool isFollowing;

    public void StartFollowing()
    {
        float randomTime = Random.Range(0f, 0.5f);
        StartCoroutine(WaitTime(randomTime));
    }
    private void Update()
    {
        if (isFollowing)
        {
            if(target != null)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target.position, ref _velocity, Time.deltaTime * Random.Range(minMod, maxMod));
            }
        }
    }

    private IEnumerator WaitTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isFollowing = true;
    }
}
