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
        isFollowing = true;
    }
    private void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref _velocity, Time.deltaTime * Random.Range(minMod, maxMod));
        }
    }
}
